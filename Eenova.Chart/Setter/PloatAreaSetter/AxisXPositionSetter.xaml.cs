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

namespace Eenova.Chart.Setter
{
    public partial class AxisXPositionSetter : BaseSetter
    {
        public AxisXPositionSetter()
        {
            InitializeComponent();
            this.cbInternalPlotY.SelectionChanged += (s, e) =>
            {
                this.cbInternalPlotYEx.SelectedValue = this.cbInternalPlotY.SelectedValue;
            };
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.rbFixPosition, RadioButton.IsCheckedProperty);
            this.AddBindingProperty(this.cbbXAlignment, ComboBox.SelectedValueProperty);

            this.AddBindingProperty(this.cbInternalPlotY, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.nbNumericPosition, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.tbTextPosition, TextBox.TextProperty);
        }

        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(AxisXPositionSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisXPositionSetter;

            switch ((DataType)e.NewValue)
            {
                case DataType.Numberic:
                    source.nbNumericPosition.Visibility = Visibility.Visible;
                    source.tpNumericPosition.Visibility = Visibility.Collapsed;
                    source.tbTextPosition.Visibility = Visibility.Collapsed;
                    break;
                case DataType.DateTime:
                    source.nbNumericPosition.Visibility = Visibility.Collapsed;
                    source.tpNumericPosition.Visibility = Visibility.Visible;
                    source.tbTextPosition.Visibility = Visibility.Collapsed;
                    break;
                case DataType.Text:
                    source.nbNumericPosition.Visibility = Visibility.Collapsed;
                    source.tpNumericPosition.Visibility = Visibility.Collapsed;
                    source.tbTextPosition.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
