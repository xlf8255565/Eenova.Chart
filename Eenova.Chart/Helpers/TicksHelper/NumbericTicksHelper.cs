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

namespace Eenova.Chart.Helpers
{
    class NumbericTicksHelper : DateTimeTicksHelper
    {
        protected override int MainCount
        {
            get
            {
                return !_axis.IsLogarithm ? base.MainCount :
                    (int)Math.Log(_axis.MaxValue / _axis.MinValue, _axis.MainUnit) + 1;
            }
        }

        protected override double MainTick
        {
            get
            {
                return !_axis.IsLogarithm ? base.MainTick :
                    _axis.Length / Math.Log(_axis.MaxValue / _axis.MinValue, _axis.MainUnit);
            }
        }

        protected override int SubCount
        {
            get { return !_axis.IsLogarithm ? base.SubCount : 0; }
        }

        protected override double SubTick
        {
            get { return !_axis.IsLogarithm ? base.SubTick : 0; }
        }

        public NumbericTicksHelper(Axis axis)
            : base(axis)
        { }

        public override IList<double> GetMainTicks()
        {
            if (_axis.IsLogarithm)
            {
                if (_axis.MinValue <= 0) throw new ArgumentException("对数刻度最小值需大于0");
                if (_axis.MainUnit < 2) throw new ArgumentException("对数值需大于等于2");
            }
            return base.GetMainTicks();
        }

        public override IList<double> GetSubTicks()
        {
            return _axis.IsLogarithm ? null : base.GetSubTicks();
        }
    }
}
