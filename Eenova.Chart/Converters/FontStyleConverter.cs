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
    /// 把FontStyle是否倾斜转化为bool.
    /// </summary>
    public class FontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToStyle(value);
            return ConvertToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToBool(value);
            return ConvertToStyle(value);
        }

        private bool ConvertToBool(object value)
        {
            return (FontStyle)value == FontStyles.Italic;
        }

        private FontStyle ConvertToStyle(object value)
        {
            return (bool)value ? FontStyles.Italic : FontStyles.Normal;
        }
    }
}
