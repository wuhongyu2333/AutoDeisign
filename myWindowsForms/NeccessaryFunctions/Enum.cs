using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWindowsForms.NeccessaryFunctions
{
    /// <summary>
    /// 混凝土强度等级
    /// </summary>

    public enum ConLevel
    {
        [Conceret(0.91, 7.2, 1.27, 10.0, 22000)]
        C15,
        [Conceret(1.10, 9.6, 1.54, 13.4, 25500)]
        C20,
        [Conceret(1.27, 11.9, 1.78, 16.7, 28000)]
        C25,
        [Conceret(1.43, 14.3, 2.01, 20.1, 30000)]
        C30,
        [Conceret(1.57, 16.7, 2.20, 23.4, 31500)]
        C35,
        [Conceret(1.71, 19.1, 2.39, 26.8, 32500)]
        C40,
        [Conceret(1.80, 21.1, 2.51, 29.6, 33500)]
        C45,
        [Conceret(1.89, 23.1, 2.64, 32.4, 34500)]
        C50,
        [Conceret(1.96, 25.3, 2.74, 35.5, 35500)]
        C55,
        [Conceret(2.04, 27.5, 2.85, 38.5, 36000)]
        C60,
        [Conceret(2.09, 29.7, 2.93, 41.5, 36500)]
        C65,
        [Conceret(2.14, 31.8, 2.99, 44.5, 37000)]
        C70,
        [Conceret(2.18, 33.8, 3.05, 47.4, 37500)]
        C75,
        [Conceret(2.22, 35.9, 3.11, 50.2, 38000)]
        C80,
    }
    /// <summary>
    /// 钢筋强度等级
    /// </summary>
    public enum RebarLevel
    {
        HPB300,
        HRB335,
        HRB400,
        HRB500,
    }
    /// <summary>
    /// 截面类型
    /// </summary>
    public enum SectionType
    {
        矩形 = 1,
        工字形 = 2,
        圆形 = 3,
        正多边形 = 4,
        槽形 = 5,
        十字形 = 6,
        箱形 = 7,
        圆管 = 8,
        双槽形 = 9,
        钢管混凝土 = 10,
        工形劲 = 13,
        箱型劲 = 14,
        Other = 99,
    }
    #region 地震相关参数
    /// <summary>
    /// 抗震设防类别
    /// </summary>
    public enum SeismicCategory
    {
        /// <summary>
        /// 甲类
        /// </summary>
        I,
        /// <summary>
        /// 乙类
        /// </summary>
        II,
        /// <summary>
        /// 丙类
        /// </summary>
        III,
        /// <summary>
        /// 丁类
        /// </summary>
        IV,
    }
    /// <summary>
    /// 构件抗震等级
    /// </summary>
    public enum AntiSeismicGrade
    {
        /// <summary>
        /// 特一级
        /// </summary>
        VeryFirst,
        /// <summary>
        /// 一级
        /// </summary>
        First,
        /// <summary>
        /// 二级
        /// </summary>
        Second,
        /// <summary>
        /// 三级
        /// </summary>
        Third,
        /// <summary>
        /// 四级
        /// </summary>
        Fourth,
        /// <summary>
        /// 非抗震
        /// </summary>
        None,
    }
    /// <summary>
    /// 构件构造抗震等级
    /// </summary>
    public enum DetialofAntiSeismicGrade
    {
        /// <summary>
        /// 提高二级
        /// </summary>
        Upper2Grade,
        /// <summary>
        /// 提高一级
        /// </summary>
        Upper1Grade,
        /// <summary>
        /// 不改变,
        /// </summary>
        Unchanged,
        /// <summary>
        /// 降低一级
        /// </summary>
        Lower1Grade,
        /// <summary>
        /// 降低两级
        /// </summary>
        Lower2Grade,
    }
    /// <summary>
    /// 地震计算
    /// </summary>
    public enum SeismicCalculation
    {
        /// <summary>
        /// 不计算
        /// </summary>
        Dismiss,
        /// <summary>
        /// 计算水平地震
        /// </summary>
        CalHorizSeismic,
        /// <summary>
        /// 计算水平+竖向地震(底部轴力法)
        /// </summary>
        CalHorizAndVerticalSeismic_N,
        /// <summary>
        /// 计算水平+竖向地震(反应谱法)
        /// </summary>
        CalHorizAndVerticalSeismic_ReactionSpectrum,
        /// <summary>
        /// 计算水平+竖向地震(底部轴力等效静力)
        /// </summary>
        CalHorizAndVerticalSeismic_EquivalentN,
    }

    /// <summary>
    /// 场地类别——与设计地震分组共同确定特征周期值
    /// </summary>
    public enum SiteCategory
    {
        I0,
        I1,
        II,
        III,
        IV,
    }

    /// <summary>
    /// 设计地震分组——由项目地点ProjectLocation查表确定
    /// </summary>
    public enum SeismicDesignCategory
    {
        /// <summary>
        /// 第一组
        /// </summary>
        First,
        /// <summary>
        /// 第二组
        /// </summary>
        Second,
        /// <summary>
        /// 第三组
        /// </summary>
        Third,
    }

    #endregion

    #region 风相关参数

    /// <summary>
    /// 风荷载计算
    /// </summary>
    public enum WindCalculation
    {
        /// <summary>
        /// 不计算
        /// </summary>
        Dismiss,
        /// <summary>
        /// 计算水平风
        /// </summary>
        CalHorizWind,
        /// <summary>
        /// 计算特殊风
        /// </summary>
        CalSpecialWind,
        /// <summary>
        /// 计算水平风和特殊风
        /// </summary>
        CalBothWind,
    }
    /// <summary>
    /// 地面粗糙度类别
    /// </summary>
    public enum WindGroundRoughCategory
    {
        A,
        B,
        C,
        D,
    }
    #endregion
}
