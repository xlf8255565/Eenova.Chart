using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 图框样式预览。
    /// </summary>
    public class BorderPreview : Control
    {
        public BorderPreview()
        {
            this.DefaultStyleKey = typeof(BorderPreview);
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(BorderPreview), null);

        public Visibility SStrokeVisibility
        {
            get { return (Visibility)GetValue(SStrokeVisibilityProperty); }
            set { SetValue(SStrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeVisibilityProperty =
            DependencyProperty.Register("SStrokeVisibility", typeof(Visibility), typeof(BorderPreview), null);



        public string SStrokeStyle
        {
            get { return (string)GetValue(SStrokeStyleProperty); }
            set { SetValue(SStrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeStyleProperty =
            DependencyProperty.Register("SStrokeStyle", typeof(string), typeof(BorderPreview), null);



        public Brush SStroke
        {
            get { return (Brush)GetValue(SStrokeProperty); }
            set { SetValue(SStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeProperty =
            DependencyProperty.Register("SStroke", typeof(Brush), typeof(BorderPreview), null);



        public double SStrokeThickness
        {
            get { return (double)GetValue(SStrokeThicknessProperty); }
            set { SetValue(SStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeThicknessProperty =
            DependencyProperty.Register("SStrokeThickness", typeof(double), typeof(BorderPreview), null);

        public Visibility SBorderVisibility
        {
            get { return (Visibility)GetValue(SBorderVisibilityProperty); }
            set { SetValue(SBorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderVisibilityProperty =
            DependencyProperty.Register("SBorderVisibility", typeof(Visibility), typeof(BorderPreview), null);



        public Brush SBorderBrush
        {
            get { return (Brush)GetValue(SBorderBrushProperty); }
            set { SetValue(SBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderBrushProperty =
            DependencyProperty.Register("SBorderBrush", typeof(Brush), typeof(BorderPreview), null);

    }
}
