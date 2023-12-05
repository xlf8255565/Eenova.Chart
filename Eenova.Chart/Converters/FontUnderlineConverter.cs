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
using System.Windows.Data;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 把下划线转化为bool.
    /// </summary>
    public class FontUnderlineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToDecoration(value);
            return ConvertToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToBool(value);
            return ConvertToDecoration(value);
        }

        private bool ConvertToBool(object value)
        {
            return (TextDecorationCollection)value == TextDecorations.Underline;
        }

        private TextDecorationCollection ConvertToDecoration(object value)
        {
            return (bool)value ? TextDecorations.Underline : null;
        }
    }
}
