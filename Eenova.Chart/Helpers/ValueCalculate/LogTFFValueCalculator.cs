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
    class LogTFFValueCalculator : ValueCalculator
    {
        public LogTFFValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            if (_axis.MaxValue <= 0)
                throw new ArgumentException("MaxValue需大于0");

            if (_axis.MainUnit < 2)
                throw new ArgumentException("MainUnit需大于等于2");

            this.MaxValue = _axis.MaxValue;
            this.MainUnit = _axis.MainUnit;
            this.MinValue = ValueCalculateAlgorithm.GetLogMin(this.MainUnit, _axis.MinData);
        }
    }
}
