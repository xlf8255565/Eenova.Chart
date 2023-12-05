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
    class TextCoordConverter : CoordConverter
    {
        public TextCoordConverter(Axis axis)
            : base(axis)
        { }

        public override IList<double> Convert(IEnumerable data)
        {
            if (data == null || _axis.Texts == null)
                return null;

            var labels = new List<string>(_axis.Texts);
            if (labels.Count == 0)
                return null;

            int index = -1;
            double avg = _axis.Length / labels.Count;
            var list = new List<double>();
            foreach (var text in data)
            {
                index = text == null ? -1 : labels.IndexOf(text.ToString());
                list.Add(index < 0 ? double.NaN : avg * (index + 0.5));
            }

            return list;
        }
    }
}
