using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Setter
{
    public class TicksShowXSelector : TicksShowSelector
    {
        public TicksShowXSelector()
        {
            this.DefaultStyleKey = typeof(TicksShowXSelector);
        }
    }

    public class TicksShowYSelector : TicksShowSelector
    {
        public TicksShowYSelector()
        {
            this.DefaultStyleKey = typeof(TicksShowYSelector);
        }
    }

    public abstract class TicksShowSelector : Control
    {

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(TicksShowSelector), null);



        public TicksShow SMainTicksShow
        {
            get { return (TicksShow)GetValue(SMainTicksShowProperty); }
            set { SetValue(SMainTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMainTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMainTicksShowProperty =
            DependencyProperty.Register("SMainTicksShow", typeof(TicksShow), typeof(TicksShowSelector),
            new PropertyMetadata(TicksShow.BottomOrRight));



        public TicksShow SSubTicksShow
        {
            get { return (TicksShow)GetValue(SSubTicksShowProperty); }
            set { SetValue(SSubTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SSubTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SSubTicksShowProperty =
            DependencyProperty.Register("SSubTicksShow", typeof(TicksShow), typeof(TicksShowSelector),
            new PropertyMetadata(TicksShow.BottomOrRight));

    }
}
