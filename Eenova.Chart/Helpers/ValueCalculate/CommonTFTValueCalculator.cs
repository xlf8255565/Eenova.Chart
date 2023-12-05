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
    class CommonTFTValueCalculator : ValueCalculator
    {
        public CommonTFTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MaxValue = _axis.MaxValue;

            if (this.MaxValue <= _axis.MinData)
            {
                var differ = _axis.MaxData - _axis.MinValue;
                this.MainUnit = ValueCalculateAlgorithm.GetUnit(_axis.DataType, differ);
                this.MinValue = this.MaxValue - this.MainUnit * 10;
            }
            else
            {
                var differ = (this.MaxValue - _axis.MinData) * 1.1;
                this.MainUnit = ValueCalculateAlgorithm.GetUnit(_axis.DataType, differ);
                this.MinValue = ValueCalculateAlgorithm.GetMin(this.MaxValue, this.MainUnit, _axis.MinData);
            }
        }
    }
}
