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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using Eenova.Chart.Helpers;
using System.Windows.Data;

namespace Eenova.Chart.Elements
{
    public class MarkupLine : Grid
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

        public MarkupLineItemCollection MarkupItems { get; private set; }

        public MarkupLine()
        {
            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            MarkupItems = new MarkupLineItemCollection();
            MarkupItems.CollectionChanged += (s, e) => this.Load();
        }

        internal void Load()
        {
            this.Children.Clear();
            if (Axis == null || Axis.DataType == DataType.Text || MarkupItems.Count == 0)
                return;

            IEnumerable region;
            foreach (var item in MarkupItems)
            {
                if (Axis.DataType == DataType.Numberic)
                    region = new List<double> { item.Position };
                else
                    region = new List<DateTime> { TimeHelper.GetTime(item.Position) };

                var values = Axis.Convert(region);
                if (double.IsNaN(values[0]))
                    continue;
                this.Children.Add(this.CreateArea(Math.Round(values[0]), item));
            }

            this.SetTransform();
        }

        private Line CreateArea(double position, MarkupLineItem item)
        {
            var line = new Line
            {
                StrokeStartLineCap = PenLineCap.Square,
                StrokeEndLineCap = PenLineCap.Square,
                Stretch = Stretch.Fill
            };
            line.SetBinding(Shape.StrokeProperty, new Binding("Brush") { Source = item });
            line.SetBinding(Shape.StrokeThicknessProperty, new Binding("Thickness") { Source = item });
            line.SetBinding(Shape.StrokeDashArrayProperty, new Binding("Style") { Source = item });
            if (Axis.IsAxisX)
            {
                line.Y2 = 80;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.Margin = new Thickness(Math.Round(position - line.StrokeThickness / 2), 0, 0, 0);
            }
            else
            {
                line.X2 = 80;
                line.VerticalAlignment = VerticalAlignment.Top;
                line.Margin = new Thickness(0, Math.Round(position - line.StrokeThickness / 2), 0, 0);
            }
            return line;
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
