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
    class CoordConverterFactory
    {
        public static CoordConverter Create(Axis axis)
        {
            switch (axis.DataType)
            {
                default:
                case DataType.Numberic:
                    return new NumbericCoordConverter(axis);
                case DataType.DateTime:
                    return new DateTimeCoordConverter(axis);
                case DataType.Text:
                    return new TextCoordConverter(axis);
            }
        }
    }
}
