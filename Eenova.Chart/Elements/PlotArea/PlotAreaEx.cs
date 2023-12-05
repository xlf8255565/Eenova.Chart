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


using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Eenova.Chart.Elements
{
    static class PlotAreaEx
    {
        /// <summary>
        /// 根据枚举值获取枚举匹配的Y值。
        /// </summary>
        /// <param name="linkY"></param>
        /// <returns></returns>
        public static Axis FindAxisY(this PlotArea area, PlotY linkY)
        {
            switch (linkY)
            {
                default:
                case PlotY.Y1:
                    return area.AxisY1;
                case PlotY.Y2:
                    return area.AxisY2;
                case PlotY.Y3:
                    return area.AxisY3;
                case PlotY.Y4:
                    return area.AxisY4;
            }
        }

        /// <summary>
        /// 获取Y1轴以左的宽度。
        /// </summary>
        /// <returns></returns>
        public static double GetLeftWidth(this PlotArea area)
        {
            var list = new List<double>();
            list.Add(area.IsAxisXVisible() ? area.AxisX.Left : 0);
            list.Add(area.IsAxisY1Visible() ? area.AxisY1.Left : 0);
            list.Add(area.IsAxisY2Visible() ? area.AxisY2.Left : 0);
            list.Add(area.IsAxisY3Visible() ? area.AxisY3.Left - area.Length : 0);
            list.Add(area.IsAxisY4Visible() ? area.AxisY4.Left - area.Length : 0);
            return list.Max() + 10;
        }

        /// <summary>
        /// 获取Y3轴以右的宽度。
        /// </summary>
        /// <returns></returns>
        public static double GetRightWidth(this PlotArea area)
        {
            var list = new List<double>();
            list.Add(area.IsAxisXVisible() ? area.AxisX.Right : 0);
            list.Add(area.IsAxisY1Visible() ? area.AxisY1.Right - area.Length : 0);
            list.Add(area.IsAxisY2Visible() ? area.AxisY2.Right - area.Length : 0);
            list.Add(area.IsAxisY3Visible() ? area.AxisY3.Right : 0);
            list.Add(area.IsAxisY4Visible() ? area.AxisY4.Right : 0);
            return list.Max() + 10;
        }

        /// <summary>
        /// 获取上部高度。
        /// </summary>
        /// <returns></returns>
        public static double GetTopHeight(this PlotArea area)
        {
            var list = new List<double>();
            list.Add(area.IsAxisXVisible() ? area.AxisX.Top - area.AxisX.Margin.Top : 0);
            list.Add(area.IsAxisY1Visible() ? area.AxisY1.Top : 0);
            list.Add(area.IsAxisY2Visible() ? area.AxisY2.Top : 0);
            list.Add(area.IsAxisY3Visible() ? area.AxisY3.Top - area.TopLinesHost.ActualHeight : 0);
            list.Add(area.IsAxisY4Visible() ? area.AxisY4.Top - area.TopLinesHost.ActualHeight : 0);
            return list.Max() + 10;
        }

        /// <summary>
        /// 获取下部高度。
        /// </summary>
        /// <returns></returns>
        public static double GetBottomHeight(this PlotArea area)
        {
            var list = new List<double>();
            list.Add(area.IsAxisXVisible() ? area.AxisX.Bottom + area.AxisX.Margin.Top - area.VisualHeight() : 0);
            list.Add(area.IsAxisY1Visible() ? area.AxisY1.Bottom - area.BottomLinesHost.ActualHeight : 0);
            list.Add(area.IsAxisY2Visible() ? area.AxisY2.Bottom - area.BottomLinesHost.ActualHeight : 0);
            list.Add(area.IsAxisY3Visible() ? area.AxisY3.Bottom : 0);
            list.Add(area.IsAxisY4Visible() ? area.AxisY4.Bottom : 0);
            return list.Max() + 10;
        }

        /// <summary>
        /// 呈现的高度。
        /// </summary>
        public static double VisualHeight(this PlotArea area)
        {
            return (area.TopVisibility == Visibility.Visible ? area.TopHeight : 0) +
                (area.BottomVisibility == Visibility.Visible ? area.BottomHeight : 0);
        }

        private static bool IsAxisXVisible(this PlotArea area)
        {
            return area.AxisX.Visibility == Visibility.Visible;
        }

        private static bool IsAxisY1Visible(this PlotArea area)
        {
            return area.AxisY1.Visibility == Visibility.Visible && area.TopVisibility == Visibility.Visible;
        }

        private static bool IsAxisY2Visible(this PlotArea area)
        {
            return area.AxisY2.Visibility == Visibility.Visible && area.BottomVisibility == Visibility.Visible;
        }

        private static bool IsAxisY3Visible(this PlotArea area)
        {
            return area.AxisY3.Visibility == Visibility.Visible && area.TopVisibility == Visibility.Visible;
        }

        private static bool IsAxisY4Visible(this PlotArea area)
        {
            return area.AxisY4.Visibility == Visibility.Visible && area.BottomVisibility == Visibility.Visible;
        }
    }
}
