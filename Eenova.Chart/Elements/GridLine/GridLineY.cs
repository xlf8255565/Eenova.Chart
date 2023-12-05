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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eenova.Chart.Elements
{
    public class GridLineY : GridLine
    {

        protected override void SetTransform()
        {
            ((CompositeTransform)this.RenderTransform).ScaleY = this.IsDesc ? 1 : -1;
        }

        protected override void SetLineSize(Polyline line, double offset)
        {
            line.Points[0] = new Point(0, offset);
            line.Points[1] = new Point(this.ActualWidth, offset);
        }

        protected override void SetOffset(double offset)
        {
            offset = this.IsDesc ? offset + 1 : offset;
            Canvas.SetTop(_linePanel, Math.Round(offset));
            Canvas.SetLeft(_linePanel, 0);
            Canvas.SetTop(this.SelectedEffect, Math.Round(offset));
            Canvas.SetLeft(this.SelectedEffect, 0);
        }

        protected override void SetEffectOffset(UIElement effect1, UIElement effect2, double offset)
        {
            Canvas.SetLeft(effect1, Math.Round(-EFFECT_SIZE / 2));
            Canvas.SetTop(effect1, Math.Round(offset));
            Canvas.SetLeft(effect2, Math.Round(this.ActualWidth - EFFECT_SIZE / 2));
            Canvas.SetTop(effect2, Math.Round(offset));
        }

        protected override void SetLineTransform(Polyline line)
        {
            line.RenderTransform = this.StrokeThickness % 2 == 0 ? null : new TranslateTransform() { X = 0.0, Y = 0.5 };
        }
    }
}
