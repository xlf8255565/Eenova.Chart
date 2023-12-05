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
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Factories
{
    class IntervalsBuilderFactory
    {
        public static TicksHelper Create(Axis axis)
        {
            switch (axis.DataType)
            {
                case DataType.Numberic:
                    return new NumbericTicksHelper(axis);
                case DataType.DateTime:
                    return new DateTimeTicksHelper(axis);
                case DataType.Text:
                    return new TextTicksHelper(axis);
            }

            return null;
        }
    }
}
