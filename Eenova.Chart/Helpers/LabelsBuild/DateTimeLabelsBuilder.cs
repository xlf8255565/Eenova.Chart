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
using Eenova.Chart.Factories;
using System;

namespace Eenova.Chart.Helpers
{
    class DateTimeLabelsBuilder : LabelsBuilder
    {
        public DateTimeLabelsBuilder(Axis axis)
            : base(axis)
        { }

        public override LabelItemCollection GetLabels(Func<object, string> format)
        {
            var ratio = (_axis.MaxValue - _axis.MinValue) / _axis.MainUnit;
            var count = (int)ratio + 1;//单元空格数
            var interval = _axis.Length / ratio;//求间距

            var lables = new LabelItemCollection();
            double spanTime;
            DateTime dt;
            for (var i = 0; i < count; i++)
            {
                spanTime = _axis.MinValue + i * _axis.MainUnit;
                dt = TimeHelper.GetTime(spanTime);
                lables.Add(LabelItem.Create(interval, format(dt)));
            }

            //补足空白不足部分。
            var space = Math.Abs(_axis.Length - ((int)ratio) * interval);//空白部分的值
            lables.Add(LabelItem.Create(space, null));
            return lables;
        }
    }
}
