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


using Eenova.Chart.Helpers;
using Eenova.Chart.Helpers.Format;

namespace Eenova.Chart.Factories
{
    class FormaterFactory
    {
        public static Formater Create(DataType dataType)
        {
            switch (dataType)
            {
                default:
                case DataType.Numberic:
                    return new NumbericFormator();
                case DataType.DateTime:
                    return new DateTimeFormator();
                case DataType.Text:
                    return new TextFormator();
            }
        }
    }
}
