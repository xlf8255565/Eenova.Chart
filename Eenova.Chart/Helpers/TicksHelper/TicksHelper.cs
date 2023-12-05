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


using System.Collections.ObjectModel;
using Eenova.Chart.Elements;
using System.Collections.Generic;

namespace Eenova.Chart.Helpers
{
    abstract class TicksHelper
    {
        protected Axis _axis;

        protected abstract int MainCount { get; }
        protected abstract double MainTick { get; }
        protected abstract int SubCount { get; }
        protected abstract double SubTick { get; }

        public TicksHelper(Axis axis)
        {
            _axis = axis;
        }

        public virtual IList<double> GetMainTicks()
        {
            var ticks = new List<double>();
            var count = this.MainCount;
            var tick = this.MainTick;
            for (var i = 0; i < count; i++)
            {
                ticks.Add(i * tick);
            }
            ticks.Add(_axis.Length);
            return ticks;
        }

        public virtual IList<double> GetSubTicks()
        {
            var ticks = new List<double>();
            var count = this.SubCount;
            var tick = this.SubTick;
            for (var i = 0; i < count; i++)
            {
                ticks.Add(i * tick);
            }
            ticks.Add(_axis.Length);
            return ticks;
        }
    }
}
