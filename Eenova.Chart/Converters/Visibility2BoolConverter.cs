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
    /// 把Visibility枚举转化为bool.
    /// </summary>
    public class Visibility2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToVisibility(value);
            return ConvertToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToBool(value);
            return ConvertToVisibility(value);
        }

        private bool ConvertToBool(object value)
        {
            return (Visibility)value == Visibility.Visible;
        }

        private Visibility ConvertToVisibility(object value)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
