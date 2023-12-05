using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 图案背景色选择。
    /// </summary>
    public class BorderSelector : Control
    {
        public BorderSelector()
        {
            this.DefaultStyleKey = typeof(BorderSelector);
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(BorderSelector), null);

        public Visibility SBorderVisibility
        {
            get { return (Visibility)GetValue(SBorderVisibilityProperty); }
            set { SetValue(SBorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderVisibilityProperty =
            DependencyProperty.Register("SBorderVisibility", typeof(Visibility), typeof(BorderSelector), null);



        public Brush SBorderBrush
        {
            get { return (Brush)GetValue(SBorderBrushProperty); }
            set { SetValue(SBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderBrushProperty =
            DependencyProperty.Register("SBorderBrush", typeof(Brush), typeof(BorderSelector), null);

    }
}
