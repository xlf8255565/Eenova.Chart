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
using System.Collections.Generic;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Elements
{
    public class AlarmGroup
    {
        public bool NeedDraw { get; set; }

        public AlarmGroup()
        {
            this.NeedDraw = true;
            this.Alarms = new List<Alarm>();
        }

        public AlarmGroup(double deep, DateTime? startDate, DateTime? endDate)
        {
            this.NeedDraw = true;
            this.Alarms = new List<Alarm>();
            this.Deep = deep;
            if (startDate == null || endDate == null)
            {
                this.NeedDraw = false;
            }
            else
            {
                this.Start = TimeHelper.GetSpanTime(startDate.Value);
                this.End = TimeHelper.GetSpanTime(endDate.Value);
            }
        }

        public IList<Alarm> Alarms { get; set; }
        public double Deep { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
    }
}
