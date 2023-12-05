using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class DateTimeTicksSetter : NumbericTicksSetter
    {
        public DateTimeTicksSetter(INumbericTicks element)
            : base(element)
        {
        }
    }

    public class NumbericTicksSetter : BaseSetter
    {
        INumbericTicks _pElement;

        public NumbericTicksSetter(INumbericTicks element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.IsDesc != SIsDesc)
                _pElement.IsDesc = SIsDesc;

            if (_pElement.IsMinValueAuto != SIsMinValueAuto)
                _pElement.IsMinValueAuto = SIsMinValueAuto;

            if (_pElement.MinValue != SMinValue && !_pElement.IsMinValueAuto)
                _pElement.MinValue = SMinValue;

            if (_pElement.IsMaxValueAuto != SIsMaxValueAuto)
                _pElement.IsMaxValueAuto = SIsMaxValueAuto;

            if (_pElement.MaxValue != SMaxValue && !_pElement.IsMaxValueAuto)
                _pElement.MaxValue = SMaxValue;

            if (_pElement.IsMainUnitAuto != SIsMainUnitAuto)
                _pElement.IsMainUnitAuto = SIsMainUnitAuto;

            if (_pElement.MainUnit != SMainUnit && !_pElement.IsMainUnitAuto)
                _pElement.MainUnit = SMainUnit;

            if (_pElement.IsSubUnitAuto != SIsSubUnitAuto)
                _pElement.IsSubUnitAuto = SIsSubUnitAuto;

            if (_pElement.SubUnit != SSubUnit && !_pElement.IsSubUnitAuto)
                _pElement.SubUnit = SSubUnit;

            if (_pElement.IsLogarithm != SIsLogarithm)
                _pElement.IsLogarithm = SIsLogarithm;

        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.MinValue != SMinValue)
                SMinValue = _pElement.MinValue;

            if (_pElement.MaxValue != SMaxValue)
                SMaxValue = _pElement.MaxValue;

            if (_pElement.MainUnit != SMainUnit)
                SMainUnit = _pElement.MainUnit;

            if (_pElement.SubUnit != SSubUnit)
                SSubUnit = _pElement.SubUnit;

            if (_pElement.IsDesc != SIsDesc)
                SIsDesc = _pElement.IsDesc;

            if (_pElement.IsLogarithm != SIsLogarithm)
                SIsLogarithm = _pElement.IsLogarithm;

            if (_pElement.IsMainUnitAuto != SIsMainUnitAuto)
                SIsMainUnitAuto = _pElement.IsMainUnitAuto;

            if (_pElement.IsMaxValueAuto != SIsMaxValueAuto)
                SIsMaxValueAuto = _pElement.IsMaxValueAuto;

            if (_pElement.IsMinValueAuto != SIsMinValueAuto)
                SIsMinValueAuto = _pElement.IsMinValueAuto;

            if (_pElement.IsSubUnitAuto != SIsSubUnitAuto)
                SIsSubUnitAuto = _pElement.IsSubUnitAuto;
        }

        #region dp



        public bool SIsDesc
        {
            get { return (bool)GetValue(SIsDescProperty); }
            set { SetValue(SIsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsDescProperty =
            DependencyProperty.Register("SIsDesc", typeof(bool), typeof(NumbericTicksSetter), null);



        public bool SIsLogarithm
        {
            get { return (bool)GetValue(SIsLogarithmProperty); }
            set { SetValue(SIsLogarithmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsLogarithm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsLogarithmProperty =
            DependencyProperty.Register("SIsLogarithm", typeof(bool), typeof(NumbericTicksSetter), null);



        public bool SIsMinValueAuto
        {
            get { return (bool)GetValue(SIsMinValueAutoProperty); }
            set { SetValue(SIsMinValueAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsMinValueAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsMinValueAutoProperty =
            DependencyProperty.Register("SIsMinValueAuto", typeof(bool), typeof(NumbericTicksSetter), null);



        public bool SIsMaxValueAuto
        {
            get { return (bool)GetValue(SIsMaxValueAutoProperty); }
            set { SetValue(SIsMaxValueAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsMaxValueAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsMaxValueAutoProperty =
            DependencyProperty.Register("SIsMaxValueAuto", typeof(bool), typeof(NumbericTicksSetter), null);



        public bool SIsMainUnitAuto
        {
            get { return (bool)GetValue(SIsMainUnitAutoProperty); }
            set { SetValue(SIsMainUnitAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsMainUnitAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsMainUnitAutoProperty =
            DependencyProperty.Register("SIsMainUnitAuto", typeof(bool), typeof(NumbericTicksSetter), null);



        public bool SIsSubUnitAuto
        {
            get { return (bool)GetValue(SIsSubUnitAutoProperty); }
            set { SetValue(SIsSubUnitAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsSubUnitAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsSubUnitAutoProperty =
            DependencyProperty.Register("SIsSubUnitAuto", typeof(bool), typeof(NumbericTicksSetter), null);



        public double SMinValue
        {
            get { return (double)GetValue(SMinValueProperty); }
            set { SetValue(SMinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMinValueProperty =
            DependencyProperty.Register("SMinValue", typeof(double), typeof(NumbericTicksSetter), null);



        public double SMaxValue
        {
            get { return (double)GetValue(SMaxValueProperty); }
            set { SetValue(SMaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMaxValueProperty =
            DependencyProperty.Register("SMaxValue", typeof(double), typeof(NumbericTicksSetter), null);



        public double SMainUnit
        {
            get { return (double)GetValue(SMainUnitProperty); }
            set { SetValue(SMainUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMainUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMainUnitProperty =
            DependencyProperty.Register("SMainUnit", typeof(double), typeof(NumbericTicksSetter), null);



        public double SSubUnit
        {
            get { return (double)GetValue(SSubUnitProperty); }
            set { SetValue(SSubUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SSubUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SSubUnitProperty =
            DependencyProperty.Register("SSubUnit", typeof(double), typeof(NumbericTicksSetter), null);



        #endregion dp
    }
}
