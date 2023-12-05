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
    /// 把FontWeights是否粗体转化为bool，是否是粗体。
    /// </summary>
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToWeight(value);
            return ConvertToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
                return ConvertToBool(value);
            return ConvertToWeight(value);
        }

        private bool ConvertToBool(object value)
        {
            return (FontWeight)value == FontWeights.Bold;
        }

        private FontWeight ConvertToWeight(object value)
        {
            return (bool)value ? FontWeights.Bold : FontWeights.Normal;
        }
    }
}
