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
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class LogFFTValueCalculator : ValueCalculator
    {
        public LogFFTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            if (_axis.MaxValue <= 0)
                throw new ArgumentException("MaxValue需大于0");

            if (_axis.MinValue <= 0)
                throw new ArgumentException("MinValue需大于0");

            this.MinValue = _axis.MinValue;
            this.MaxValue = _axis.MaxValue;
            this.MainUnit = ValueCalculateAlgorithm.GetLogUnit(this.MinValue, this.MaxValue);
        }
    }
}
