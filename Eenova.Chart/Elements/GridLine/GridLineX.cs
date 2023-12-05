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
    public class GridLineX : GridLine
    {

        protected override void SetTransform()
        {
            ((CompositeTransform)this.RenderTransform).ScaleX = this.IsDesc ? -1 : 1;
        }

        protected override void SetLineSize(Polyline line, double offset)
        {
            line.Points[0] = new Point(offset, 0);
            line.Points[1] = new Point(offset, this.ActualHeight);
        }

        protected override void SetOffset(double offset)
        {
            offset = this.IsDesc ? offset : offset + 1;
            Canvas.SetTop(_linePanel, 0);
            Canvas.SetLeft(_linePanel, Math.Round(offset));
            Canvas.SetTop(this.SelectedEffect, 0);
            Canvas.SetLeft(this.SelectedEffect, Math.Round(offset));
        }

        protected override void SetEffectOffset(UIElement effect1, UIElement effect2, double offset)
        {
            Canvas.SetLeft(effect1, Math.Round(offset));
            Canvas.SetTop(effect1, Math.Round(-EFFECT_SIZE / 2));
            Canvas.SetLeft(effect2, Math.Round(offset));
            Canvas.SetTop(effect2, Math.Round(this.ActualHeight - EFFECT_SIZE / 2));
        }

        protected override void SetLineTransform(Polyline line)
        {
            line.RenderTransform = this.StrokeThickness % 2 == 0 ? null : new TranslateTransform() { X = 0.5, Y = 0.0 };
        }
    }
}
