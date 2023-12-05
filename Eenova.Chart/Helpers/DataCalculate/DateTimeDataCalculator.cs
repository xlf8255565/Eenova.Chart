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
using System.Linq;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class DateTimeDataCalculator : DataCalculator
    {

        public DateTimeDataCalculator(Axis axis)
            : base(axis)
        { }

        public override void Calculate()
        {
            var list = new List<double>();
            IList<object> data;
            foreach (var link in _axis.DataLinks)
            {
                data = _axis.GetLinkData(link);
                if (data == null)
                    continue;
                list.AddRange(from d in data select TimeHelper.GetSpanTime((DateTime)d));
            }
            this.MaxData = list.Count == 0 ? 3600 * 24 * 7 : list.Max();
            this.MinData = list.Count == 0 ? 0 : list.Min();
            this.Texts = null;
        }
    }
}
