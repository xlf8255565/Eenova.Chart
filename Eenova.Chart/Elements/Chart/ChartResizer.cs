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


using System;
using System.Windows;
using Eenova.Chart.Behaviors;

namespace Eenova.Chart.Elements
{
    class ChartResizer
    {
        MouseResizeBehavior _resizer;
        double _xOffset;
        double _yOffset;
        double _x;

        public ChartResizer()
        {
            _resizer = new MouseResizeBehavior();
        }

        internal void Attach(Chart chart)
        {
            _resizer.BeginResize += new EventHandler(BeginResize);
            _resizer.EndResize += new EventHandler(EndResize);
            _resizer.Attach(chart);
        }

        internal void Detach()
        {
            _resizer.BeginResize -= new EventHandler(BeginResize);
            _resizer.EndResize -= new EventHandler(EndResize);
            _resizer.Detach();
        }

        void EndResize(object sender, EventArgs e)
        {
            var chart = sender as Chart;
            if (chart.PlotAreas.Count > 0)
            {
                var p = chart.PlotAreas[0];
                //p.Length = Math.Max(50, chart.ActualWidth - _xOffset);
                p.Length = Math.Max(50, chart.ActualWidth - _x);

                if (p.TopVisibility == Visibility.Visible && p.BottomVisibility == Visibility.Collapsed)
                {
                    p.TopHeight = Math.Max(50, chart.ActualHeight - _yOffset);
                }
                else if (p.TopVisibility == Visibility.Collapsed && p.BottomVisibility == Visibility.Visible)
                {
                    p.BottomHeight = Math.Max(50, chart.ActualHeight - _yOffset);
                }
                else if (p.TopVisibility == Visibility.Visible && p.BottomVisibility == Visibility.Visible)
                {
                    var topRatio = p.TopHeight / (p.TopHeight + p.BottomHeight);
                    p.TopHeight = Math.Max(50, (chart.ActualHeight - _yOffset) * topRatio);
                    p.BottomHeight = Math.Max(50, (chart.ActualHeight - _yOffset) * (1 - topRatio));
                }
                p.SetSize();
            }
        }

        void BeginResize(object sender, EventArgs e)
        {
            var chart = sender as Chart;
            if (chart.PlotAreas.Count > 0)
            {
                var p = chart.PlotAreas[0];
                _x = chart.ActualWidth - p.Length;
                _xOffset = Math.Max(p.ActualWidth - p.Length + p.Margin.Left + p.Margin.Right, chart.ActualWidth - p.Length);
                _yOffset = Math.Max(p.ActualHeight - p.VisualHeight() + p.Margin.Top + p.Margin.Bottom, chart.ActualHeight - p.VisualHeight());
            }
        }
    }
}
