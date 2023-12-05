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
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Elements
{
    public class MarkupArea : Grid
    {
        Axis _axis;

        public Axis Axis
        {
            get { return _axis; }
            internal set
            {
                _axis = value;
                this.Load();
            }
        }

        public MarkupAreaItemCollection MarkupItems { get; private set; }

        public MarkupArea()
        {
            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            MarkupItems = new MarkupAreaItemCollection();
            MarkupItems.CollectionChanged += (s, e) => this.Load();
            this.SizeChanged += (s, e) => this.SetClip();
        }

        internal void Load()
        {
            this.Children.Clear();
            if (Axis == null || Axis.DataType == DataType.Text || MarkupItems.Count == 0)
                return;

            foreach (var item in MarkupItems)
            {
                this.LoadAutoExtendItem(item);
            }

            this.SetClip();
            this.SetTransform();
        }

        private void LoadAutoExtendItem(MarkupAreaItem item)
        {
            IEnumerable region;
            if (Axis.DataType == DataType.Numberic)
                region = new List<double> { item.Start, item.End };
            else
                region = new List<DateTime> { TimeHelper.GetTime(item.Start), TimeHelper.GetTime(item.End) };

            var values = Axis.Convert(region);
            if (double.IsNaN(values[1]))
                return;

            if (double.IsNaN(values[0]))
                values[0] = 0;

            this.Children.Add(this.CreateArea(Math.Round(values[0]), Math.Round(values[1]), item.Brush));
        }

        private Rectangle CreateArea(double start, double end, Brush brush)
        {
            var rect = new Rectangle { Fill = brush };
            if (Axis.IsAxisX)
            {
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.Width = Math.Max(end - start, 0);
                rect.Margin = new Thickness(start, 0, 0, 0);
            }
            else
            {
                rect.VerticalAlignment = VerticalAlignment.Top;
                rect.Height = Math.Max(end - start, 0);
                rect.Margin = new Thickness(0, start, 0, 0);
            }
            return rect;
        }

        private void SetClip()
        {
            this.Clip = new RectangleGeometry { Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight) };
        }

        private void SetTransform()
        {
            if (Axis.IsAxisX)
            {
                ((CompositeTransform)this.RenderTransform).ScaleX = Axis.IsDesc ? -1 : 1;
            }
            else
            {
                ((CompositeTransform)this.RenderTransform).ScaleY = Axis.IsDesc ? 1 : -1;
            }
        }
    }
}
