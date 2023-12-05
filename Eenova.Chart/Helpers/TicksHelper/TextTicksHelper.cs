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


using System.Collections.Generic;
using System.Linq;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class TextTicksHelper : TicksHelper
    {
        protected override int MainCount
        {
            get { return this.GetCount() + 1; }
        }

        protected override double MainTick
        {
            get { return _axis.Length / this.GetCount(); }
        }

        protected override int SubCount
        {
            get { return this.GetCount() * 2 + 1; }
        }

        protected override double SubTick
        {
            get { return this.MainTick / 2; }
        }

        public TextTicksHelper(Axis axis)
            : base(axis)
        { }

        public override IList<double> GetMainTicks()
        {
            return _axis.Texts == null ? null : base.GetMainTicks();
        }

        public override IList<double> GetSubTicks()
        {
            return _axis.Texts == null ? null : base.GetSubTicks();
        }

        private int GetCount()
        {
            //为什么要-2，忘了。
            return _axis.Texts == null ? -2 : _axis.Texts.Count();
        }
    }
}
