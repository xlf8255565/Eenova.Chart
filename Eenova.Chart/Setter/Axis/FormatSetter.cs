using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class NumbericFormatSetter : FormatSetter
    {
        public NumbericFormatSetter(IFormat element)
            : base(element)
        {
        }
    }

    public class DateTimeFormatSetter : FormatSetter
    {
        public DateTimeFormatSetter(IFormat element)
            : base(element)
        {
        }
    }

    public class TextFormatSetter : FormatSetter
    {
        public TextFormatSetter(IFormat element)
            : base(element)
        {
        }
    }

    public abstract class FormatSetter : BaseSetter
    {
        IFormat _pElement;

        public FormatSetter(IFormat element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.Format != SFormat)
                _pElement.Format = SFormat;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.Format != SFormat)
                SFormat = _pElement.Format;
        }

        public string SFormat
        {
            get { return (string)GetValue(SFormatProperty); }
            set { SetValue(SFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SFormatProperty =
            DependencyProperty.Register("SFormat", typeof(string), typeof(FormatSetter), null);

    }
}
