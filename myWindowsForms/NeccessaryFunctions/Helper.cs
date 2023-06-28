using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWindowsForms.NeccessaryFunctions
{

    [AttributeUsage(AttributeTargets.Field)]
    public class ConceretAttribute : Attribute
    {
        protected double _ft;
        protected double _fc;
        protected double _ftk;
        protected double _fck;
        protected double _Ec;
        /// <summary>
        /// 设置不同混凝土等级对应的参数
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="fc"></param>
        /// <param name="ftk"></param>
        /// <param name="fck"></param>
        /// <param name="Ec"></param>
        public ConceretAttribute(double ft, double fc, double ftk, double fck, double Ec)
        {
            this._ft = ft;
            this._fc = fc;
            this._ftk = ftk;
            this._fck = fck;
            this._Ec = Ec;
        }
        public double ft { get { return _ft; } }
        public double fc { get { return _fc; } }
        public double ftk { get { return _fck; } }
        public double fck { get { return _fck; } }
        public double Ec { get { return _Ec; } }

    }
}
