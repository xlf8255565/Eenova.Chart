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
    class TitleNoteStyler : XmlStyler
    {
        public TitleNoteStyler(TitleNote element)
            : this(element, "Title")
        {
        }

        public TitleNoteStyler(TitleNote element, string header)
            : base(element, header)
        {
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => TitleNote.TextProperty);

            AddAttribute(() => TitleNote.OrientationProperty);
            AddAttribute(() => TitleNote.TextRotationProperty);
            AddAttribute(() => TitleNote.VerticalContentAlignmentProperty);
            AddAttribute(() => TitleNote.HorizontalContentAlignmentProperty);

            AddAttribute(() => TitleNote.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => TitleNote.StrokeStyleProperty);
            AddAttribute(() => TitleNote.StrokeThicknessProperty);
            AddAttribute(() => TitleNote.StrokeVisibilityProperty);
            AddAttribute(() => TitleNote.BorderBrushProperty, new Brush2ColorConverter());
            AddAttribute(() => TitleNote.BorderVisibilityProperty);

            AddAttribute(() => TitleNote.WidthProperty);
            AddAttribute(() => TitleNote.HeightProperty);
            AddAttribute(() => TitleNote.MarginProperty);
            AddAttribute(() => TitleNote.VerticalAlignmentProperty);
            AddAttribute(() => TitleNote.HorizontalAlignmentProperty);

            AddAttribute(() => TitleNote.ForegroundProperty, new Brush2ColorConverter());
            AddAttribute(() => TitleNote.BackgroundProperty, new Brush2ColorConverter());
            AddAttribute(() => TitleNote.FontSizeProperty);
            AddAttribute(() => TitleNote.FontStyleProperty);
            AddAttribute(() => TitleNote.FontWeightProperty);
            AddAttribute(() => TitleNote.FontFamilyProperty);
            AddAttribute(() => TitleNote.TextDecorationsProperty, new FontUnderlineConverter());

            AddAttribute(() => TitleNote.TextRenderTransformOriginProperty);
            AddAttribute(() => TitleNote.RenderTransformProperty, new CompositeTransformTranslateToPointConverter());

            return _xml;
        }
    }

}
