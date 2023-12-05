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

namespace Eenova.Chart.Factories
{
    class DataValidatorFactory
    {
        internal static DataValidator Create(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Numberic:
                    return new NumbericDataValidator();
                case DataType.DateTime:
                    return new DateTimeDataValidator();
                case DataType.Text:
                    return new TextDataValidator();
            }

            return null;
        }
    }
}
