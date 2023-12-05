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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Eenova.Chart.Setter
{
    public partial class AxisYPositionSetter : BaseSetter
    {
        public AxisYPositionSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.rbFixPositionY1, RadioButton.IsCheckedProperty);
            this.AddBindingProperty(this.nbNumericPositionY1, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.tbTextPositionY1, TextBox.TextProperty);

            this.AddBindingProperty(this.rbFixPositionY2, RadioButton.IsCheckedProperty);
            this.AddBindingProperty(this.nbNumericPositionY2, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.tbTextPositionY2, TextBox.TextProperty);

            this.AddBindingProperty(this.rbFixPositionY3, RadioButton.IsCheckedProperty);
            this.AddBindingProperty(this.nbNumericPositionY3, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.tbTextPositionY3, TextBox.TextProperty);

            this.AddBindingProperty(this.rbFixPositionY4, RadioButton.IsCheckedProperty);
            this.AddBindingProperty(this.nbNumericPositionY4, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.tbTextPositionY4, TextBox.TextProperty);
        }

        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(AxisYPositionSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisYPositionSetter;

            switch ((DataType)e.NewValue)
            {
                case DataType.Numberic:
                    source.nbNumericPositionY1.Visibility = Visibility.Visible;
                    source.tpNumericPositionY1.Visibility = Visibility.Collapsed;
                    source.tbTextPositionY1.Visibility = Visibility.Collapsed;
                    break;
                case DataType.DateTime:
                    source.nbNumericPositionY1.Visibility = Visibility.Collapsed;
                    source.tpNumericPositionY1.Visibility = Visibility.Visible;
                    source.tbTextPositionY1.Visibility = Visibility.Collapsed;
                    break;
                case DataType.Text:
                    source.nbNumericPositionY1.Visibility = Visibility.Collapsed;
                    source.tpNumericPositionY1.Visibility = Visibility.Collapsed;
                    source.tbTextPositionY1.Visibility = Visibility.Visible;
                    break;
            }
        }

    }
}
