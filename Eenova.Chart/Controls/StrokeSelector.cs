﻿using System;
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

namespace Eenova.Chart.Controls
{
    public class StrokeSelector : Control
    {
        public StrokeSelector()
        {
            this.DefaultStyleKey = typeof(StrokeSelector);
        }



        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(StrokeSelector), null);

        public Visibility SStrokeVisibility
        {
            get { return (Visibility)GetValue(SStrokeVisibilityProperty); }
            set { SetValue(SStrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeVisibilityProperty =
            DependencyProperty.Register("SStrokeVisibility", typeof(Visibility), typeof(StrokeSelector), null);



        public string SStrokeStyle
        {
            get { return (string)GetValue(SStrokeStyleProperty); }
            set { SetValue(SStrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeStyleProperty =
            DependencyProperty.Register("SStrokeStyle", typeof(string), typeof(StrokeSelector), null);



        public Brush SStroke
        {
            get { return (Brush)GetValue(SStrokeProperty); }
            set { SetValue(SStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeProperty =
            DependencyProperty.Register("SStroke", typeof(Brush), typeof(StrokeSelector), null);



        public double SStrokeThickness
        {
            get { return (double)GetValue(SStrokeThicknessProperty); }
            set { SetValue(SStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeThicknessProperty =
            DependencyProperty.Register("SStrokeThickness", typeof(double), typeof(StrokeSelector), null);

    }
}

