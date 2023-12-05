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
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eenova.Chart.Controls
{
    public class StrokeBorder : Control
    {
        public StrokeBorder()
        {
            this.DefaultStyleKey = typeof(StrokeBorder);
        }

        #region dp

        #region Stroke


        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(StrokeBorder), null);



        /// <summary>
        /// 线条样式。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(StrokeBorder),
            new PropertyMetadata(StrokeStyles.Solid));

        /// <summary>
        /// 线条厚度。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(StrokeBorder),
            new PropertyMetadata((double)2, OnStrokeThicknessChanged));


        /// <summary>
        /// 线条颜色。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(StrokeBorder),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        #endregion

        #region border


        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(StrokeBorder), null);



        #endregion

        #endregion dp

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as StrokeBorder;
            element.OnStrokeThicknessChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnStrokeThicknessChanged(double oldValue, double newValue)
        {
            //改变线宽的时候不会改变。重新设置下线形就会改变了。先用这种方法，原因再去排查。
            string style = this.StrokeStyle;
            this.StrokeStyle = StrokeStyles.Rush;
            this.StrokeStyle = style;
        }
    }
}
