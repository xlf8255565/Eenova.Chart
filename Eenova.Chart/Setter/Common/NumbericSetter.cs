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
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class NumbericSetter : BaseSetter
    {
        INumberic _pElement;

        public NumbericSetter(INumberic element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.Value != SValue)
                _pElement.Value = SValue;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.Value != SValue)
                SValue = _pElement.Value;
        }



        public double SValue
        {
            get { return (double)GetValue(SValueProperty); }
            set { SetValue(SValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SValueProperty =
            DependencyProperty.Register("SValue", typeof(double), typeof(NumbericSetter), null);



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NumbericSetter), null);



        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(NumbericSetter), null);



        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(NumbericSetter), null);



        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecimalPlaces.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumbericSetter), null);


    }
}
