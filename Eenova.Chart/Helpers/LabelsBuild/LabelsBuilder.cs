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
using System;

namespace Eenova.Chart.Helpers
{
    abstract class LabelsBuilder
    {
        protected Axis _axis;

        public LabelsBuilder(Axis axis)
        {
            _axis = axis;
        }

        public abstract LabelItemCollection GetLabels(Func<object,string> format);
    }
}
