using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class TicksShowXSelector : Control
    {
        public TicksShowXSelector()
        {
            this.DefaultStyleKey = typeof(TicksShowXSelector);
        }



        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(TicksShowXSelector), null);



        public TicksShow SMainTicksShow
        {
            get { return (TicksShow)GetValue(SMainTicksShowProperty); }
            set { SetValue(SMainTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMainTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMainTicksShowProperty =
            DependencyProperty.Register("SMainTicksShow", typeof(TicksShow), typeof(TicksShowXSelector), null);



        public TicksShow SSubTicksShow
        {
            get { return (TicksShow)GetValue(SSubTicksShowProperty); }
            set { SetValue(SSubTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SSubTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SSubTicksShowProperty =
            DependencyProperty.Register("SSubTicksShow", typeof(TicksShow), typeof(TicksShowXSelector), null);



    }
}
