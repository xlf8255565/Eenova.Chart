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



namespace Eenova.Chart.Elements
{
    public class LabelItem
    {
        public double Interval { get; set; }
        public string Label { get; set; }

        public static LabelItem Create(double interval, string label)
        {
            return new LabelItem()
            {
                Interval = interval,
                Label = label
            };
        }
    }
}
