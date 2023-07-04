using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace gh3
{
    public class gh3Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public gh3Component()
          : base("curve_truss", "C_T",
              "generate 2-node truss",
              "myGH", "C_T")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "C", "Please select a curve", GH_ParamAccess.item);
            pManager.AddNumberParameter("OffSet", "D", "Please input offset value", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Type", "T", "Please input type of truss generation", GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vec", "Please select a unit vector to offset", GH_ParamAccess.item, new Vector3d(0, 0, 1));
            pManager.AddIntegerParameter("Segment", "Seg", "Please input number of segments", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {   
            pManager.AddPointParameter("Point", "P", "This is a point list", GH_ParamAccess.item);
            pManager.AddLineParameter("Line", "L", "This is a line list", GH_ParamAccess.item);
            pManager.AddNumberParameter("Test", "T", "This is a test item", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve_point", "cp", "This is a point of curve", GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vec", "Please select a unit vector to offset", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve cur = null;
            double h = 0;
            int type = 0;
            bool res1 = DA.GetData(0, ref cur);
            bool res2 = DA.GetData(1, ref h);
            bool res3 = DA.GetData(2, ref type);//type为1，2，3，4，5，6
            if (!res1 | !res2 | !res3) { return; }

            Vector3d direct = new Vector3d(0, 0, 1);
            int segment = (int)(cur.GetLength() / h) + 1;
            int default_Seg = 1;
            DA.GetData(3, ref direct);
            DA.GetData(4, ref default_Seg);
            if (default_Seg != 1)
            {
                segment = default_Seg;
            }
            //新建输出的点集和杆件集
            List<Point3d> pt = new List<Point3d>();
            List<Line> ele = new List<Line>();

            
            if (type <= 4)
            {
                double[] cur_div = cur.DivideByCount(segment, true);
                pt.Add(cur.PointAt(0));
                pt.Add(new Point3d(cur.PointAt(0).X + direct.X * h, cur.PointAt(0).Y + direct.Y * h, cur.PointAt(0).Z + direct.Z * h));
                for (int i = 0; i < segment; i++)
                {
                    Point3d p = cur.PointAt(cur_div[i + 1]);
                    pt.Add(p);
                    pt.Add(new Point3d(p.X + direct.X * h, p.Y + direct.Y * h, p.Z + direct.Z * h));
                }
                DA.SetDataList(0, pt);
                for (int i = 0; i < segment; i++)//下弦横杆
                {
                    ele.Add(new Line(pt[2 * i], pt[2 * i + 2]));
                }
                for (int i = 0; i < segment; i++)//上弦横杆
                {
                    ele.Add(new Line(pt[2 * i + 1], pt[2 * i + 3]));
                }
                for (int i = 0; i < segment + 1; i++)//竖杆
                {
                    ele.Add(new Line(pt[2 * i], pt[2 * i + 1]));
                }
                if (type == 1)
                {
                    int half = (int)(segment / 2);
                    for (int i = 0; i < half; i++)//右斜杆
                    {
                        ele.Add(new Line(pt[2 * i], pt[2 * i + 3]));
                    }
                    for (int i = half + 1; i < segment + 1; i++)//左斜杆
                    {
                        ele.Add(new Line(pt[2 * i], pt[2 * i - 1]));
                    }
                }
                if (type == 2)
                {
                    int half = (int)(segment / 2);
                    for (int i = 0; i < half; i++)//左斜杆
                    {
                        ele.Add(new Line(pt[2 * i + 1], pt[2 * i + 2]));
                    }
                    for (int i = half; i < segment; i++)//左斜杆
                    {
                        ele.Add(new Line(pt[2 * i], pt[2 * i + 3]));
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
                        ele.Add(new Line(pt[n_ls[i]], pt[n_ls[i + 1]]));
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
                        ele.Add(new Line(pt[n_ls[i]], pt[n_ls[i + 1]]));
                    }
                }
                // 设置输出参数
                DA.SetDataList(1, ele);
                DA.SetData(4, direct);
            }
            if (type == 5)
            {
                double[] cur_div = cur.DivideByCount(2*segment, true);
                pt.Add(cur.PointAt(0));
                pt.Add(new Point3d(cur.PointAt(0).X + direct.X * h, cur.PointAt(0).Y + direct.Y * h, cur.PointAt(0).Z + direct.Z * h));
                for (int i = 0; i < 2*segment; i++)
                {
                    Point3d p = cur.PointAt(cur_div[i + 1]);
                    if (i % 2 == 0)
                    {
                        pt.Add(p);
                    }
                    else
                    {
                        pt.Add(new Point3d(p.X + direct.X * h, p.Y + direct.Y * h, p.Z + direct.Z * h));
                    }
                    
                }
                pt.Add(cur.PointAtEnd);
                DA.SetDataList(0, pt);

                for (int i = 0; i < segment+1; i++)//下弦横杆
                {
                    ele.Add(new Line(pt[2 * i], pt[2 * i + 2]));
                }
                for (int i = 0; i < segment; i++)//上弦横杆
                {
                    ele.Add(new Line(pt[2 * i + 1], pt[2 * i + 3]));
                }
                for (int i = 0; i < pt.Count-1; i++)//竖杆
                {
                    ele.Add(new Line(pt[i], pt[i + 1]));
                }
                DA.SetDataList(1, ele);
            }
            if (type == 6)
            {
                double[] cur_div = cur.DivideByCount(2 * segment, true);
                
                pt.Add(new Point3d(cur.PointAt(0).X + direct.X * h, cur.PointAt(0).Y + direct.Y * h, cur.PointAt(0).Z + direct.Z * h));
                pt.Add(cur.PointAt(0));
                for (int i = 0; i < 2 * segment; i++)
                {
                    Point3d p = cur.PointAt(cur_div[i + 1]);
                    if (i % 2 == 0)
                    {
                        pt.Add(new Point3d(p.X + direct.X * h, p.Y + direct.Y * h, p.Z + direct.Z * h));
                    }
                    else
                    {
                        pt.Add(p);
                    }
                }
                pt.Add(new Point3d(cur.PointAtEnd.X + direct.X * h, cur.PointAtEnd.Y + direct.Y * h, cur.PointAtEnd.Z + direct.Z * h));
                DA.SetDataList(0, pt);

                for (int i = 0; i < segment ; i++)//下弦横杆
                {
                    ele.Add(new Line(pt[2 * i+1], pt[2 * i + 3]));
                }
                for (int i = 0; i < segment+1; i++)//上弦横杆
                {
                    ele.Add(new Line(pt[2 * i], pt[2 * i + 2]));
                }
                ele.Add(new Line(pt[2 * segment + 1], pt[2 * segment + 2]));
                for (int i = 0; i < pt.Count - 1; i++)//竖杆
                {
                    ele.Add(new Line(pt[i], pt[i + 1]));
                }
                DA.SetDataList(1, ele);
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
            get { return new Guid("16415381-4dba-4d2a-8682-d61f46f05e6c"); }
        }
    }
}
