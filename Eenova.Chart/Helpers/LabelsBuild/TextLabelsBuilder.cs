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
using Eenova.Chart.Factories;
using System;

namespace Eenova.Chart.Helpers
{
    class TextLabelsBuilder : LabelsBuilder
    {
        public TextLabelsBuilder(Axis axis)
            : base(axis)
        { }

        public override LabelItemCollection GetLabels(Func<object, string> format)
        {
            //当坐标轴显示文本时，所有数据均匀分布于坐标轴上。
            if (_axis.Texts == null)
                return null;

            var texts = new List<string>(_axis.Texts);
            var interval = _axis.Length / texts.Count;
            var lables = new LabelItemCollection();
            foreach (var text in texts)
            {
                lables.Add( LabelItem.Create(interval, format(text)));
            }
            return lables;
        }
    }
}
