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


using System.Windows.Media;

namespace Eenova.Chart.Elements
{
    public class Alarm
    {
        public Alarm()
        { }

        public Alarm(Color color, double min, double max)
        {
            Color = color;
            Min = min;
            Max = max;
        }

        public Color Color { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}
