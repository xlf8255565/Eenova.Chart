using System.Windows;
using System.Windows.Controls;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Setter
{
    public partial class MarkupAreaExSetter : BaseSetter
    {
        public MarkupAreaExSetter()
        {
            InitializeComponent();
            this.nbStart.Minimum = -214748364700000;
            this.nbEnd.Maximum = 214748364700000;
            this.nbStartEx.Minimum = -214748364700000;
            this.nbEndEx.Maximum = 214748364700000;
            this.cbItemVisible.IsChecked = true;
            this.cbAuto.IsChecked = true;
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
                var isAutoExtend = this.cbAuto.IsChecked ?? false;
                var startEx = this.nbStartEx.Value;
                var endEx = this.nbEndEx.Value;
                var isVisible = this.cbItemVisible.IsChecked ?? false;
                item.Brush = brush;
                item.Start = start;
                item.End = end;
                item.IsAutoExtend = isAutoExtend;
                item.StartEx = startEx;
                item.EndEx = endEx;
                item.IsVisible = isVisible;
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
                End = this.nbEnd.Value,
                IsAutoExtend = this.cbAuto.IsChecked ?? false,
                StartEx = this.nbStartEx.Value,
                EndEx = this.nbEndEx.Value,
                IsVisible = this.cbItemVisible.IsChecked ?? false
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
            DependencyProperty.Register("DataType", typeof(DataType), typeof(MarkupAreaExSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupAreaExSetter;

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




        public DataType DataTypeEx
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTypeEx.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeExProperty =
            DependencyProperty.Register("DataTypeEx", typeof(DataType), typeof(MarkupAreaExSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeExChanged));

        private static void OnDataTypeExChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupAreaExSetter;

            source.LayoutExtend.Visibility = Visibility.Visible;

            switch ((DataType)e.NewValue)
            {
                case DataType.Numberic:
                    source.nbStartEx.Visibility = Visibility.Visible;
                    source.nbEndEx.Visibility = Visibility.Visible;
                    source.tpStartEx.Visibility = Visibility.Collapsed;
                    source.tpEndEx.Visibility = Visibility.Collapsed;
                    break;
                case DataType.DateTime:
                    source.nbStartEx.Visibility = Visibility.Collapsed;
                    source.nbEndEx.Visibility = Visibility.Collapsed;
                    source.tpStartEx.Visibility = Visibility.Visible;
                    source.tpEndEx.Visibility = Visibility.Visible;
                    break;
                case DataType.Text:
                    source.cbAuto.IsChecked = true;
                    source.LayoutExtend.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
