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


using System.Collections.Generic;
using System.Windows.Media;
using System;

namespace Eenova.Chart.Helpers
{
    static class Utility
    {
        /// <summary>
        /// 比较两个double集合是否匹配。
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool IsDoubleCollectionMatch(IList<double> c1, IList<double> c2)
        {
            if (c1 == null || c2 == null)
                return false;

            if (c1.Count != c2.Count)
                return false;

            int count = c1.Count;
            for (int i = 0; i < count; i++)
            {
                if (c1[i] != c2[i])
                    return false;
            }
            return true;
        }

        public static Color ConvertFromString(string argb)
        {
            try
            {
                var alpha = argb.Substring(0, 2);
                var red = argb.Substring(2, 2);
                var green = argb.Substring(4, 2);
                var blue = argb.Substring(6, 2);

                var alphaByte = Convert.ToByte(alpha, 16);
                var redByte = Convert.ToByte(red, 16);
                var greenByte = Convert.ToByte(green, 16);
                var blueByte = Convert.ToByte(blue, 16);
                return Color.FromArgb(alphaByte, redByte, greenByte, blueByte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
