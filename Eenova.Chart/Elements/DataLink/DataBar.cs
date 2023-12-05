using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Eenova.Chart.Elements
{
    public class DataBar : ContentControl
    {
        internal DataLink DataLink { get; set; }


        public void Load()
        {
            //todo 
        }

        #region dp

        #region BorderVisibility

        /// <summary>
        /// 边框底色是否可见，默认Collapsed。
        /// </summary>
        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        public static DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(DataBar),
            new PropertyMetadata(Visibility.Collapsed));


        #endregion BorderVisibility

        #region StrokeVisibility

        /// <summary>
        /// 数据线是否可见，默认Visible。
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(DataBar),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region StrokeStyle

        /// <summary>
        /// 线条样式，默认StrokeStyles.Solid。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(DataBar),
            new PropertyMetadata(StrokeStyles.Solid));


        #endregion

        #region StrokeThickness

        /// <summary>
        /// 线条厚度，默认1。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(DataBar),
            new PropertyMetadata((double)1, OnStrokeThicknessChanged));

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region Stroke

        /// <summary>
        /// 线条颜色，默认Black。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(DataBar),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #endregion dp
    }
}
