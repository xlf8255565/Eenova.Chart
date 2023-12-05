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
    class LogTFTValueCalculator : ValueCalculator
    {
        public LogTFTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            if (_axis.MaxValue <= 0)
                throw new ArgumentException("MaxValue需大于0");

            this.MaxValue = _axis.MaxValue;
            this.MainUnit = ValueCalculateAlgorithm.GetLogUnit(_axis.MinData, this.MaxValue);
            this.MinValue = ValueCalculateAlgorithm.GetLogMin(this.MainUnit, _axis.MinData);
        }
    }
}
