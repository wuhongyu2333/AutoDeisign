using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWindowsForms.NeccessaryFunctions
{
    public class Section
    {
        public SectionType Type { set; get; }
        public int B { set; get; }
        public int H { set; get; }
        public int U { set; get; }
        public int T { set; get; }
        public int D { set; get; }
        public int F { set; get; }
        /// <summary>
        /// 用来构造一个默认混凝土矩形截面
        /// </summary>
        public Section()
        {
            this.Type = SectionType.矩形;
            this.B = 300;
            this.H = 700;
        }
        /// <summary>
        /// 用来构造最常见的混凝土圆形截面
        /// </summary>
        /// <param name="b"></param>
        public Section(int b)
        {
            this.Type = SectionType.圆形;
            this.B = b;
        }
        /// <summary>
        /// 用来构造最常见的混凝土矩形截面
        /// </summary>
        /// <param name="b"></param>
        /// <param name="h"></param>
        public Section(int b, int h)
        {
            this.Type = SectionType.矩形;
            this.B = b;
            this.H = h;
        }
        /// <summary>
        /// 用来构造最常见的工字型截面
        /// </summary>
        /// <param name="b"></param>
        /// <param name="h"></param>
        /// <param name="u"></param>
        /// <param name="t"></param>
        public Section(int b, int h, int u, int t)
        {
            this.Type = SectionType.工字形;
            this.B = b;
            this.H = h;
            this.U = u;
            this.T = t;
        }
        /// <summary>
        /// 用来构造最常见的工形劲
        /// </summary>
        /// <param name="b"></param>
        /// <param name="h"></param>
        /// <param name="u"></param>
        /// <param name="t"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        public Section(int b, int h, int u, int t, int d, int f)
        {
            this.Type = SectionType.工形劲;
            this.B = b;
            this.H = h;
            this.U = u;
            this.T = t;
            this.D = d;
            this.F = f;
        }
        /// <summary>
        /// 用来构造复杂截面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="b"></param>
        /// <param name="h"></param>
        /// <param name="u"></param>
        /// <param name="t"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        public Section(SectionType type, int b, int h, int u, int t, int d, int f)
        {
            this.Type = type;
            this.B = b;
            this.H = h;
            this.U = u;
            this.T = t;
            this.D = d;
            this.F = f;
        }
    }
}
