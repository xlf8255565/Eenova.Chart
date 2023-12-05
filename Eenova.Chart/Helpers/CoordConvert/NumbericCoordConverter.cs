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
using Eenova.Chart.Elements;
using System.Collections;

namespace Eenova.Chart.Helpers
{
    class NumbericCoordConverter : CoordConverter
    {
        public NumbericCoordConverter(Axis axis)
            : base(axis)
        { }

        public override IList<double> Convert(IEnumerable data)
        {
            if (data == null)
                return null;

            return _axis.IsLogarithm ? this.LogarithmConvert(data) : this.CommonConvert(data);
        }

        private IList<double> LogarithmConvert(IEnumerable data)
        {
            var avg = _axis.Length / Math.Log(_axis.MaxValue / _axis.MinValue, _axis.MainUnit);
            var list = new List<double>();
            foreach (var d in data)
            {
                list.Add((double)d <= 0 ? double.NaN : Math.Log((double)d / _axis.MinValue, _axis.MainUnit) * avg);
            }
            return list;
        }

        private IList<double> CommonConvert(IEnumerable data)
        {
            var avg = _axis.Length / (_axis.MaxValue - _axis.MinValue);
            var list = new List<double>();
            foreach (var d in data)
            {
                list.Add(((double)d - _axis.MinValue) * avg);
            }
            return list;
        }
    }
}
