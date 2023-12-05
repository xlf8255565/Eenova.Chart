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
    public class MarkupLineItem : DependencyObject, INotifyPropertyChanged
    {
        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(MarkupLineItem),
            new PropertyMetadata(new SolidColorBrush(Colors.Red), OnPropertyChangedCallback));



        public double Position
        {
            get { return (double)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(double), typeof(MarkupLineItem),
            new PropertyMetadata(0.00, OnPropertyChangedCallback));




        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(MarkupLineItem),
            new PropertyMetadata(1.00, OnPropertyChangedCallback));



        public string Style
        {
            get { return (string)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Style.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register("Style", typeof(string), typeof(MarkupLineItem),
            new PropertyMetadata(StrokeStyles.Solid, OnPropertyChangedCallback));



        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupLineItem;
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

    public class MarkupLineItemCollection : ObservableCollection<MarkupLineItem>
    {
        public new void Add(MarkupLineItem item)
        {
            if (this.Contains(item) || item == null)
                return;

            base.Add(item);
            item.PropertyChanged += new PropertyChangedEventHandler(Item_PropertyChanged);
        }

        public new bool Remove(MarkupLineItem item)
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
