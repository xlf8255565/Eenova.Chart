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


using System.Collections;
using System.Collections.Generic;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    public abstract class CoordConverter
    {
        protected Axis _axis;

        public CoordConverter(Axis axis)
        {
            _axis = axis;
        }

        public abstract IList<double> Convert(IEnumerable data);

    }
}
