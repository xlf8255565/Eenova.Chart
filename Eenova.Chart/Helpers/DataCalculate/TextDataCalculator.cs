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


using System;
using System.Collections.Generic;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class TextDataCalculator : DataCalculator
    {

        public TextDataCalculator(Axis axis)
            : base(axis)
        { }

        public override void Calculate()
        {
            var list = new List<string>();
            string value;
            foreach (var link in _axis.DataLinks)
            {
                var data = _axis.GetLinkData(link);
                if (data == null)
                    continue;
                foreach (var d in data)
                {
                    value = (string)d;
                    if (!list.Contains(value))
                        list.Add(value);
                }
            }
            this.MaxData = 0;
            this.MinData = 0;
            this.Texts = list;
        }
    }
}
