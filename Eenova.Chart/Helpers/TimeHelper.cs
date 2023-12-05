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

namespace Eenova.Chart.Helpers
{
    public class TimeHelper
    {
        private static DateTime _time = new DateTime(2010, 1, 1, 0, 0, 0);

        public static double GetSpanTime(DateTime time)
        {
            return time.Subtract(_time).TotalSeconds;
        }

        public static DateTime GetTime(double spanTime)
        {
            TimeSpan ts = TimeSpan.FromSeconds(spanTime);
            return _time.Subtract(-ts);
        }
    }
}
