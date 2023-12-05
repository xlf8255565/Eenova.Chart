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

namespace Eenova.Chart.Helpers.Format
{
    class TextFormator : Formater
    {

        public override string Format(object value, string format)
        {
            string text = value == null ? "" : value.ToString();

            if (string.IsNullOrWhiteSpace(format))
            {
                return text;
            }

            var splits = format.Split(new string[] { "*|*" }, StringSplitOptions.None);
            if (splits == null || splits.Length != 2)
                return text;

            var start = splits[0].Substring(1);
            var end = splits[1].Substring(1);
            var newValue = start + text + end;

            return newValue;
        }
    }
}
