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
using Eenova.Chart.Elements;

namespace Eenova.Chart.Styler
{
    class AxisNoteStyler : XmlStyler
    {
        public AxisNoteStyler(AxisNote element)
            : this(element, "AxisNote")
        {
        }

        public AxisNoteStyler(AxisNote element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            //AddAttribute(() => AxisNote.TopProperty);
            AddAttribute(() => AxisNote.ArrowSizeProperty);
            AddAttribute(() => AxisNote.SideHeightProperty);

            return _xml;
        }
    }

}
