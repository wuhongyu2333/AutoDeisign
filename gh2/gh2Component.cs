using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace gh2
{
    public class gh2Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public gh2Component()
          : base("truss", "TRUSS",
              "generate 2-node truss",
              "myGH", "2N")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point1", "P1", "Please select Point1", GH_ParamAccess.item);
            pManager.AddPointParameter("Point2", "P2", "Please select Point2", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "Please input height value", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Type", "T", "Please input type of truss generation", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Direction", "Direct", "Please input direction", GH_ParamAccess.item, true);
            pManager.AddIntegerParameter("Segment", "Seg", "Please input number of segments", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Test", "T", "This is a test item", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "This is a point list", GH_ParamAccess.item);
            pManager.AddLineParameter("Line", "L", "This is a line list", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d p1 = Point3d.Unset;
            Point3d p2 = Point3d.Unset;
            double h = 0;
            int type = 0;
            bool res1 = DA.GetData(0, ref p1);
            bool res2 = DA.GetData(1, ref p2);
            bool res3 = DA.GetData(2, ref h);
            bool res4 = DA.GetData(3, ref type);//type为1，2，3，4，5，6
            if (!res1 | !res2 | !res3 | !res4) { return; }

            Boolean direct = true;
            int segment = (int)(p1.DistanceTo(p2) / h) + 1;
            int default_Seg = 1;
            DA.GetData(4, ref direct);
            DA.GetData(5, ref default_Seg);
            if (default_Seg != 1)
            {
                segment = default_Seg;
            }
            DA.SetData(0, segment);

            //生成桁架算法
            float x0 = ((float)p1.X);
            float y0 = ((float)p1.Y);
            float z0 = ((float)p1.Z);
            float x1 = ((float)p2.X);
            float y1 = ((float)p2.Y);
            float z1 = ((float)p2.Z);
            if (type <= 4)
            {
                List<Point3d> pt_ls = new List<Point3d>();
                for (int i = 0; i < segment + 1; i++)
                {
                    float x = x0 + (x1 - x0) / segment * i;
                    float y = y0 + (y1 - y0) / segment * i;
                    float z = z0 + (z1 - z0) / segment * i;
                    pt_ls.Add(new Point3d(x, y, z));
                    if (direct)
                    {
                        pt_ls.Add(new Point3d(x, y, z + h));
                    }
                    else
                    {
                        pt_ls.Add(new Point3d(x, y, z - h));
                    }

                }
                List<Line> l_ls = new List<Line>();
                for (int i = 0; i < segment; i++)//下弦横杆
                {
                    l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 2]));
                }
                for (int i = 0; i < segment; i++)//上弦横杆
                {
                    l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i + 3]));
                }
                for (int i = 0; i < segment + 1; i++)//竖杆
                {
                    l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 1]));
                }
                if (type == 1)
                {
                    int half = (int)(segment / 2);
                    for (int i = 0; i < half; i++)//右斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 3]));
                    }
                    for (int i = half + 1; i < segment + 1; i++)//左斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i - 1]));
                    }
                }
                if (type == 2)
                {
                    int half = (int)(segment / 2);
                    for (int i = 0; i < half; i++)//左斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i + 2]));
                    }
                    for (int i = half; i < segment; i++)//左斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 3]));
                    }
                }
                if (type == 3)
                {
                    int[] n_ls = new int[segment + 1];
                    for (int i = 0; i < segment + 1; i++)
                    {
                        if (i % 2 == 0)
                        {
                            n_ls[i] = 2 * i;
                        }
                        else
                        {
                            n_ls[i] = 2 * i + 1;
                        }
                    }
                    for (int i = 0; i < segment; i++)//type3
                    {
                        l_ls.Add(new Line(pt_ls[n_ls[i]], pt_ls[n_ls[i + 1]]));
                    }
                }
                if (type == 4)
                {
                    int[] n_ls = new int[segment + 1];
                    for (int i = 0; i < segment + 1; i++)
                    {
                        if (i % 2 == 0)
                        {
                            n_ls[i] = 2 * i + 1;
                        }
                        else
                        {
                            n_ls[i] = 2 * i;
                        }
                    }
                    for (int i = 0; i < segment; i++)//type4
                    {
                        l_ls.Add(new Line(pt_ls[n_ls[i]], pt_ls[n_ls[i + 1]]));
                    }
                }
                DA.SetDataList(1, pt_ls);
                DA.SetDataList(2, l_ls);
            }
            else
            {
                List<Point3d> pt_ls = new List<Point3d>();
                for (int i = 0; i < segment + 1; i++)
                {
                    float x = x0 + (x1 - x0) / segment * i;
                    float y = y0 + (y1 - y0) / segment * i;
                    float z = z0 + (z1 - z0) / segment * i;
                    pt_ls.Add(new Point3d(x, y, z));
                    if (direct)
                    {
                        pt_ls.Add(new Point3d(x, y, z + h));
                    }
                    else
                    {
                        pt_ls.Add(new Point3d(x, y, z - h));
                    }
                }
                if (type == 5)
                {
                    pt_ls.Add(new Point3d(x1, y1, z1));
                    for (int i = 1; i < segment + 1; i++)
                    {
                        pt_ls[2 * i] = new Point3d(pt_ls[2 * i].X - (x1 - x0) / (segment * 2), pt_ls[2 * i].Y - (y1 - y0) / (segment * 2), pt_ls[2 * i].Z - (z1 - z0) / (segment * 2));
                    }
                    List<Line> l_ls = new List<Line>();
                    for (int i = 0; i < segment + 1; i++)//下弦横杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 2]));
                    }
                    for (int i = 0; i < segment; i++)//上弦横杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i + 3]));
                    }
                    for (int i = 0; i < segment + 1; i++)//斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i]));
                        l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i + 2]));
                    }
                    DA.SetDataList(1, pt_ls);
                    DA.SetDataList(2, l_ls);
                }
                if (type == 6)
                {
                    if (direct)
                    {
                        pt_ls.Add(new Point3d(x1, y1, z1 + h));
                    }
                    else
                    {
                        pt_ls.Add(new Point3d(x1, y1, z1 - h));
                    }
                    for (int i = 1; i < segment + 1; i++)
                    {
                        pt_ls[2 * i + 1] = new Point3d(pt_ls[2 * i + 1].X - (x1 - x0) / (segment * 2), pt_ls[2 * i + 1].Y - (y1 - y0) / (segment * 2), pt_ls[2 * i + 1].Z - (z1 - z0) / (segment * 2));
                    }
                    List<Line> l_ls = new List<Line>();
                    for (int i = 0; i < segment; i++)//下弦横杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 2]));
                    }
                    for (int i = 0; i < segment; i++)//上弦横杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i + 1], pt_ls[2 * i + 3]));
                    }
                    l_ls.Add(new Line(pt_ls[2 * segment + 1], pt_ls[2 * segment + 2]));
                    for (int i = 0; i < segment; i++)//斜杆
                    {
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 1]));
                        l_ls.Add(new Line(pt_ls[2 * i], pt_ls[2 * i + 3]));
                    }
                    l_ls.Add(new Line(pt_ls[2 * segment], pt_ls[2 * segment + 1]));
                    l_ls.Add(new Line(pt_ls[2 * segment], pt_ls[2 * segment + 2]));
                    DA.SetDataList(1, pt_ls);
                    DA.SetDataList(2, l_ls);
                }



            }

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
            get { return new Guid("bf58996e-818a-4f9e-be54-427cfde1f215"); }
        }
    }
}
