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


using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class CommonFFTValueCalculator : ValueCalculator
    {
        public CommonFFTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MinValue = _axis.MinValue;
            this.MaxValue = _axis.MaxValue;

            var differ = this.MaxValue - this.MinValue;
            this.MainUnit = ValueCalculateAlgorithm.GetUnit(_axis.DataType, differ);
        }
    }
}
