using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace gh5
{
    public class gh5Component : GH_Component
    {
        
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public gh5Component()
          : base("Auto_Design_Software", "ADS",
              "Auto_Design_Software",
              "ADS", "type1")
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
            pManager.AddIntegerParameter("柱数", "NC", "请输入每层柱数", GH_ParamAccess.item, 4);
            pManager.AddNumberParameter("核心筒墙间距", "Space", "请输入核心筒墙间距", GH_ParamAccess.item);
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
            double angle = 0;
            double space = 0;
            DA.GetData(0, ref surf1);
            DA.GetData(1, ref surf2);
            DA.GetDataList<double>(2, height);
            DA.GetData(3, ref ba);
            DA.GetData(4, ref dis);
            DA.GetData(5, ref num_col);
            DA.GetData(6, ref space);
            //DA.GetData(6, ref angle);

            //新建内外立面的交线和交点
            List<Curve> CurveOut = new List<Curve>();
            List<Curve> CurveIn = new List<Curve>();
            List<Point3d> pout = new List<Point3d>();
            List<Point3d> pin = new List<Point3d>();

            //新建梁柱墙
            //List<Line> colunm = new List<Line>();
            //List<Line> beam1 = new List<Line>();
            //List<Line> beam2 = new List<Line>();
            //List<Line> wall = new List<Line>();

            //Grasshopper.DataTree<Line> standFloor = new Grasshopper.DataTree<Line>();//树结构为标准层-构件类型-构件编号
            Grasshopper.DataTree<Line> column = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> beam1 = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> beam2 = new Grasshopper.DataTree<Line>();
            Grasshopper.DataTree<Line> wall = new Grasshopper.DataTree<Line>();



            double pin_x_min = 0;
            double pin_x_max = 0;
            double pin_y_min = 0;
            double pin_y_max = 0;


            for (int floor = 0; floor < height.Count; floor++)
            {
                Plane pla = new Plane(new Point3d(0, 0, height[floor] + ba), new Vector3d(0, 0, 1));

                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf1, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesOut, out Point3d[] intersectionPointsOut);
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf2, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesIn, out Point3d[] intersectionPointsIn);

                if (intersectionCurvesOut is null | intersectionCurvesIn is null)
                {
                    break;
                }
                else
                {
                    CurveOut.Add(intersectionCurvesOut[0]);
                    CurveIn.Add(intersectionCurvesIn[0]);
                }
                double[] out_div = intersectionCurvesOut[0].DivideByCount(num_col, true);
                Point3d p_temp = Point3d.Unset;
                for (int j = 0; j < out_div.Length; j++)
                {
                    p_temp = intersectionCurvesOut[0].PointAt((out_div[j] + intersectionCurvesOut[0].GetLength() * angle / 360) % intersectionCurvesOut[0].GetLength());
                    pout.Add(p_temp);
                }
                List<Curve> in_div = new List<Curve>();
                in_div.AddRange(intersectionCurvesIn[0].DuplicateSegments());
                pin_x_min = in_div[0].PointAtStart.X;
                pin_x_max = in_div[0].PointAtStart.X;
                pin_y_min = in_div[0].PointAtStart.Y;
                pin_y_max = in_div[0].PointAtStart.Y;
                for (int j = 0; j < in_div.Count; j++)
                {
                    p_temp = in_div[j].PointAtStart;
                    pin.Add(p_temp);
                    pin_x_min = pin_x_min < p_temp.X ? pin_x_min : p_temp.X;
                    pin_x_max = pin_x_max > p_temp.X ? pin_x_max : p_temp.X;
                    pin_y_min = pin_y_min < p_temp.Y ? pin_y_min : p_temp.Y;
                    pin_y_max = pin_y_max > p_temp.Y ? pin_y_max : p_temp.Y;
                }
            }

            //柱
            for (int i = 0; i < pout.Count / num_col - 1; i++)
            {
                for (int j = 0; j < num_col; j++)
                {
                    //standFloor.Insert(new Line(pout[j + i * num_col], pout[j + (i + 1) * num_col]), new GH_Path(i, 0, j), 0);

                    column.Insert(new Line(pout[j + i * num_col], pout[j + (i + 1) * num_col]), new GH_Path(i,j), 0);
                }

            }
            //梁
            for (int i = 1; i < pout.Count / num_col; i++)//外圈梁
            {
                for (int j = 0; j < num_col-1; j++)
                {
                    //standFloor.Insert(new Line(pout[j + i * num_col], pout[j + i * num_col + 1]), new GH_Path(i - 1, 1, j), 0);
                    beam1.Insert(new Line(pout[j + i * num_col], pout[j + i * num_col + 1]), new GH_Path(i - 1, j), 0);
                }
                //standFloor.Insert(new Line(pout[i * num_col], pout[(i + 1) * num_col - 1]), new GH_Path(i - 1, 1, num_col - 1), 0);
                beam1.Insert(new Line(pout[i * num_col], pout[(i + 1) * num_col - 1]), new GH_Path(i - 1, num_col - 1), 0);
            }
            for (int i = 1; i < pout.Count / num_col; i++)//主梁
            {
                for (int j = 0; j < num_col; j++)
                {
                    double pout_x = pout[j + i * num_col].X;
                    double pout_y = pout[j + i * num_col].Y;
                    double pout_z = pout[j + i * num_col].Z;
                    if (pout_x >= pin_x_max && pout_y >= pin_y_max)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_max, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_max, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x > pin_x_min && pout_x < pin_x_max && pout_y > pin_y_max)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_max, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x <= pin_x_min && pout_y >= pin_y_max)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_max, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_max, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x < pin_x_min && pout_y < pin_y_max && pout_y > pin_y_min)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pout_y, pout_z)), new GH_Path(i - 1,  j), 0);
                    }
                    else if (pout_x <= pin_x_min && pout_y <= pin_y_min)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_min, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_min, pin_y_min, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x > pin_x_min && pout_x < pin_x_max && pout_y < pin_y_min)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pout_x, pin_y_min, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x >= pin_x_max && pout_y <= pin_y_min)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_min, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pin_y_min, pout_z)), new GH_Path(i - 1, j), 0);
                    }
                    else if (pout_x > pin_x_max && pout_y > pin_y_min && pout_y < pin_y_max)
                    {
                        //standFloor.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)), new GH_Path(i - 1, 2, j), 0);
                        beam2.Insert(new Line(new Point3d(pout_x, pout_y, pout_z), new Point3d(pin_x_max, pout_y, pout_z)), new GH_Path(i - 1,  j), 0);
                    }

                }
            }
            //墙
            for (int i = 0; i < pout.Count / num_col-1; i++)
            {
                double cur_h = pin[4*i].Z;
                wall.Insert(new Line(new Point3d(pin_x_min, pin_y_min, cur_h), new Point3d(pin_x_max, pin_y_min, cur_h)), new GH_Path(i, 0), 0);
                wall.Insert(new Line(new Point3d(pin_x_max, pin_y_min, cur_h), new Point3d(pin_x_max, pin_y_max, cur_h)), new GH_Path(i, 1), 0);
                wall.Insert(new Line(new Point3d(pin_x_max, pin_y_max, cur_h), new Point3d(pin_x_min, pin_y_max, cur_h)), new GH_Path(i, 2), 0);
                wall.Insert(new Line(new Point3d(pin_x_min, pin_y_max, cur_h), new Point3d(pin_x_min, pin_y_min, cur_h)), new GH_Path(i, 3), 0);
                //standFloor.Insert(new Line(new Point3d(pin_x_min, pin_y_min, cur_h), new Point3d(pin_x_max, pin_y_min, cur_h)), new GH_Path(i, 3, 0), 0);
                //standFloor.Insert(new Line(new Point3d(pin_x_max, pin_y_min, cur_h), new Point3d(pin_x_max, pin_y_max, cur_h)), new GH_Path(i, 3, 1), 0);
                //standFloor.Insert(new Line(new Point3d(pin_x_max, pin_y_max, cur_h), new Point3d(pin_x_min, pin_y_max, cur_h)), new GH_Path(i, 3, 2), 0);
                //standFloor.Insert(new Line(new Point3d(pin_x_min, pin_y_max, cur_h), new Point3d(pin_x_min, pin_y_min, cur_h)), new GH_Path(i, 3, 3), 0);

                for (int j = 0; j < (int)((pin_y_max - pin_y_min) / space); j++)
                {
                    int sp = (int)((pin_y_max - pin_y_min) / space) + 1;
                    double test= (double)(j + 1) / sp;
                    double ytemp = pin_y_min + (j + 1) / sp * (pin_y_max - pin_y_min);
                    wall.Insert(new Line(new Point3d(pin_x_min, pin_y_min + ((double)(j + 1) / sp) * (pin_y_max - pin_y_min), cur_h), new Point3d(pin_x_max, pin_y_min + ((double)(j + 1) / sp) * (pin_y_max - pin_y_min), cur_h)), new GH_Path(i, 4 + j), 0);
                    //standFloor.Insert(new Line(new Point3d(pin_x_min, pin_y_min + ((double)(j + 1) / sp) * (pin_y_max - pin_y_min), cur_h), new Point3d(pin_x_max, pin_y_min + ((double)(j + 1) / sp) * (pin_y_max - pin_y_min), cur_h)), new GH_Path(i, 3, 4+j), 0);
                }
                for (int j = 0; j < (int)((pin_x_max - pin_x_min) / space); j++)
                {
                    int sp = (int)((pin_x_max - pin_x_min) / space) + 1;
                    wall.Insert(new Line(new Point3d(pin_x_min + ((double)(j + 1) / sp) * (pin_x_max - pin_x_min), pin_y_min, cur_h), new Point3d(pin_x_min + ((double)(j + 1) / sp) * (pin_x_max - pin_x_min), pin_y_max, cur_h)), new GH_Path(i, 4 + (int)((pin_y_max - pin_y_min) / space) + j), 0);
                    //standFloor.Insert(new Line(new Point3d(pin_x_min + ((double)(j + 1) / sp) * (pin_x_max - pin_x_min), pin_y_min, cur_h), new Point3d(pin_x_min + ((double)(j + 1) / sp) * (pin_x_max - pin_x_min), pin_y_max, cur_h)), new GH_Path(i, 3, 4 + (int)((pin_y_max - pin_y_min) / space) + j), 0);

                }
            }
            DA.SetDataTree(0, column);
            DA.SetDataTree(1, beam1);
            DA.SetDataTree(2, beam2);
            DA.SetDataTree(3, wall);
            //DA.SetDataTree(4, standFloor);
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
            get { return new Guid("fca27715-7a96-4ab8-bb37-935167153941"); }
        }
    }
}
