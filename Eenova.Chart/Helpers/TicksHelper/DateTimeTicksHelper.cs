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
    class DateTimeTicksHelper : TicksHelper
    {
        protected override int MainCount
        {
            get { return (int)((_axis.MaxValue - _axis.MinValue) / _axis.MainUnit) + 1; }
        }

        protected override double MainTick
        {
            get { return _axis.Length * _axis.MainUnit / (_axis.MaxValue - _axis.MinValue); }
        }

        protected override int SubCount
        {
            get { return (int)((_axis.MaxValue - _axis.MinValue) / _axis.SubUnit) + 1; }
        }

        protected override double SubTick
        {
            get { return _axis.Length * _axis.SubUnit / (_axis.MaxValue - _axis.MinValue); }
        }

        public DateTimeTicksHelper(Axis axis)
            : base(axis)
        { }
    }
}
