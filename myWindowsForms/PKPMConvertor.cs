using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myWindowsForms.NeccessaryFunctions;
using APIFrameworkRef;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace myWindowsForms
{
    public class PKPMConvertor
    {
        public PKPMConvertor() { }

        public void Convert(string FullJwsPath, List<double> height, List<List<Line>> column, List<List<Line>> beam1, List<List<Line>> beam2, List<List<Line>> wall, List<List<Line>> brace1, List<List<Line>> brace2)
        {
            var Building = new Building();

            List<int> intHeight = height.Select(d => (int)d).ToList();
            Building.SetLevels(intHeight);

            #region 根据层高设定ConLevel沿高度变化
            if (Building.TopLevel > 400000)
            {
                Building.SetColConLevel(ConLevel.C80);
                Building.SetColConLevel(ConLevel.C70, Building.FloorNum / 4);
                Building.SetColConLevel(ConLevel.C60, Building.FloorNum / 4 * 2);
                Building.SetColConLevel(ConLevel.C50, Building.FloorNum / 4 * 3);
                Building.SetWallConLevel(ConLevel.C80);
                Building.SetWallConLevel(ConLevel.C70, Building.FloorNum / 4);
                Building.SetWallConLevel(ConLevel.C60, Building.FloorNum / 4 * 2);
                Building.SetWallConLevel(ConLevel.C50, Building.FloorNum / 4 * 3);
            }
            else if (Building.TopLevel > 300000)
            {
                Building.SetColConLevel(ConLevel.C75);
                Building.SetColConLevel(ConLevel.C65, Building.FloorNum / 4);
                Building.SetColConLevel(ConLevel.C55, Building.FloorNum / 4 * 2);
                Building.SetColConLevel(ConLevel.C50, Building.FloorNum / 4 * 3);
                Building.SetWallConLevel(ConLevel.C75);
                Building.SetWallConLevel(ConLevel.C65, Building.FloorNum / 4);
                Building.SetWallConLevel(ConLevel.C55, Building.FloorNum / 4 * 2);
                Building.SetWallConLevel(ConLevel.C50, Building.FloorNum / 4 * 3);
            }
            else if (Building.TopLevel > 200000)
            {
                Building.SetColConLevel(ConLevel.C70);
                Building.SetColConLevel(ConLevel.C60, Building.FloorNum / 3);
                Building.SetColConLevel(ConLevel.C50, Building.FloorNum / 3 * 2);
                Building.SetWallConLevel(ConLevel.C70);
                Building.SetWallConLevel(ConLevel.C60, Building.FloorNum / 3);
                Building.SetWallConLevel(ConLevel.C50, Building.FloorNum / 3 * 2);
            }
            else
            {
                Building.SetColConLevel(ConLevel.C65);
                Building.SetColConLevel(ConLevel.C55, Building.FloorNum / 3);
                Building.SetColConLevel(ConLevel.C50, Building.FloorNum / 3 * 2);
                Building.SetWallConLevel(ConLevel.C65);
                Building.SetWallConLevel(ConLevel.C55, Building.FloorNum / 3);
                Building.SetWallConLevel(ConLevel.C50, Building.FloorNum / 3 * 2);
            }
            #endregion


            var model = new Model();
            if (!File.Exists(FullJwsPath))
            {
                MessageBox.Show("JWS文件不存在", "提示：", MessageBoxButtons.OK);
                return;
            }
            model.OpenPMModel(FullJwsPath);
            model.SetCurrentStandFloor(1);
            ClearModel(ref model);// 清理当前模型空间 

            #region 生成楼层组装
            StandFloor standFloor;
            RealFloor realFloor;
            for (int i = 1; i <= height.Count; i++)
            {
                if (i != 1)
                {
                    model.AddStandFloor();
                }
                model.SetCurrentStandFloor(i);
                standFloor = model.GetCurrentStandFloor();
                // 恒活设置
                standFloor.SetDeadLive(float.Parse("2.5"), float.Parse("3.0"));
                // 其他必要设置：板厚、砼等级、钢筋等级etc.
                var conLevelPatt = "[0-9]+";
                standFloor.SetInfoValue(0, int.Parse("120"));//板厚
                //standFloor.SetInfoValue(1, int.Parse(Regex.Matches(slabConLevel[i - 1].ToString(), conLevelPatt)[0].Value));//板砼强度
                //standFloor.SetInfoValue(2, int.Parse("15"));//板保护层
                //standFloor.SetInfoValue(3, int.Parse(Regex.Matches(colConLevel[i - 1].ToString(), conLevelPatt)[0].Value));//柱砼强度
                //standFloor.SetInfoValue(4, int.Parse(Regex.Matches(beamConLevel[i - 1].ToString(), conLevelPatt)[0].Value));//梁砼强度
                //standFloor.SetInfoValue(5, int.Parse(Regex.Matches(wallConLevel[i - 1].ToString(), conLevelPatt)[0].Value));//墙砼强度
                standFloor.SetInfoValue(6, int.Parse("3"));//梁钢筋级别
                standFloor.SetInfoValue(7, int.Parse("3"));//柱钢筋级别
                standFloor.SetInfoValue(8, int.Parse("3"));//墙钢筋级别
                realFloor = new RealFloor();
                realFloor.SetStandFloorIndex(i);
                realFloor.SetFloorHeight(float.Parse((height[i] - height[i - 1]).ToString()));
                realFloor.SetBottomElevation(float.Parse(height[i].ToString()));
                model.AddRealFloor(realFloor);
                model.SetFloorAssemTable(i, realFloor);
            }
            #endregion

            #region 加入梁1构件
            for (int i = 0; i < beam1.Count; i++)
            {
                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach (Line beamLine in beam1[i])
                {
                    SectionShape sectionShape = new SectionShape();
                    sectionShape.H = Math.Max((int)(beamLine.Length / 12 / 50) * 50, 750);
                    sectionShape.B = (sectionShape.H / 100) * 50;
                    BeamSection beamSection = new BeamSection();
                    beamSection.SetUserSect(SectionKind.IDSec_Rectangle, sectionShape);
                    int beamSectionInt = model.AddBeamSection(beamSection);

                    Point3d pStart = beamLine.From;
                    Point3d pEnd = beamLine.To;
                    Node startNode = standFloor.AddNode((float)pStart.X, (float)pStart.Y);
                    Node endNode = standFloor.AddNode((float)pEnd.X, (float)pEnd.Y);
                    Net net = standFloor.AddLineNet(startNode.GetID(), endNode.GetID());
                    standFloor.AddBeam(beamSectionInt, net.GetID());
                }
            }
            #endregion

            #region 加入梁2构件
            for (int i = 0; i < beam2.Count; i++)
            {
                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach (Line beamLine in beam2[i])
                {
                    SectionShape sectionShape = new SectionShape();
                    sectionShape.H = Math.Max((int)(beamLine.Length / 12 / 50) * 50, 750);
                    sectionShape.B = (sectionShape.H / 100) * 50;
                    BeamSection beamSection = new BeamSection();
                    beamSection.SetUserSect(SectionKind.IDSec_Rectangle, sectionShape);
                    int beamSectionInt = model.AddBeamSection(beamSection);

                    Point3d pStart = beamLine.From;
                    Point3d pEnd = beamLine.To;
                    Node startNode = standFloor.AddNode((float)pStart.X, (float)pStart.Y);
                    Node endNode = standFloor.AddNode((float)pEnd.X, (float)pEnd.Y);
                    Net net = standFloor.AddLineNet(startNode.GetID(), endNode.GetID());
                    standFloor.AddBeam(beamSectionInt, net.GetID());
                }
            }

            #endregion

            #region 加入墙构件
            for (int i = 0; i < wall.Count; i++)
            {
                WallSection wallSection = new WallSection();
                int tempWallTickness = Building.WallSectionList[i];
                wallSection.Set(1, tempWallTickness, 0, 1, 1, 1);
                int wallSectionInt = model.AddWallSection(wallSection);
                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach (Line wallLine in wall[i])
                {
                    Point3d pStart = wallLine.From;
                    Point3d pEnd = wallLine.To;
                    Node startNode = standFloor.AddNode((float)pStart.X, (float)pStart.Y);
                    Node endNode = standFloor.AddNode((float)pEnd.X, (float)pEnd.Y);
                    Net net = standFloor.AddLineNet(startNode.GetID(), endNode.GetID());
                    standFloor.AddWall(wallSectionInt, net.GetID());
                }
            }

            #endregion

            #region 加入柱构件
            for (int i = 0; i < column.Count; i++)
            {
                SectionShape sectionShape = new SectionShape();
                Section tempSection = Building.ColSectionList[i];
                sectionShape.B = tempSection.B;
                sectionShape.H = tempSection.H;
                BraceSection braSection = new BraceSection();
                braSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int braceSectionInt = model.AddBraceSection(braSection);
                ColumnSection colSection = new ColumnSection();
                colSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int colSectionInt = model.AddColumnSection(colSection);

                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach(Line colLine in column[i])
                {
                    Point3d pStart = colLine.From;
                    Point3d pEnd = colLine.To;
                    int xStart = (int)pStart.X;
                    int yStart = (int)pStart.Y;
                    int xEnd = (int)pEnd.X;
                    int yEnd = (int)pEnd.Y;
                    if (xStart != xEnd | yStart != yEnd)
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        Node endNode = standFloor.AddNode(xEnd, yEnd);
                        standFloor.AddBrace(braceSectionInt, startNode.GetID(), endNode.GetID(), 0, (int)(height[i + 1] - height[i]), 0);
                    }
                    else
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        standFloor.AddColumn(colSectionInt, startNode.GetID());
                    }
                }
            }
            #endregion

            #region 加入支撑1构件
            for (int i = 0; i < brace1.Count; i++)
            {
                SectionShape sectionShape = new SectionShape();
                Section tempSection = Building.ColSectionList[i];
                sectionShape.B = tempSection.B;
                sectionShape.H = tempSection.H;
                BraceSection braSection = new BraceSection();
                braSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int braceSectionInt = model.AddBraceSection(braSection);
                ColumnSection colSection = new ColumnSection();
                colSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int colSectionInt = model.AddColumnSection(colSection);

                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach (Line braceLine in brace1[i])
                {
                    Point3d pStart = braceLine.From;
                    Point3d pEnd = braceLine.To;
                    int xStart = (int)pStart.X;
                    int yStart = (int)pStart.Y;
                    int xEnd = (int)pEnd.X;
                    int yEnd = (int)pEnd.Y;
                    if (xStart != xEnd | yStart != yEnd)
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        Node endNode = standFloor.AddNode(xEnd, yEnd);
                        standFloor.AddBrace(braceSectionInt, startNode.GetID(), endNode.GetID(), 0, (int)(height[i + 1] - height[i]), 0);
                    }
                    else
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        standFloor.AddColumn(colSectionInt, startNode.GetID());
                    }
                }
            }
            #endregion

            #region 加入支撑2构件
            for (int i = 0; i < brace2.Count; i++)
            {
                SectionShape sectionShape = new SectionShape();
                Section tempSection = Building.ColSectionList[i];
                sectionShape.B = tempSection.B;
                sectionShape.H = tempSection.H;
                BraceSection braSection = new BraceSection();
                braSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int braceSectionInt = model.AddBraceSection(braSection);
                ColumnSection colSection = new ColumnSection();
                colSection.SetUserSect((SectionKind)tempSection.Type, sectionShape);
                int colSectionInt = model.AddColumnSection(colSection);

                model.SetCurrentStandFloor(i + 1);
                standFloor = model.GetCurrentStandFloor();
                foreach (Line braceLine in brace2[i])
                {
                    Point3d pStart = braceLine.From;
                    Point3d pEnd = braceLine.To;
                    int xStart = (int)pStart.X;
                    int yStart = (int)pStart.Y;
                    int xEnd = (int)pEnd.X;
                    int yEnd = (int)pEnd.Y;
                    if (xStart != xEnd | yStart != yEnd)
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        Node endNode = standFloor.AddNode(xEnd, yEnd);
                        standFloor.AddBrace(braceSectionInt, startNode.GetID(), endNode.GetID(), 0, (int)(height[i + 1] - height[i]), 0);
                    }
                    else
                    {
                        Node startNode = standFloor.AddNode(xStart, yStart);
                        standFloor.AddColumn(colSectionInt, startNode.GetID());
                    }
                }
            }
            #endregion

            model.SavePMModel();
            SocketClient myClient = new SocketClient(2000);
            myClient.StartClient();
        }
        protected void ClearModel(ref Model model)
        {
            StandFloor standFloor;
            List<Node> nodes;
            List<Net> nets;
            List<Beam> beams;
            List<Brace> braces;
            List<Column> columns;
            List<Wall> walls;


            #region 1.删除二层及以上的所有标准层
            int standFloorNum = model.GetStandFloorCount();
            if (standFloorNum > 1)
            {
                for (int i = standFloorNum; i > 1; i--)
                {
                    model.DeleteStandFloor(i);
                }
            }
            #endregion

            #region 2.删除第一标准层内所有的节点、轴网内容
            model.SetCurrentStandFloor(1);
            standFloor = model.GetCurrentStandFloor();
            nets = standFloor.GetNets();
            if (nets.Count > 0)
            {
                foreach (Net net in nets)
                {
                    standFloor.DeleteNet(net.GetID());
                }
            }


            nodes = standFloor.GetNodes();
            if (nodes.Count > 0)
            {
                foreach (Node node in nodes)
                {
                    standFloor.DeleteNode(node.GetID());
                }
            }



            #endregion

            #region 3.删除第一标准层内所有的梁、柱、支撑构件
            beams = standFloor.GetBeams();
            if (beams.Count > 0)
            {
                foreach (Beam beam in beams)
                {
                    standFloor.DeleteBeam(beam.GetID());
                }
            }


            braces = standFloor.GetBraces();
            if (braces.Count > 0)
            {
                foreach (Brace brace in braces)
                {
                    standFloor.DeleteBrace(brace.GetID());
                }
            }

            columns = standFloor.GetColumns();
            if (columns.Count > 0)
            {
                foreach (Column column in columns)
                {
                    standFloor.DeleteColumn(column.GetID());
                }
            }

            walls = standFloor.GetWalls();
            if (walls.Count > 0)
            {
                foreach (Wall wall in walls)
                {
                    standFloor.DeleteWall(wall.GetID());
                }
            }

            #endregion

            #region 4.删除楼层组装
            model.DeleteRealFloor(1, 1);
            #endregion
        }

    }
}
