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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Eenova.Chart.Setter
{
    public class PointSelector : Control
    {
        private bool _ignoreChanged;

        public PointSelector()
        {
            this.DefaultStyleKey = typeof(PointSelector);
        }

        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Point.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(Point), typeof(PointSelector),
            new PropertyMetadata(OnPointChanged));

        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PointSelector;
            if (source._ignoreChanged)
            {
                source._ignoreChanged = false;
                return;
            }
            if (source.X != source.Point.X)
            {
                source._ignoreChanged = true;
                source.X = source.Point.X;
            }
            if (source.Y != source.Point.Y)
            {
                source._ignoreChanged = true;
                source.Y = source.Point.Y;
            }
        }



        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(PointSelector),
            new PropertyMetadata(OnXChanged));

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PointSelector;
            if (source._ignoreChanged)
            {
                source._ignoreChanged = false;
                return;
            }
            var point= new Point(source.X, source.Y);
            if (source.Point != point)
            {
                source._ignoreChanged = true;
                source.Point = point;
            }
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(PointSelector),
            new PropertyMetadata(OnYChanged));

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PointSelector;
            if (source._ignoreChanged)
            {
                source._ignoreChanged = false;
                return;
            }
            var point = new Point(source.X, source.Y);
            if (source.Point != point)
            {
                source._ignoreChanged = true;
                source.Point = point;
            }
        }
    }
}
