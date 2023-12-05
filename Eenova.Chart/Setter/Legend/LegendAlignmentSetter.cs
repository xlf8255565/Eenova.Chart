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
    public class LegendAlignmentSetter : BaseSetter
    {
        ILegendAlignment _pElement;

        public LegendAlignmentSetter(ILegendAlignment element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.Orientation != SOrientation)
                _pElement.Orientation = SOrientation;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.Orientation != SOrientation)
                SOrientation = _pElement.Orientation;
        }



        public Orientation SOrientation
        {
            get { return (Orientation)GetValue(SOrientationProperty); }
            set { SetValue(SOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SOrientationProperty =
            DependencyProperty.Register("SOrientation", typeof(Orientation), typeof(LegendAlignmentSetter), null);


    }
}
