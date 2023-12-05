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
    public class SpanDateTimePicker : Control
    {
        DatePicker _dater;
        TimePicker _timer;

        public SpanDateTimePicker()
        {
            this.DefaultStyleKey = typeof(SpanDateTimePicker);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LoadControls();
        }

        private void LoadControls()
        {
            _dater = this.GetTemplateChild("Dater") as DatePicker;
            _timer = this.GetTemplateChild("Timer") as TimePicker;
        }

        public double SpanSeconds
        {
            get { return (double)GetValue(SpanSecondsProperty); }
            set { SetValue(SpanSecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpanSeconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpanSecondsProperty =
            DependencyProperty.Register("SpanSeconds", typeof(double), typeof(SpanDateTimePicker), null);



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(SpanDateTimePicker), null);


    }
}
