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
    class LogTTTValueCalculator : ValueCalculator
    {
        public LogTTTValueCalculator(Axis axis)
            : base(axis)
        { }

        protected override void CaculateValue()
        {
            this.MainUnit = ValueCalculateAlgorithm.GetLogUnit(_axis.MinData, _axis.MaxData);
            this.MinValue = ValueCalculateAlgorithm.GetLogMin(this.MainUnit, _axis.MinData);
            this.MaxValue = ValueCalculateAlgorithm.GetLogMax(this.MainUnit, _axis.MaxData);
        }
    }
}
