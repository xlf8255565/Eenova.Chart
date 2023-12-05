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
using System.Windows;
using System.Text.RegularExpressions;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 把FontFamily转化为string.
    /// </summary>
    public class FontFamilyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FontFamilies.Default;
            else
            {
                var family = (FontFamily)value;
                var p="^"+family.Source+"$";
                foreach (var font in FontFamilies.Source)
                {
                    if (Regex.IsMatch(font, p, RegexOptions.IgnoreCase))
                        return font;
                }
                return FontFamilies.Default;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? new FontFamily(FontFamilies.Default) : new FontFamily(value.ToString());
        }
    }
}
