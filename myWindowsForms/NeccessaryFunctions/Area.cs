using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWindowsForms.NeccessaryFunctions
{
    public static class Calculation
    {
        //public static List<double> CalFloorAreas(DataTree<Curve> dataTree)
        //{
        //    List<double> areas = new List<double>();
        //    for (int i = 0; i < dataTree.Branches.Count; i++)
        //    {
        //        List<Curve> dataList = dataTree.Branches[i];
        //        PolyCurve pl = new PolyCurve();
        //        double z = dataList[0].PointAt(0).Z;
        //        for (int j = 0; j < dataList.Count; j++)
        //        {
        //            double x1 = dataList[j].PointAt(0).X;
        //            double y1 = dataList[j].PointAt(0).Y;
        //            double x2 = dataList[j].PointAt(1).X;
        //            double y2 = dataList[j].PointAt(1).Y;
        //            bool sucess = pl.Append(new Line(x1, y1, z, x2, y2, z));
        //        }
        //        pl.MakeClosed(50000);
        //        var areaComp = AreaMassProperties.Compute(pl);
        //        if (areaComp != null)
        //        {
        //            areas.Add(areaComp.Area);
        //        }
        //        else
        //        {
        //            var bbx = pl.GetBoundingBox(true);
        //            // 这边直接用BoundingBox的面积来近似估计荷载，稍微简单一点
        //            areas.Add(bbx.Area / 2);
        //        }
        //    }
        //    return areas;
        //}
    }
}
