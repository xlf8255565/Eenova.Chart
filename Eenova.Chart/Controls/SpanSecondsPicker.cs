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


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class SpanSecondsPicker : Control
    {

        NumericUpDown _valueBox;
        ComboBox _unitComboBox;

        public SpanSecondsPicker()
        {
            this.DefaultStyleKey = typeof(SpanSecondsPicker);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LoadControls();
            this.InitComboBox();
            this.InitEvents();
            this.SetValue();
        }

        private void LoadControls()
        {
            _valueBox = this.GetTemplateChild("ValueBox") as NumericUpDown;
            _unitComboBox = this.GetTemplateChild("UnitComboBox") as ComboBox;
        }

        private void InitComboBox()
        {
            if (_unitComboBox == null)
                return;

            var dict = new Dictionary<double, string>();
            dict.Add(1, "秒");
            dict.Add(60, "分");
            dict.Add(60 * 60, "小时");
            dict.Add(60 * 60 * 24, "天");

            _unitComboBox.ItemsSource = dict;
            _unitComboBox.DisplayMemberPath = "Value";
            _unitComboBox.SelectedValuePath = "Key";
            _unitComboBox.SelectedIndex = 0;
        }

        private void InitEvents()
        {
            _valueBox.ValueChanged += new RoutedPropertyChangedEventHandler<double>(_valueBox_ValueChanged);
            _unitComboBox.SelectionChanged += new SelectionChangedEventHandler(_unitComboBox_SelectionChanged);
        }

        private void SetValue()
        {
            if (_valueBox != null)
                _valueBox.Value = SpanSeconds / (double)(_unitComboBox.SelectedValue);
        }

        void _unitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetValue();
        }


        void _valueBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SpanSeconds = _valueBox.Value * (double)_unitComboBox.SelectedValue;
        }


        public double SpanSeconds
        {
            get { return (double)GetValue(SpanSecondsProperty); }
            set { SetValue(SpanSecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpanSeconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpanSecondsProperty =
            DependencyProperty.Register("SpanSeconds", typeof(double), typeof(SpanSecondsPicker),
            new PropertyMetadata(OnSpanSecondsChanged));


        private static void OnSpanSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as SpanSecondsPicker;
            source.SetValue();
        }
    }
}
