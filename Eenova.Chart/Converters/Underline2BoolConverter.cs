using System;
using System.Windows;
using System.Windows.Data;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 把下划线转化为bool.
    /// </summary>
    public class Underline2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (TextDecorationCollection)value == TextDecorations.Underline;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return (bool)value ? TextDecorations.Underline : null;
        }
    }
}
