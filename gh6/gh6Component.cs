using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Data;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace gh6
{
    public class gh6Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public gh6Component()
          : base("Auto_Design_Software", "ADS",
              "Auto_Design_Software",
              "ADS", "type2")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("外立面曲面", "外", "请选择外立面曲面", GH_ParamAccess.item);
            pManager.AddBrepParameter("核心筒曲面", "内", "请选择核心筒曲面", GH_ParamAccess.item);
            pManager.AddNumberParameter("层高表", "H", "请输入层高表", GH_ParamAccess.list);
            pManager.AddNumberParameter("基底标高", "Base", "请输入基底标高", GH_ParamAccess.item);
            pManager.AddNumberParameter("外柱与外立面偏移量", "Dis", "请输入偏移量", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("核心筒柱数", "NC", "请输入核心筒柱数", GH_ParamAccess.item, 4);
            pManager.AddIntegerParameter("角柱数", "NB", "请输入外立面角柱数", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("腰部和环带桁架楼层表", "S", "请输入腰部桁架所在楼层", GH_ParamAccess.list, new List<int> { 0 });
            pManager.AddIntegerParameter("腰部桁架段数", "NS", "请输入伸臂桁架段数", GH_ParamAccess.list, new List<int> { 0 });
            pManager.AddIntegerParameter("腰部桁架类型", "NT", "请输入伸臂桁架类型", GH_ParamAccess.list, new List<int> { 1 });
            pManager.AddIntegerParameter("环带桁架段数", "HS", "请输入环带桁架段数", GH_ParamAccess.list, new List<int> { 0 });
            pManager.AddIntegerParameter("环带桁架类型", "HT", "请输入环带桁架类型", GH_ParamAccess.list, new List<int> { 1 });

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Column", "Col", "This is the column set", GH_ParamAccess.item);
            pManager.AddLineParameter("Beam1", "Beam1", "This is the beam1 set", GH_ParamAccess.item);
            pManager.AddLineParameter("Beam2", "Beam2", "This is the beam2 set", GH_ParamAccess.item);
            pManager.AddLineParameter("Wall", "Wall", "This is the wall set", GH_ParamAccess.item);
            pManager.AddLineParameter("Brace1", "Brace1", "This is the waist brace1 set", GH_ParamAccess.item);
            pManager.AddLineParameter("Brace2", "Brace2", "This is the waist brace2 set", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Brep surf1 = null;
            Brep surf2 = null;
            List<double> height = new List<double>();
            double ba = 0;
            double dis = 0;
            int num_col = 4;
            int num_corner = 0;
            List<int> waist = new List<int>();
            List<int> segment = new List<int>();
            List<int> type = new List<int>();
            List<int> segment_round = new List<int>();
            List<int> type_round = new List<int>();

            DA.GetData(0, ref surf1);
            DA.GetData(1, ref surf2);
            DA.GetDataList<double>(2, height);
            DA.GetData(3, ref ba);
            DA.GetData(4, ref dis);
            DA.GetData(5, ref num_col);
            DA.GetData(6, ref num_corner);
            DA.GetDataList<int>(7, waist);
            DA.GetDataList<int>(8, segment);
            DA.GetDataList<int>(9, type);
            DA.GetDataList<int>(10, segment_round);
            DA.GetDataList<int>(11, type_round);

            Grasshopper.DataTree<Line> column = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> beam1 = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> beam2 = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> wall = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> brace1 = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> brace2 = new Grasshopper.DataTree<Line>();


            num_col = num_col >= 4 ? num_col : 4;
            num_col = num_col % 2 == 1 ? ++num_col : num_col;
            num_corner = num_corner >= 0 ? num_corner : 0;

            double tolerance = 0.1;

            double pin_x_min, pin_x_max, pin_y_min, pin_y_max, pin_x_cen, pin_y_cen;
            List<List<Point3d>> pout_all = new List<List<Point3d>>();
            List<List<Line>> waist_hor = new List<List<Line>>();

            for (int floor = 0; floor < height.Count; floor++)
            {
                List<Point3d> pout = new List<Point3d>();
                Plane pla = new Plane(new Point3d(0, 0, height[floor] + ba), new Vector3d(0, 0, 1));
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf1, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesOut, out Point3d[] intersectionPointsOut);
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf2, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesIn, out Point3d[] intersectionPointsIn);
                if (intersectionCurvesOut is null | intersectionCurvesIn is null)
                {
                    break;
                }

                //找到核心筒的四个角点
                List<Curve> in_div = new List<Curve>();
                in_div.AddRange(intersectionCurvesIn[0].DuplicateSegments());
                pin_x_min = in_div[0].PointAtStart.X;
                pin_x_max = in_div[0].PointAtStart.X;
                pin_y_min = in_div[0].PointAtStart.Y;
                pin_y_max = in_div[0].PointAtStart.Y;
                for (int j = 0; j < in_div.Count; j++)
                {
                    pin_x_min = pin_x_min < in_div[j].PointAtStart.X ? pin_x_min : in_div[j].PointAtStart.X;
                    pin_x_max = pin_x_max > in_div[j].PointAtStart.X ? pin_x_max : in_div[j].PointAtStart.X;
                    pin_y_min = pin_y_min < in_div[j].PointAtStart.Y ? pin_y_min : in_div[j].PointAtStart.Y;
                    pin_y_max = pin_y_max > in_div[j].PointAtStart.Y ? pin_y_max : in_div[j].PointAtStart.Y;
                }
                pin_x_cen = (pin_x_min + pin_x_max) / 2;
                pin_y_cen = (pin_y_min + pin_y_max) / 2;

                //找到外立面的八个角点
                List<double> corn1 = new List<double>();
                List<double> corn2 = new List<double>();
                List<double> corn3 = new List<double>();
                List<double> corn4 = new List<double>();
                Rhino.Geometry.Intersect.CurveIntersections intersections;
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.Y >= pin_y_max)
                    {
                        corn1.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn4.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_max, pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.Y >= pin_y_max)
                    {
                        corn2.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn3.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_max, height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.X >= pin_x_max)
                    {
                        corn2.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn1.Add(intersections[i].ParameterA);
                    }
                }
                intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min, pin_y_min, height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                for (int i = 0; i < intersections.Count; i++)
                {
                    if (intersections[i].PointA.X >= pin_x_max)
                    {
                        corn3.Add(intersections[i].ParameterA);
                    }
                    else
                    {
                        corn4.Add(intersections[i].ParameterA);
                    }
                }
                for (int i = 0; i <= num_corner + 1; i++)
                {
                    pout.Add(intersectionCurvesOut[0].PointAt(corn1[0] + (corn1[1] - corn1[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn2[0] + (corn2[1] - corn2[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn3[0] + (corn3[1] - corn3[0]) / (num_corner + 1) * i));
                    pout.Add(intersectionCurvesOut[0].PointAt(corn4[0] + (corn4[1] - corn4[0]) / (num_corner + 1) * i));
                }
                int num_hor = (int)((num_col / 2 - 2) * (pin_x_max - pin_x_min) / (pin_x_max + pin_y_max - pin_x_min - pin_y_min));
                int num_ver = num_col / 2 - 2 - num_hor;

                for (int i = 1; i <= num_hor; i++)
                {
                    intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_min + i * (pin_x_max - pin_x_min) / (num_hor + 1), pin_y_max, height[floor] + ba), new Vector3d(0, 1, 0)), 1, 1);
                    pout.Add(intersections[0].PointA);
                    pout.Add(intersections[1].PointA);
                }
                for (int i = 1; i <= num_ver; i++)
                {
                    intersections = Rhino.Geometry.Intersect.Intersection.CurveLine(intersectionCurvesOut[0], new Line(new Point3d(pin_x_max, pin_y_min + i * (pin_y_max - pin_y_min) / (num_ver + 1), height[floor] + ba), new Vector3d(1, 0, 0)), 1, 1);
                    pout.Add(intersections[0].PointA);
                    pout.Add(intersections[1].PointA);
                }

                //墙
                int num_wall = 0;
                for (int i = 0; i <= num_ver + 1; i++)
                {
                    for (int j = 0; j <= num_hor; j++)
                    {
                        wall.Insert(new Line(new Point3d(pin_x_min + (double)j / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)(j + 1) / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)i / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)), new GH_Path(floor, num_wall++), 0);
                    }
                }
                for (int i = 0; i <= num_hor + 1; i++)
                {
                    for (int j = 0; j <= num_ver; j++)
                    {
                        wall.Insert(new Line(new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)j / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba), new Point3d(pin_x_min + (double)i / (num_hor + 1) * (pin_x_max - pin_x_min), pin_y_min + (double)(j + 1) / (num_ver + 1) * (pin_y_max - pin_y_min), height[floor] + ba)), new GH_Path(floor, num_wall++), 0);
                    }
                }
                //排序
                double theta1;
                double theta2;
                Point3d p_temp;
                for (int i = 0; i < pout.Count; i++)
                {
                    for (int j = 0; j < pout.Count - i - 1; j++)
                    {
                        theta1 = Math.Acos((pout[j].X - pin_x_cen) / Math.Sqrt(Math.Pow(pout[j].X - pin_x_cen, 2) + Math.Pow(pout[j].Y - pin_y_cen, 2)));
                        theta2 = Math.Acos((pout[j + 1].X - pin_x_cen) / Math.Sqrt(Math.Pow(pout[j + 1].X - pin_x_cen, 2) + Math.Pow(pout[j + 1].Y - pin_y_cen, 2)));
                        theta1 = pout[j].Y >= pin_y_cen - 0.1 ? theta1 : 2 * Math.PI - theta1;//0.1是宽容度
                        theta2 = pout[j + 1].Y >= pin_y_cen - 0.1 ? theta2 : 2 * Math.PI - theta2;
                        if (theta1 > theta2)
                        {
                            p_temp = pout[j];
                            pout[j] = pout[j + 1];
                            pout[j + 1] = p_temp;
                        }
                    }
                }
                pout_all.Add(pout);

                //外圈梁
                for (int i = 0; i < pout.Count - 1; i++)
                {
                    beam1.Insert(new Line(pout[i], pout[i + 1]), new GH_Path(floor, i), 0);
                }
                beam1.Insert(new Line(pout[0], pout[pout.Count - 1]), new GH_Path(floor, pout.Count - 1), 0);


                List<Line> temp_waist_hor = new List<Line>();

                //主梁
                //pin_x_min -= tolerance;
                //pin_x_max += tolerance;
                //pin_y_min -= tolerance;
                //pin_y_max += tolerance;
                for (int i = 0; i < pout.Count; i++)
                {
                    double pout_x = pout[i].X;
                    double pout_y = pout[i].Y;
                    double pout_z = pout[i].Z;
                    if ((Math.Abs(pout_x - pin_x_max)<tolerance || Math.Abs(pout_x - pin_x_min) < tolerance))
                    {
                        if(pout_y >= pin_y_max)
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)));
                        }
                        else
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)));
                        }                        
                    }
                    else if ((Math.Abs(pout_y - pin_y_max) < tolerance || Math.Abs(pout_y - pin_y_min) < tolerance))
                    {
                        if (pout_x >= pin_x_max)
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max,pout_y, pout_z)));
                        }
                        else
                        {
                            temp_waist_hor.Add(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)));
                        }
                    }





                    if (pout_x >= pin_x_min && pout_x <= pin_x_max + tolerance && pout_y >= pin_y_max - tolerance)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x <= pin_x_min && pout_y <= pin_y_max + tolerance && pout_y >= pin_y_min - tolerance)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x >= pin_x_min - tolerance && pout_x <= pin_x_max + tolerance && pout_y <= pin_y_min)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x >= pin_x_max && pout_y >= pin_y_min - tolerance && pout_y <= pin_y_max + tolerance)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)), new GH_Path(floor, i), 0);
                    }
                    else if (pout_x > pin_x_max && pout_y > pin_y_max)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x < pin_x_min && pout_y > pin_y_max)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_max, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x < pin_x_min && pout_y < pin_y_min)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }

                    else if (pout_x > pin_x_max && pout_y < pin_y_min)
                    {
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_min, pout_z)), new GH_Path(floor, i), 0);
                    }

                }
                waist_hor.Add(temp_waist_hor);


            }
            //柱
            for (int i = 0; i < pout_all.Count - 1; i++)
            {
                for (int j = 0; j < pout_all[0].Count; j++)
                {
                    column.Insert(new Line(pout_all[i][j], pout_all[i + 1][j]), new GH_Path(i, j), 0);
                }
            }
            //腰部桁架
            if (waist.Count > 0)
            {
                int temp_waist;
                int temp_segment = 0;
                int temp_type = 1;
                for (int i = 0; i < waist.Count; i++)
                {

                    temp_waist = waist[i];
                    if (temp_waist >= waist_hor.Count - 1)
                    {
                        break;
                    }

                    if (segment.Count > i)
                    {
                        temp_segment = segment[i];
                    }
                    else
                    {
                        temp_segment = (int)(waist_hor[temp_waist][0].Length / Math.Abs(waist_hor[temp_waist][0].FromZ - waist_hor[temp_waist + 1][0].FromZ));
                    }
                    if (type.Count > i)
                    {
                        temp_type = type[i];
                    }
                    for (int j = 0; j < waist_hor[temp_waist].Count; j++)
                    {
                        int num_brace = 0;
                        Line line_b = waist_hor[temp_waist][j];
                        Line line_u = waist_hor[temp_waist + 1][j];
                        List<Point3d> bot = new List<Point3d>();
                        List<Point3d> upp = new List<Point3d>();
                        if (temp_type == 1)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k - 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 2)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                brace1.Insert(new Line(bot[k + 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k - 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 3)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 4)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace1.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 5)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 6)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    brace1.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace1.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                    }
                }
            }

            //环带桁架
            if (waist.Count > 0)
            {
                int temp_waist;
                int temp_segment = 0;
                int temp_type = 1;
                for (int i = 0; i < waist.Count; i++)
                {
                    temp_waist = waist[i];
                    if (temp_waist >= waist_hor.Count - 1)
                    {
                        break;
                    }

                    if (segment_round.Count > i)
                    {
                        temp_segment = segment_round[i];
                    }
                    else
                    {
                        temp_segment = (int)(waist_hor[temp_waist][0].Length / Math.Abs(waist_hor[temp_waist][0].FromZ - waist_hor[temp_waist + 1][0].FromZ));
                    }
                    if (type_round.Count > i)
                    {
                        temp_type = type_round[i];
                    }

                    for (int j = 0; j < pout_all[0].Count; j++)
                    {
                        int num_brace = 0;
                        Line line_b = beam1[new GH_Path(temp_waist, j), 0];
                        Line line_u = beam1[new GH_Path(temp_waist + 1, j), 0];
                        List<Point3d> bot = new List<Point3d>();
                        List<Point3d> upp = new List<Point3d>();
                        if (temp_type == 1)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_u.FromX + (double)k / temp_segment * (line_u.ToX - line_u.FromX), line_u.FromY + (double)k / temp_segment * (line_u.ToY - line_u.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k - 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 2)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count / 2; k++)
                            {
                                brace2.Insert(new Line(bot[k + 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = bot.Count / 2; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k - 1], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                        }
                        if (temp_type == 3)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 4)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < temp_segment; k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / temp_segment * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / temp_segment * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / temp_segment * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / temp_segment * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count; k++)
                            {
                                brace2.Insert(new Line(bot[k], upp[k]), new GH_Path(temp_waist, j, num_brace++), 0);
                            }
                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 5)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 1)
                                {
                                    brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                        if (temp_type == 6)
                        {
                            bot.Add(line_b.From);
                            upp.Add(line_u.From);
                            for (int k = 1; k < (2 * temp_segment); k++)
                            {
                                bot.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_b.FromZ + (double)k / (2 * temp_segment) * (line_b.ToZ - line_b.FromZ)));
                                upp.Add(new Point3d(line_b.FromX + (double)k / (2 * temp_segment) * (line_b.ToX - line_b.FromX), line_b.FromY + (double)k / (2 * temp_segment) * (line_b.ToY - line_b.FromY), line_u.FromZ + (double)k / (2 * temp_segment) * (line_u.ToZ - line_u.FromZ)));
                            }
                            bot.Add(line_b.To);
                            upp.Add(line_u.To);

                            for (int k = 0; k < bot.Count - 1; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    brace2.Insert(new Line(bot[k], upp[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                                else
                                {
                                    brace2.Insert(new Line(upp[k], bot[k + 1]), new GH_Path(temp_waist, j, num_brace++), 0);
                                }
                            }
                        }
                    }
                }
            }


            DA.SetDataTree(0, column);
            DA.SetDataTree(1, beam1);
            DA.SetDataTree(2, beam2);
            DA.SetDataTree(3, wall);
            DA.SetDataTree(4, brace1);
            DA.SetDataTree(5, brace2);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e5556cf4-9b4a-4f88-9bb3-ca9bc0ce3984"); }
        }
    }
}
