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



namespace Eenova.Chart.Helpers
{
    class NumbericDataValidator : DataValidator
    {
        public override object Validate(object data)
        {
            if (data is double)
                return data;

            double d;
            if (double.TryParse(data.ToString(), out d))
                return d;
            return null;
        }
    }
}
