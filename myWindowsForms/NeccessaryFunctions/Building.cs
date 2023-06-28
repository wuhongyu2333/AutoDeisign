using APIFrameworkRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace myWindowsForms.NeccessaryFunctions
{
    public class Building
    {
        private int _floorNum; // 总楼层高度
        private List<int> _levels; // 标高列表
        private List<int> _floorHeight;
        private List<ConLevel> _colConLevelList;
        private List<ConLevel> _beamConLevelList;
        private List<ConLevel> _wallConLevelList;
        private List<ConLevel> _slabConLevelList;
        private List<Section> _colSectionList;
        private List<int> _wallSectionList;

        private List<ColumnSection> ColumnSectionList;

        private ProjectLocation _location;
        public Building()
        {
            this.SetLevels(new List<int>() { 0, 4000, 8000, 12000 });
            this.SetColConLevel(ConLevel.C60);
            this.SetBeamConLevel(ConLevel.C35);
            this.SetWallConLevel(ConLevel.C60);
            this.SetSlabConLevel(ConLevel.C35);
            this.SetColSection(new Section(1000, 1000));
            this.SetWallSection(600);
            this.SetProjectLocation("上海", "上海市", "黄浦区");
        }

        public void SetLevels(List<int> levels)
        {
            if (levels != null && levels.Count != 0)
            {
                levels.Sort();
                this._levels = levels;
                this._floorNum = this._levels.Count - 1;
                List<int> floorHeight = new List<int>();
                int lastLevel = this._levels[0];
                for (int i = 1; i < this._levels.Count; i++)
                {
                    floorHeight.Add(this._levels[i] - lastLevel);
                    lastLevel = this._levels[i];
                }
                this._floorHeight = floorHeight;
                if (this.ColConLevelList != null)
                {
                    this.SetColConLevel(this.ColConLevelList);
                    this.SetBeamConLevel(this.BeamConLevelList);
                    this.SetWallConLevel(this.WallConLevelList);
                    this.SetSlabConLevel(this.SlabConLevelList);
                    this.SetColSection(this.ColSectionList);
                    this.SetWallSection(this.WallSectionList);

                }
            }
        }
        public void SetProjectLocation(string provence, string city, string district)
        {
            this._location = new ProjectLocation(provence, city, district);
        }
        public void SetColConLevel(ConLevel conLevel)
        {
            List<ConLevel> colConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                colConLevelList.Add(conLevel);
            }
            this._colConLevelList = colConLevelList;
        }
        public void SetColConLevel(ConLevel conLevel, int floor)
        {
            List<ConLevel> colConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    colConLevelList.Add(this._colConLevelList[i]);
                }
                else
                {
                    colConLevelList.Add(conLevel);
                }

            }
            this._colConLevelList = colConLevelList;
        }
        public void SetColConLevel(List<ConLevel> conLevelList)
        {
            List<ConLevel> colConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                colConLevelList.Add(conLevelList[Math.Min(i, conLevelList.Count - 1)]);
            }
            this._colConLevelList = colConLevelList;
        }
        public void SetBeamConLevel(ConLevel conLevel)
        {
            List<ConLevel> beamConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                beamConLevelList.Add(conLevel);
            }
            this._beamConLevelList = beamConLevelList;
        }
        public void SetBeamConLevel(ConLevel conLevel, int floor)
        {
            List<ConLevel> beamConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    beamConLevelList.Add(this._beamConLevelList[i]);
                }
                else
                {
                    beamConLevelList.Add(conLevel);
                }

            }
            this._beamConLevelList = beamConLevelList;
        }
        public void SetBeamConLevel(List<ConLevel> conLevelList)
        {
            List<ConLevel> beamConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                beamConLevelList.Add(conLevelList[Math.Min(i, conLevelList.Count - 1)]);
            }
            this._beamConLevelList = beamConLevelList;
        }
        public void SetSlabConLevel(ConLevel conLevel)
        {
            List<ConLevel> slabConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                slabConLevelList.Add(conLevel);
            }
            this._slabConLevelList = slabConLevelList;
        }
        public void SetSlabConLevel(ConLevel conLevel, int floor)
        {
            List<ConLevel> slabConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    slabConLevelList.Add(this._slabConLevelList[i]);
                }
                else
                {
                    slabConLevelList.Add(conLevel);
                }

            }
            this._slabConLevelList = slabConLevelList;
        }
        public void SetSlabConLevel(List<ConLevel> conLevelList)
        {
            List<ConLevel> slabConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                slabConLevelList.Add(conLevelList[Math.Min(i, conLevelList.Count - 1)]);
            }
            this._slabConLevelList = slabConLevelList;
        }
        public void SetWallConLevel(ConLevel conLevel)
        {
            List<ConLevel> wallConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                wallConLevelList.Add(conLevel);
            }
            this._wallConLevelList = wallConLevelList;
        }
        public void SetWallConLevel(ConLevel conLevel, int floor)
        {
            List<ConLevel> wallabConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    wallabConLevelList.Add(this._wallConLevelList[i]);
                }
                else
                {
                    wallabConLevelList.Add(conLevel);
                }

            }
            this._wallConLevelList = wallabConLevelList;
        }
        public void SetWallConLevel(List<ConLevel> conLevelList)
        {
            List<ConLevel> wallConLevelList = new List<ConLevel>();
            for (int i = 0; i < this._floorNum; i++)
            {
                wallConLevelList.Add(conLevelList[Math.Min(i, conLevelList.Count - 1)]);
            }
            this._wallConLevelList = wallConLevelList;
        }

        public void SetColSection(Section section)
        {
            List<Section> colSectionList = new List<Section>();
            for (int i = 0; i < this._floorNum; i++)
            {
                colSectionList.Add(section);
            }
            this._colSectionList = colSectionList;
        }
        public void SetColSection(Section section, int floor)
        {
            List<Section> colSectionList = new List<Section>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    colSectionList.Add(this._colSectionList[i]);
                }
                else
                {
                    colSectionList.Add(section);
                }
            }
            this._colSectionList = colSectionList;
        }
        public void SetColSection(List<Section> sectionList)
        {
            List<Section> colSectionList = new List<Section>();
            for (int i = 0; i < this._floorNum; i++)
            {
                colSectionList.Add(sectionList[Math.Min(i, sectionList.Count - 1)]);
            }
            this._colSectionList = colSectionList;
        }

        public void SetWallSection(int wallSection)
        {
            List<int> wallSectionList = new List<int>();
            for (int i = 0; i < this._floorNum; i++)
            {
                wallSectionList.Add(wallSection);
            }
            this._wallSectionList = wallSectionList;
        }
        public void SetWallSection(int wallSection, int floor)
        {
            List<int> wallSectionList = new List<int>();
            for (int i = 0; i < this._floorNum; i++)
            {
                if (i < floor)
                {
                    wallSectionList.Add(this._wallSectionList[i]);
                }
                else
                {
                    wallSectionList.Add(wallSection);
                }
            }
            this._wallSectionList = wallSectionList;
        }
        public void SetWallSection(List<int> wallSectionList)
        {
            List<int> wallSectionList2 = new List<int>();
            for (int i = 0; i < this._floorNum; i++)
            {
                wallSectionList2.Add(wallSectionList[Math.Min(i, wallSectionList.Count - 1)]);
            }
            this._wallSectionList = wallSectionList2;
        }

        public int GetLevel(int floor)
        {
            if (floor >= 0 && floor <= this._floorNum)
            {
                return this._levels[floor];
            }
            else
            {
                return -1;
            }
        }
        public int TopLevel { get { return this.GetLevel(this.FloorNum); } }
        public List<int> Levels { get { return this._levels; } }
        public List<int> FloorHeigths { get { return this._floorHeight; } }
        public int FloorNum { get { return this._floorNum; } }
        public ProjectLocation ProjectLocation { get { return this._location; } }
        public List<ConLevel> ColConLevelList { get { return this._colConLevelList; } }
        public List<ConLevel> BeamConLevelList { get { return this._beamConLevelList; } }
        public List<ConLevel> WallConLevelList { get { return this._wallConLevelList; } }
        public List<ConLevel> SlabConLevelList { get { return this._slabConLevelList; } }
        public List<Section> ColSectionList { get { return this._colSectionList; } }
        public List<int> WallSectionList { get { return this._wallSectionList; } }

    }

}
