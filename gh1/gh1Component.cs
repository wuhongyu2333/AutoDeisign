using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;


// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace gh1
{
    public class gh1Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public gh1Component()
          : base("truss", "TRUSS",
              "generate 2-line truss",
              "myGH", "2L")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddLineParameter("Line1", "L1", "Please select Line1", GH_ParamAccess.item);
            pManager.AddLineParameter("Line2", "L2", "Please select Line2", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Segments", "SG", "Please select number of segments", GH_ParamAccess.item);
            pManager.AddIntegerParameter("type", "TP", "Please select a type", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("point", "PT", "This is a point", GH_ParamAccess.item);
            pManager.AddLineParameter("line", "L","This is a line", GH_ParamAccess.item);
            
        }
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int Segment = 0;
            int type = 0;
            Line l1 = Line.Unset;
            Line l2 = Line.Unset;
            bool res1 = DA.GetData(0, ref l1);
            bool res2 = DA.GetData(1, ref l2);
            bool res3 = DA.GetData(2, ref Segment);
            bool res4 = DA.GetData(3, ref type);
            if (!res1 | !res2 | !res3 | !res4) { return; }
            float x0 = ((float)l1.FromX);
            float y0 = ((float)l1.FromY);
            float z0 = ((float)l1.FromZ);
            float x1 = ((float)l1.ToX);
            float y1 = ((float)l1.ToY);
            float z1 = ((float)l1.ToZ);
            float x2 = ((float)l2.FromX);
            float y2 = ((float)l2.FromY);
            float z2 = ((float)l2.FromZ);
            float x3 = ((float)l2.ToX);
            float y3 = ((float)l2.ToY);
            float z3 = ((float)l2.ToZ);
            Point3d[] pt = new Point3d[2 * Segment + 2];
            pt[0] = new Point3d(x0, y0, z0);
            pt[1] = new Point3d(x1, y1, z1);
            pt[2 * Segment] = new Point3d(x2, y2, z2);
            pt[2 * Segment + 1] = new Point3d(x3, y3, z3);
            for (int i = 1; i < Segment; i++)
            {
                pt[2 * i] = new Point3d(x0 + (x2 - x0) * i / Segment, y0 + (y2 - y0) * i / Segment, z0 + (z2 - z0) * i / Segment);
                pt[2 * i + 1] = new Point3d(x1 + (x3 - x1) * i / Segment, y1 + (y3 - y1) * i / Segment, z1 + (z3 - z1) * i / Segment);
            }
            DA.SetDataList(0, pt);
            Line[] l_a = new Line[5 * Segment + 1];
            l_a[0] = l1;
            l_a[1] = l2;
            for (int i = 0; i < Segment; i++)
            {
                l_a[i + 2] = new Line(pt[2 * i], pt[2 * i + 2]);
            }
            for (int i = 0; i < Segment; i++)
            {
                l_a[i + 2 + Segment] = new Line(pt[2 * i + 1], pt[2 * i + 3]);
            }
            if (type == 1 | type == 2 | type == 5 | type == 6)//竖杆
            {
                for (int i = 0; i < Segment - 1; i++)
                {
                    l_a[i + 2 * Segment + 2] = new Line(pt[2 * i + 2], pt[2 * i + 3]);
                }
            }
            if (type == 1 | type == 3 | type == 5 | type == 7)//左斜杆
            {
                for (int i = 0; i < Segment; i++)
                {
                    l_a[i + 3 * Segment + 1] = new Line(pt[2 * i + 1], pt[2 * i + 2]);
                }
            }
            if (type == 1 | type == 4 | type == 6 | type == 7)//右斜杆
            {
                for (int i = 0; i < Segment; i++)
                {
                    l_a[i + 4 * Segment + 1] = new Line(pt[2 * i], pt[2 * i + 3]);
                }
            }
            if (type == 9)
            {
                int[] temp = new int[Segment+1];
                for(int i = 0; i < Segment + 1; i++)
                {
                    if (i % 2 == 1)
                    {
                        temp[i] = 2 * i;
                    }
                    else
                    {
                        temp[i] = 2 * i + 1;
                    }
                }
                for (int i = 0; i < Segment; i++)
                {
                    l_a[i + 2 + 2 * Segment] = new Line(pt[temp[i]], pt[temp[i + 1]]);
                }
            }
            if (type == 10)
            {
                int[] temp = new int[Segment + 1];
                for (int i = 0; i < Segment + 1; i++)
                {
                    if (i % 2 == 1)
                    {
                        temp[i] = 2 * i + 1;
                    }
                    else
                    {
                        temp[i] = 2 * i;
                    }
                }
                for (int i = 0; i < Segment; i++)
                {
                    l_a[i + 2 + 2 * Segment] = new Line(pt[temp[i]], pt[temp[i + 1]]);
                }
            }
            DA.SetDataList(1, l_a);
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
            get { return new Guid("27cac153-4aa9-486a-957a-f65b61dc9536"); }
        }
    }
}
