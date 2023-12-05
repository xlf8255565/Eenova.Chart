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


using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Eenova.Chart.Controls;

namespace Eenova.Chart.Elements
{
    /// <summary>
    /// 标题。
    /// </summary>
    public class Title : Control
    {
        public Title()
        {
            this.DefaultStyleKey = typeof(Title);
            //this.Foreground = new SolidColorBrush(Colors.Black);
        }

        #region dp

        #region Border

        /// <summary>
        /// 边框底色是否可见，默认Collapsed。
        /// </summary>
        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        public static DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(Title),
            new PropertyMetadata(Visibility.Collapsed));


        #endregion

        #region Stroke

        /// <summary>
        /// 边框线是否可见，默认Collapsed。
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        public static DependencyProperty StrokeVisibilityProperty = DependencyProperty.Register
            ("StrokeVisibility", typeof(Visibility), typeof(Title),
            new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// 边框线颜色。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register
            ("Stroke", typeof(Brush), typeof(Title),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// 边框线线形。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(Title),
            new PropertyMetadata(StrokeStyles.Solid));

        /// <summary>
        /// 边框线厚度。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Title),
            new PropertyMetadata((double)1));



        #endregion

        #region Text

        /// <summary>
        /// 文本内容。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Title), null);

        /// <summary>
        /// 旋转角。
        /// </summary>
        public double TextRotation
        {
            get { return (double)GetValue(TextRotationProperty); }
            set { SetValue(TextRotationProperty, value); }
        }

        public static DependencyProperty TextRotationProperty =
            DependencyProperty.Register("TextRotation", typeof(double), typeof(Title),
            new PropertyMetadata((double)0));

        /// <summary>
        /// 下划线。
        /// </summary>
        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(Title),
            new PropertyMetadata(null));

        /// <summary>
        /// 文字方向，默认Horizontal。
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Title),
            new PropertyMetadata(Orientation.Horizontal));



        public Point TextRenderTransformOrigin
        {
            get { return (Point)GetValue(TextRenderTransformOriginProperty); }
            set { SetValue(TextRenderTransformOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextRenderTransformOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextRenderTransformOriginProperty =
            DependencyProperty.Register("TextRenderTransformOrigin", typeof(Point), typeof(Title),
            new PropertyMetadata(new Point(0.5, 0.5)));


        #endregion

        #endregion dp
    }
}
