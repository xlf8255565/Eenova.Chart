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
    class CommonTTFValueCalculator : ValueCalculator
    {
        public CommonTTFValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MainUnit = _axis.MainUnit;

            
            if (_axis.MinData >= 0)
            {
                var center = ValueCalculateAlgorithm.GetMax(0, this.MainUnit, _axis.MinData);
                this.MinValue = ValueCalculateAlgorithm.GetMin(center, this.MainUnit, _axis.MinData);
            }
            else
            {
                this.MinValue = ValueCalculateAlgorithm.GetMin(0, this.MainUnit, _axis.MinData);
            }

            this.MaxValue = ValueCalculateAlgorithm.GetMax(this.MinValue, this.MainUnit, _axis.MaxData);
        }
    }
}
