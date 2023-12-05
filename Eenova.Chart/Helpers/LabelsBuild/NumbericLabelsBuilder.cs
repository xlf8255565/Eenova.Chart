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
using Eenova.Chart.Elements;
using Eenova.Chart.Factories;

namespace Eenova.Chart.Helpers
{
    class NumbericLabelsBuilder : LabelsBuilder
    {
        public NumbericLabelsBuilder(Axis axis)
            : base(axis)
        { }

        public override LabelItemCollection GetLabels(Func<object, string> format)
        {
            var ratio = this.GetRatio();
            var count = (int)ratio + 1;//单元空格数
            var interval = _axis.Length / ratio;//求间距

            var lables = new LabelItemCollection();
            for (var i = 0; i < count; i++)
            {
                lables.Add(LabelItem.Create(interval, format(this.GetLabel(i))));
            }

            //补足空白不足部分。
            var space = Math.Abs(_axis.Length - ((int)ratio) * interval);//空白部分的值
            lables.Add(LabelItem.Create(space, null));
            return lables;
        }

        private double GetRatio()
        {
            if (_axis.IsLogarithm)
                return Math.Log(_axis.MaxValue / _axis.MinValue, _axis.MainUnit);
            else
                return (_axis.MaxValue - _axis.MinValue) / _axis.MainUnit;
        }

        private double GetLabel(int index)
        {
            if (_axis.IsLogarithm)
                return Math.Pow(_axis.MainUnit, index) * _axis.MinValue;
            else
                return _axis.MinValue + index * _axis.MainUnit;
        }
    }
}
