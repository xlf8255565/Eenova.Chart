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
    class DataLinkStyler : XmlStyler
    {
        public DataLinkStyler(DataLink element)
            : this(element, "DataLink")
        {
        }

        public DataLinkStyler(DataLink element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => DataLink.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => DataLink.StrokeStyleProperty);
            AddAttribute(() => DataLink.StrokeThicknessProperty);
            AddAttribute(() => DataLink.StrokeVisibilityProperty);
            AddAttribute(() => DataLink.ShadowVisibilityProperty);

            AddAttribute(() => DataLink.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => DataLink.MarkSizeProperty);
            AddAttribute(() => DataLink.MarkTypeProperty);
            AddAttribute(() => DataLink.MarkVisibilityProperty);

            return _xml;
        }
    }

}
