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
    class LogFTTValueCalculator : ValueCalculator
    {
        public LogFTTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            if (_axis.MinValue <= 0)
                throw new ArgumentException("MinValue需大于0");

            this.MinValue = _axis.MinValue;
            this.MainUnit = ValueCalculateAlgorithm.GetLogUnit(this.MinValue, _axis.MaxData);
            this.MaxValue = ValueCalculateAlgorithm.GetLogMax(this.MainUnit, _axis.MaxData);
        }
    }
}
