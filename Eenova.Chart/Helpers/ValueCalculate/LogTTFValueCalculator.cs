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
    class LogTTFValueCalculator : ValueCalculator
    {
        public LogTTFValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            if (_axis.MainUnit < 2)
                throw new ArgumentException("MainUnit需大于等于2");

            this.MainUnit = _axis.MainUnit;
            this.MinValue = ValueCalculateAlgorithm.GetLogMin(this.MainUnit, _axis.MinData);
            this.MaxValue = ValueCalculateAlgorithm.GetLogMax(this.MainUnit, _axis.MaxData);
        }
    }
}
