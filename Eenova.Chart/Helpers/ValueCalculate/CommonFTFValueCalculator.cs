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
    class CommonFTFValueCalculator : ValueCalculator
    {
        public CommonFTFValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MinValue = _axis.MinValue;
            this.MainUnit = _axis.MainUnit;
            this.MaxValue = ValueCalculateAlgorithm.GetMax(this.MinValue, this.MainUnit, _axis.MaxData);
        }
    }
}
