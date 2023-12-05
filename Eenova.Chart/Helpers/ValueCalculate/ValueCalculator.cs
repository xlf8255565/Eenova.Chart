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
    /// <summary>
    /// 根据自动最大值自动最小值等设置计算最大值最小值。
    /// </summary>
    abstract class ValueCalculator
    {
        protected Axis _axis;

        public double MinValue { get; protected set; }
        public double MaxValue { get; protected set; }
        public double MainUnit { get; protected set; }

        public ValueCalculator(Axis axis)
        {
            _axis = axis;
        }

        public void Caculate()
        {
            this.CaculateValue();
            this.ValidateValue();
        }

        protected abstract void CaculateValue();

        private void ValidateValue()
        {
            if (this.MinValue == this.MaxValue)
            {
                var value = this.MinValue;
                this.MinValue = value - this.MainUnit;
                this.MaxValue = value + this.MainUnit;
            }
        }
    }
}
