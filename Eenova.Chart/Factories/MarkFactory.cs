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


using System.Windows.Data;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Factories
{
    class MarkFactory
    {
        public static Mark Create(DataLink element)
        {
            var mark = new Mark();

            var b = new Binding("MarkVisibility");
            b.Source = element;
            mark.SetBinding(Mark.VisibilityProperty, b);

            b = new Binding("MarkSize");
            b.Source = element;
            mark.SetBinding(Mark.HeightProperty, b);

            b = new Binding("MarkSize");
            b.Source = element;
            mark.SetBinding(Mark.WidthProperty, b);

            b = new Binding("MarkBrush");
            b.Source = element;
            mark.SetBinding(Mark.ForegroundProperty, b);

            b = new Binding("MarkType");
            b.Source = element;
            mark.SetBinding(Mark.MarkTypeProperty, b);

            return mark;
        }
    }
}
