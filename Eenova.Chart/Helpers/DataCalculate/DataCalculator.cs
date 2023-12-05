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
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    abstract class DataCalculator
    {
        protected Axis _axis;
        public double MaxData { get; protected set; }
        public double MinData { get; protected set; }
        public IEnumerable<string> Texts { get; protected set; }

        public DataCalculator(Axis axis)
        {
            _axis = axis;
        }

        public abstract void Calculate();
    }
}
