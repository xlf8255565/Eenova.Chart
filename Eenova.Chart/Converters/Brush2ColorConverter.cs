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
using System.Windows.Data;
using System.Windows.Media;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 把Brush转化为SolidColorBrush获取单一颜色.
    /// </summary>
    public class Brush2ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush == null)
            {
                return Colors.Transparent;
            }

            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.Transparent);
            }

            return new SolidColorBrush((Color)value);
        }
    }
}
