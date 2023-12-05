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


using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Eenova.Chart.Elements
{
    public class MarkupAreaItem : DependencyObject, INotifyPropertyChanged
    {
        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(MarkupAreaItem),
            new PropertyMetadata(new SolidColorBrush(Colors.Orange), OnPropertyChangedCallback));


        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Start.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(double), typeof(MarkupAreaItem),
            new PropertyMetadata(0.00, OnPropertyChangedCallback));


        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for End.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(double), typeof(MarkupAreaItem),
            new PropertyMetadata(10.00, OnPropertyChangedCallback));


        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupAreaItem;
            source.OnPropertyChanged("Property");
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged

    }

    public class MarkupAreaItemCollection : ObservableCollection<MarkupAreaItem>
    {
        public new void Add(MarkupAreaItem item)
        {
            if (this.Contains(item) || item == null)
                return;

            base.Add(item);
            item.PropertyChanged += new PropertyChangedEventHandler(Item_PropertyChanged);
        }

        public new bool Remove(MarkupAreaItem item)
        {
            bool result = base.Remove(item);
            if (result)
            {
                item.PropertyChanged -= new PropertyChangedEventHandler(Item_PropertyChanged);
            }
            return result;
        }

        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
