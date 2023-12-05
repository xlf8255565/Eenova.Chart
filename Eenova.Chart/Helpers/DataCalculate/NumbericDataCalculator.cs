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
    class NumbericDataCalculator : DataCalculator
    {

        public NumbericDataCalculator(Axis axis)
            : base(axis)
        { }

        public override void Calculate()
        {
            var list = new List<double>();
            foreach (var link in _axis.DataLinks)
            {
                var data = _axis.GetLinkData(link);
                if (data == null)
                    continue;
                list.AddRange(from d in data select (double)d);
            }

            var max = list.Count == 0 ? 100 : list.Max();
            var min = list.Count == 0 ? 0 : list.Min();

            this.MaxData = _axis.IsLogarithm && max <= 0 ? 100 : max;
            this.MinData = _axis.IsLogarithm && min <= 0 ? 100 : min;
            this.Texts = null;
        }
    }
}
