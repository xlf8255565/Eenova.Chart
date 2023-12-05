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


using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Styler
{
    class GridLineStyler : XmlStyler
    {
        public GridLineStyler(GridLine element)
            : this(element, "GridLine")
        {
        }

        public GridLineStyler(GridLine element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => GridLine.StrokeProperty,  new Brush2ColorConverter());
            AddAttribute(() => GridLine.StrokeStyleProperty);
            AddAttribute(() => GridLine.StrokeThicknessProperty);
            AddAttribute(() => GridLine.StrokeVisibilityProperty);
            AddAttribute(() => GridLine.VisibilityProperty);

            return _xml;
        }
    }

}
