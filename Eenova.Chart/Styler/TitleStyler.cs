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
    class TitleStyler : XmlStyler
    {
        public TitleStyler(Title element)
            : this(element, "Title")
        {
        }

        public TitleStyler(Title element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => Title.TextProperty);

            AddAttribute(() => Title.OrientationProperty);
            AddAttribute(() => Title.TextRotationProperty);
            AddAttribute(() => Title.VerticalContentAlignmentProperty);
            AddAttribute(() => Title.HorizontalContentAlignmentProperty);

            AddAttribute(() => Title.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => Title.StrokeStyleProperty);
            AddAttribute(() => Title.StrokeThicknessProperty);
            AddAttribute(() => Title.StrokeVisibilityProperty);
            AddAttribute(() => Title.BorderBrushProperty, new Brush2ColorConverter());
            AddAttribute(() => Title.BorderVisibilityProperty);

            AddAttribute(() => Title.WidthProperty);
            AddAttribute(() => Title.HeightProperty);
            AddAttribute(() => Title.MarginProperty);
            AddAttribute(() => Title.VerticalAlignmentProperty);
            AddAttribute(() => Title.HorizontalAlignmentProperty);

            AddAttribute(() => Title.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => Title.BackgroundProperty, new Brush2ColorConverter());
            AddAttribute(() => Title.FontSizeProperty);
            AddAttribute(() => Title.FontStyleProperty);
            AddAttribute(() => Title.FontWeightProperty);
            AddAttribute(() => Title.FontFamilyProperty);
            AddAttribute(() => Title.TextDecorationsProperty, new FontUnderlineConverter());

            AddAttribute(() => Title.TextRenderTransformOriginProperty);
            AddAttribute(() => Title.RenderTransformProperty, new CompositeTransformTranslateToPointConverter());

            return _xml;
        }
    }
}
