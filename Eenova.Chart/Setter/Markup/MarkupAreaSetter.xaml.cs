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
using Eenova.Chart.Elements;

namespace Eenova.Chart.Setter
{
    public partial class MarkupAreaSetter : BaseSetter
    {
        public MarkupAreaSetter()
        {
            InitializeComponent();
            this.nbStart.Minimum = -214748364700000;
            this.nbEnd.Maximum = 214748364700000;
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbVisibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.slOpacity, Slider.ValueProperty);
        }

        public override void Load()
        {
            base.Load();

            var item = this.lbItems.SelectedItem;
            this.lbItems.SelectedItem = null;
            this.lbItems.SelectedItem = item;
        }

        public override void Apply()
        {
            var item = this.lbItems.SelectedItem as MarkupAreaItem;
            if (item != null)
            {
                var brush = this.cpBrush.SelectedBrush;
                var start = this.nbStart.Value;
                var end = this.nbEnd.Value;
                item.Brush = brush;
                item.Start = start;
                item.End = end;
                this.lbItems.SelectedItem = item;
            }

            base.Apply();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = this.lbItems.SelectedItem as MarkupAreaItem;
            var items = this.lbItems.ItemsSource as MarkupAreaItemCollection;
            if (items == null || item == null)
                return;
            items.Remove(item);
            this.lbItems.SelectedItem = items.Count > 0 ? items[0] : null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var items = this.lbItems.ItemsSource as MarkupAreaItemCollection;
            if (items == null)
                return;
            var item = new MarkupAreaItem
            {
                Brush = this.cpBrush.SelectedBrush,
                Start = this.nbStart.Value,
                End = this.nbEnd.Value
            };
            items.Add(item);
            this.lbItems.SelectedItem = item;
        }


        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(MarkupAreaSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupAreaSetter;

            source.LayoutSetter.Visibility = Visibility.Visible;
            source.LayoutNotSupport.Visibility = Visibility.Collapsed;

            switch ((DataType)e.NewValue)
            {
                case DataType.Numberic:
                    source.nbStart.Visibility = Visibility.Visible;
                    source.nbEnd.Visibility = Visibility.Visible;
                    source.tpStart.Visibility = Visibility.Collapsed;
                    source.tpEnd.Visibility = Visibility.Collapsed;
                    break;
                case DataType.DateTime:
                    source.nbStart.Visibility = Visibility.Collapsed;
                    source.nbEnd.Visibility = Visibility.Collapsed;
                    source.tpStart.Visibility = Visibility.Visible;
                    source.tpEnd.Visibility = Visibility.Visible;
                    break;
                case DataType.Text:
                    source.LayoutSetter.Visibility = Visibility.Collapsed;
                    source.LayoutNotSupport.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
