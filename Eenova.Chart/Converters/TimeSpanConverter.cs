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
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 把DateTime类型转化为数值型的时间差.
    /// </summary>
    public class TimeSpanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value == null ? DateTime.Now : TimeHelper.GetTime((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? 0 : TimeHelper.GetSpanTime((DateTime)value);
        }
    }
}
