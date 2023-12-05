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

namespace Eenova.Chart.Controls
{
    public class AutoValueNumbericUpDown : Control
    {
        public AutoValueNumbericUpDown()
        {
            this.DefaultStyleKey = typeof(AutoValueNumbericUpDown);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoValueNumbericUpDown), null);


        public double ActualValue
        {
            get { return (double)GetValue(ActualValueProperty); }
            set { SetValue(ActualValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActualValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualValueProperty =
            DependencyProperty.Register("ActualValue", typeof(double), typeof(AutoValueNumbericUpDown), new PropertyMetadata(OnActualValueChanged));


        private static void OnActualValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AutoValueNumbericUpDown;
            if (!source.IsNaN)
            {
                source.Value = (double)e.NewValue;
            }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(AutoValueNumbericUpDown), new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AutoValueNumbericUpDown;
            var value = (double)e.NewValue;
            source.IsNaN = double.IsNaN(value);
        }

        internal bool IsNaN
        {
            get { return (bool)GetValue(IsNaNProperty); }
            private set { SetValue(IsNaNProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsNaN.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty IsNaNProperty =
            DependencyProperty.Register("IsNaN", typeof(bool), typeof(AutoValueNumbericUpDown), new PropertyMetadata(OnIsNaNChanged));

        private static void OnIsNaNChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AutoValueNumbericUpDown;
            source.Value = (bool)e.NewValue ? double.NaN : source.ActualValue;
        }
    }
}
