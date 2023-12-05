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
    class LegendStyler : XmlStyler
    {
        public LegendStyler(Legend element)
            : this(element, "Legend")
        {
        }

        public LegendStyler(Legend element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => Legend.OrientationProperty);

            AddAttribute(() => Legend.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => Legend.StrokeStyleProperty);
            AddAttribute(() => Legend.StrokeThicknessProperty);
            AddAttribute(() => Legend.StrokeVisibilityProperty);
            AddAttribute(() => Legend.BorderBrushProperty, new Brush2ColorConverter());
            AddAttribute(() => Legend.BorderVisibilityProperty);

            AddAttribute(() => Legend.WidthProperty);
            AddAttribute(() => Legend.HeightProperty);
            AddAttribute(() => Legend.MarginProperty);
            AddAttribute(() => Legend.VerticalAlignmentProperty);
            AddAttribute(() => Legend.HorizontalAlignmentProperty);

            AddAttribute(() => Legend.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => Legend.BackgroundProperty, new Brush2ColorConverter());
            AddAttribute(() => Legend.FontSizeProperty);
            AddAttribute(() => Legend.FontStyleProperty);
            AddAttribute(() => Legend.FontWeightProperty);
            AddAttribute(() => Legend.FontFamilyProperty);
            AddAttribute(() => Legend.TextDecorationsProperty, new FontUnderlineConverter());

            AddAttribute(() => Legend.RenderTransformProperty, new CompositeTransformTranslateToPointConverter());
            return _xml;
        }
    }
}
