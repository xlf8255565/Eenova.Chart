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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Eenova.Chart.Behaviors
{
    public class AutoDecimalPlacesBehavior : Behavior<NumericUpDown>
    {
        public AutoDecimalPlacesBehavior()
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.ValueChanging += new RoutedPropertyChangingEventHandler<double>(AssociatedObject_ValueChanging);
            this.AssociatedObject.ValueChanged += new RoutedPropertyChangedEventHandler<double>(AssociatedObject_ValueChanged);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.ValueChanging -= new RoutedPropertyChangingEventHandler<double>(AssociatedObject_ValueChanging);
            this.AssociatedObject.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(AssociatedObject_ValueChanged);
        }

        void AssociatedObject_ValueChanging(object sender, RoutedPropertyChangingEventArgs<double> e)
        {
            var values = e.NewValue.ToString().Split('.');
            this.AssociatedObject.DecimalPlaces = values.Length == 2 ? Math.Min(values[1].Length, 15) : 0;
        }

        void AssociatedObject_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var values = e.NewValue.ToString().Split('.');
            this.AssociatedObject.DecimalPlaces = values.Length == 2 ? Math.Min(values[1].Length, 15) : 0;
        }
    }
}
