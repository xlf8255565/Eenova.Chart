using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 对齐方式选择框。包括水平对齐和竖直对齐。
    /// </summary>
    public class AlignmentComboBox : Control
    {
        public AlignmentComboBox()
        {
            this.DefaultStyleKey = typeof(AlignmentComboBox);
        }

        /// <summary>
        /// Groupbox的标题。
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(AlignmentComboBox), null);


        /// <summary>
        /// 选择的对平对齐方式。
        /// </summary>
        public HorizontalAlignment SHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(SHorizontalAlignmentProperty); }
            set { SetValue(SHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizontalAlignmentProperty =
            DependencyProperty.Register("SHorizontalAlignment", typeof(HorizontalAlignment), typeof(AlignmentComboBox), null);


        /// <summary>
        /// 选择的竖直对齐方式。
        /// </summary>
        public VerticalAlignment SVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(SVerticalAlignmentProperty); }
            set { SetValue(SVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SVerticalAlignmentProperty =
            DependencyProperty.Register("SVerticalAlignment", typeof(VerticalAlignment), typeof(AlignmentComboBox), null);


    }
}
