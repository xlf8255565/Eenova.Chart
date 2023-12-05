using System;
using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class AxisLabelsPositionXSetter : AxisLabelsPositionSetter
    {
        public AxisLabelsPositionXSetter(IAxisLabelsPosition element)
            : base(element)
        {
        }
    }

    public class AxisLabelsPositionYSetter : AxisLabelsPositionSetter
    {
        public AxisLabelsPositionYSetter(IAxisLabelsPosition element)
            : base(element)
        {
        }
    }

    public abstract class AxisLabelsPositionSetter : BaseSetter
    {
        IAxisLabelsPosition _pElement;

        public AxisLabelsPositionSetter(IAxisLabelsPosition element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.LabelLocation != SLabelLocation)
                _pElement.LabelLocation = SLabelLocation;

            if (_pElement.LabelOffset != SLabelOffset)
                _pElement.LabelOffset = SLabelOffset;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.LabelLocation != SLabelLocation)
                SLabelLocation = _pElement.LabelLocation;

            if (_pElement.LabelOffset != SLabelOffset)
                SLabelOffset = _pElement.LabelOffset;
        }

        #region dp

        public AxisLocation SLabelLocation
        {
            get { return (AxisLocation)GetValue(SLabelLocationProperty); }
            set { SetValue(SLabelLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLabelLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLabelLocationProperty =
            DependencyProperty.Register("SLabelLocation", typeof(AxisLocation), typeof(AxisLabelsPositionSetter), null);



        public double SLabelOffset
        {
            get { return (double)GetValue(SLabelOffsetProperty); }
            set { SetValue(SLabelOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLabelOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLabelOffsetProperty =
            DependencyProperty.Register("SLabelOffset", typeof(double), typeof(AxisLabelsPositionSetter), null);


        #endregion dp
    }
}
