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
using System.Collections;
using System.Collections.Generic;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class DateTimeCoordConverter : CoordConverter
    {
        public DateTimeCoordConverter(Axis axis)
            : base(axis)
        { }

        public override IList<double> Convert(IEnumerable data)
        {
            if (data == null)
                return null;

            var avg = _axis.Length / (_axis.MaxValue - _axis.MinValue);//每像素的值。
            var list = new List<double>();
            foreach (var d in data)
            {
                list.Add((TimeHelper.GetSpanTime((DateTime)d) - _axis.MinValue) * avg);
            }
            return list;
        }
    }
}
