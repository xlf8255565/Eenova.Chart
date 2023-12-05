using System;
using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class TextTicksSetter : BaseSetter
    {
        ITextTicks _pElement;

        public TextTicksSetter(ITextTicks element)
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

        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.IsDesc != SIsDesc)
                SIsDesc = _pElement.IsDesc;
        }

        public bool SIsDesc
        {
            get { return (bool)GetValue(SIsDescProperty); }
            set { SetValue(SIsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsDescProperty =
            DependencyProperty.Register("SIsDesc", typeof(bool), typeof(TextTicksSetter), null);

    }
}
