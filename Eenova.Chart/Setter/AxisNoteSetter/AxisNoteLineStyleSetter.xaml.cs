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
    public partial class AxisNoteLineStyleSetter : BaseSetter
    {
        public AxisNoteLineStyleSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            //this.AddBindingProperty(this.nbTop, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbArrowSize, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbSideHeight, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbStart, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbEnd, NumericUpDown.ValueProperty);
        }

        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(AxisNoteLineStyleSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisNoteLineStyleSetter;

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
                    source.nbStart.Visibility = Visibility.Collapsed;
                    source.nbEnd.Visibility = Visibility.Collapsed;
                    source.tpStart.Visibility = Visibility.Collapsed;
                    source.tpEnd.Visibility = Visibility.Collapsed;
                    break;
            }
        }

    }
}
