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
    class AxisLabelsStyler : XmlStyler
    {
        public AxisLabelsStyler(AxisLabels element)
            : this(element, "Labels")
        {
        }

        public AxisLabelsStyler(AxisLabels element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(()=>AxisLabels.OrientationProperty);
            AddAttribute(() => AxisLabels.TextRotationProperty);
            AddAttribute(() => AxisLabels.VerticalContentAlignmentProperty);
            AddAttribute(() => AxisLabels.HorizontalContentAlignmentProperty);

            AddAttribute(() => AxisLabels.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => AxisLabels.BackgroundProperty, new Brush2ColorConverter());
            AddAttribute(() => AxisLabels.FontSizeProperty);
            AddAttribute(() => AxisLabels.FontStyleProperty);
            AddAttribute(() => AxisLabels.FontWeightProperty);
            AddAttribute(() => AxisLabels.FontFamilyProperty);
            AddAttribute(() => AxisLabels.TextDecorationsProperty, new FontUnderlineConverter());

            AddAttribute(() => AxisLabels.TextRenderTransformOriginProperty);

            return _xml;
        }
    }

}
