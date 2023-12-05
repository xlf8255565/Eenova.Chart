﻿/*****************************************************************************
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


using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Setter
{
    public class MarginSelector : Control
    {
        public MarginSelector()
        {
            this.DefaultStyleKey = typeof(MarginSelector);
        }

        public Thickness SMargin
        {
            get { return (Thickness)GetValue(SMarginProperty); }
            set { SetValue(SMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarginProperty =
            DependencyProperty.Register("SMargin", typeof(Thickness), typeof(MarginSelector),
            new PropertyMetadata(OnMarginChanged));



        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(MarginSelector),
            new PropertyMetadata(OnLeftChanged));



        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(MarginSelector),
            new PropertyMetadata(OnTopChanged));



        public double Bottom
        {
            get { return (double)GetValue(BottomProperty); }
            set { SetValue(BottomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Bottom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.Register("Bottom", typeof(double), typeof(MarginSelector),
            new PropertyMetadata(OnBottomChanged));



        public double Right
        {
            get { return (double)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Right.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(double), typeof(MarginSelector),
            new PropertyMetadata(OnRightChanged));

        private static void OnMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MarginSelector;
            element.OnMarginChanged((Thickness)e.OldValue, (Thickness)e.NewValue);
        }


        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MarginSelector;
            element.OnLeftChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MarginSelector;
            element.OnTopChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MarginSelector;
            element.OnRightChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MarginSelector;
            element.OnBottomChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnLeftChanged(double oldValue, double newValue)
        {
            var left = newValue;
            var top = SMargin.Top;
            var right = SMargin.Right;
            var bottom = SMargin.Bottom;
            SMargin = new Thickness(left, top, right, bottom);
        }

        private void OnTopChanged(double oldValue, double newValue)
        {
            var left = SMargin.Left;
            var top = newValue;
            var right = SMargin.Right;
            var bottom = SMargin.Bottom;
            SMargin = new Thickness(left, top, right, bottom);
        }

        private void OnRightChanged(double oldValue, double newValue)
        {
            var left = SMargin.Left;
            var top = SMargin.Top;
            var right = newValue;
            var bottom = SMargin.Bottom;
            SMargin = new Thickness(left, top, right, bottom);
        }

        private void OnBottomChanged(double oldValue, double newValue)
        {
            var left = SMargin.Left;
            var top = SMargin.Top;
            var right = SMargin.Right;
            var bottom = newValue;
            SMargin = new Thickness(left, top, right, bottom);
        }

        private void OnMarginChanged(Thickness oldValue, Thickness newValue)
        {
            this.Left = this.SMargin.Left;
            this.Top = this.SMargin.Top;
            this.Bottom = this.SMargin.Bottom;
            this.Right = this.SMargin.Right;
        }
    }
}
