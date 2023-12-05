/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/



using System.ComponentModel;
namespace Eenova.Chart
{
    public enum DragDirection
    {
        None = 0,
        X = 1,
        Y = 2,
        Both = 3,
    }

    public enum OrderType
    {
        [Description("默认")]
        Default = 0,
        [Description("根据X轴数据排序")]
        OrderByX = 1,
        [Description("根据Y轴数据排序")]
        OrderByY = 2,
    }

    public enum NoteLocation
    {
        Top = 0,
        Bottom = 1,
    }

    /// <summary>
    /// 刻度显示方式。
    /// </summary>
    public enum TicksShow
    {
        All = 0,
        TopOrLeft = 1,
        BottomOrRight = 2,
        None = 3,
    }

    /// <summary>
    /// 坐标轴类型。
    /// </summary>
    public enum AxisType
    {
        X = 0,
        Y = 1,
    }

    /// <summary>
    /// 坐标轴标注或标题的位置。
    /// </summary>
    public enum AxisLocation
    {
        TopOrLeft = 0,
        BottomOrRight = 1,
        None = 2,
    }

    /// <summary>
    /// 坐标轴标题对齐方式。
    /// </summary>
    public enum AxisAlignment
    {
        TopOrLeft = 0,
        Center = 1,
        BottomOrRight = 2,
    }

    public enum AxisFixPoint
    {
        StartPoint = 0,
        EndPoint = 1,
    }

    public enum PlotY
    {
        /// <summary>
        /// 左上。
        /// </summary>
        Y1 = 0,
        /// <summary>
        /// 左下。
        /// </summary>
        Y2 = 1,
        /// <summary>
        /// 右上。
        /// </summary>
        Y3 = 2,
        /// <summary>
        /// 右下。
        /// </summary>
        Y4 = 3,
    }

    public enum DataType
    {
        /// <summary>
        /// 文本轴。
        /// </summary>
        Text = 0,
        /// <summary>
        /// 时间轴。
        /// </summary>
        DateTime = 1,
        /// <summary>
        /// 数值轴。
        /// </summary>
        Numberic = 2,
    }

    public enum ShapeType
    {
        /// <summary>
        /// 圆。
        /// </summary>
        Circle = 0,
        /// <summary>
        /// 正方形。
        /// </summary>
        Square = 1,
        /// <summary>
        /// 菱形。
        /// </summary>
        Diamond = 2,
        /// <summary>
        /// 三角形。
        /// </summary>
        Triangle = 3,
        /// <summary>
        /// 五边形。
        /// </summary>
        Pentagon = 4,
        /// <summary>
        /// 六边形。
        /// </summary>
        Hexagon = 5,
        /// <summary>
        /// 十字星。
        /// </summary>
        CrossStar = 6,
        /// <summary>
        /// 五角星。
        /// </summary>
        Star = 7,
    }

    public enum LinkType
    {
        Line = 0,
        Bar = 1,
    }
}
