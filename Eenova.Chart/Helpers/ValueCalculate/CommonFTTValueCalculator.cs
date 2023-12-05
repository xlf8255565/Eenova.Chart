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
    class CommonFTTValueCalculator : ValueCalculator
    {
        public CommonFTTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MinValue = _axis.MinValue;

            if (_axis.MaxData > this.MinValue)
            {
                var differ = (_axis.MaxData - this.MinValue) * 1.1;
                this.MainUnit = ValueCalculateAlgorithm.GetUnit(_axis.DataType, differ);
                this.MaxValue = ValueCalculateAlgorithm.GetMax(this.MinValue, this.MainUnit, _axis.MaxData);
            }
            else if (_axis.MaxData <= this.MinValue)//如果最大数据小于最小值
            {
                var differ = _axis.MinValue - _axis.MaxData;
                this.MainUnit = ValueCalculateAlgorithm.GetUnit(_axis.DataType, differ);
                this.MaxValue = this.MinValue + this.MainUnit * 10;
            }
        }
    }
}
