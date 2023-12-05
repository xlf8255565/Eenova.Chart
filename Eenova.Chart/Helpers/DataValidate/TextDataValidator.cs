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
    class TextDataValidator : DataValidator
    {
        public override object Validate(object data)
        {
            if (data == null)
                return null;
            return data.ToString();
        }
    }
}
