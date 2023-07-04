using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace gh4
{
    public class gh4Component : GH_Component
    {
        public gh4Component()
          : base("column_arrange", "C_A",
              "arrange column",
              "myGH", "C_A")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("外立面曲面", "外", "请选择外立面曲面", GH_ParamAccess.item);
            pManager.AddBrepParameter("核心筒曲面", "内", "请选择核心筒曲面", GH_ParamAccess.item);
            pManager.AddNumberParameter("层高表", "H", "请输入层高表", GH_ParamAccess.list);
            pManager.AddNumberParameter("基底标高", "Base", "请输入基底标高", GH_ParamAccess.item);
            pManager.AddNumberParameter("外柱与外立面偏移量", "Dis", "请输入偏移量", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("柱数", "NC", "请输入每层柱数", GH_ParamAccess.item, 4);
            pManager.AddNumberParameter("旋转角度", "Angle", "请输入外立面旋转角度数", GH_ParamAccess.item, 0);

        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Column", "Col", "This is the column set", GH_ParamAccess.item);
            pManager.AddLineParameter("Beam1", "Beam1", "This is the beam1 set", GH_ParamAccess.item);
            pManager.AddLineParameter("Beam2", "Beam2", "This is the beam2 set", GH_ParamAccess.item); 
            pManager.AddBrepParameter("Wall", "Wall", "This is the wall set", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Brep surf1 = null;
            Brep surf2 = null;
            List<double> height = new List<double>();
            double ba = 0;
            double dis = 0;
            int num_col=4;
            double angle = 0;
            DA.GetData(0, ref surf1);
            DA.GetData(1, ref surf2);
            DA.GetDataList<double>(2,  height);
            DA.GetData(3, ref ba);
            DA.GetData(4, ref dis);
            DA.GetData(5, ref num_col);
            DA.GetData(6, ref angle);

            //新建内外立面的交线和交点
            List<Curve> CurveOut = new List<Curve>();
            List<Curve> CurveIn = new List<Curve>();
            List<Point3d> pout = new List<Point3d>();
            List<Point3d> pin = new List<Point3d>();

            //新建梁柱
            List<Line> colunm = new List<Line>();
            List<Line> beam1 = new List<Line>();
            List<Line> beam2 = new List<Line>();
            

            for (int floor = 0; floor < height.Count; floor++)
            {
                Plane pla = new Plane(new Point3d(0,0,height[floor]+ba),new Vector3d(0,0,1));

                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf1, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesOut, out Point3d[] intersectionPointsOut);
                Rhino.Geometry.Intersect.Intersection.BrepPlane(surf2, pla, Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance, out Curve[] intersectionCurvesIn,out Point3d[] intersectionPointsIn);
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
                    p_temp = intersectionCurvesOut[0].PointAt((out_div[j]+ intersectionCurvesOut[0].GetLength()*angle/360)% intersectionCurvesOut[0].GetLength());
                    pout.Add(p_temp);
                }
                List<Curve> in_div = new List<Curve>();
                in_div.AddRange(intersectionCurvesIn[0].DuplicateSegments());
                int n1 = (int)Math.Round((num_col - 4) / 2 * in_div[0].GetLength()/(in_div[0].GetLength()+ in_div[1].GetLength()));
                int n2 = (num_col - 4) / 2 - n1;
;               for(int j = 0; j < in_div.Count; j++)
                {
                    p_temp = in_div[j].PointAtStart;
                    pin.Add(p_temp);
                    if (j % 2 == 0)
                    {
                        double[] div_temp = in_div[j].DivideByCount(n1+1, true);
                        for(int k = 0; k < n1; k++)
                        {
                            pin.Add(in_div[j].PointAt(div_temp[k+1]));
                        }
                    }
                    else
                    {
                        double[] div_temp = in_div[j].DivideByCount(n2 + 1, true);
                        for (int k = 0; k < n2; k++)
                        {
                            pin.Add(in_div[j].PointAt(div_temp[k + 1]));
                        }
                    }
                }
            }

            //柱
            for(int i = 0; i < pout.Count / num_col - 1; i++)
            {
                for(int j = 0; j < num_col; j++)
                {
                    colunm.Add(new Line(pout[j+i*num_col],pout[j + (i+1) * num_col]));
                    //colunm.Add(new Line(pin[j + i * num_col], pin[j + (i + 1) * num_col]));
                }
                
            }

            //梁
            for (int i = 0; i < pout.Count / num_col; i++)
            {
                for (int j = 0; j < num_col; j++)
                {
                    beam1.Add(new Line(pin[j + i * num_col], pout[num_col-1-j + i  * num_col]));
                }

            }
            for (int i = 0; i < pout.Count / num_col; i++)
            {
                for (int j = 0; j < num_col - 1; j++)
                {
                    beam2.Add(new Line(pout[j + i * num_col], pout[j + i * num_col + 1]));
                }
                beam2.Add(new Line(pout[i * num_col], pout[(i + 1) * num_col - 1]));

            }
            DA.SetDataList(0, colunm);
            DA.SetDataList(1, beam1);
            DA.SetDataList(2, beam2);
            DA.SetData(3, surf2);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("575ea858-7df0-473e-aa58-3f19dccd579f"); }
        }
    }
}
