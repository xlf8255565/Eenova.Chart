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
    class PlotAreaStyler : XmlStyler
    {
        XmlStyler _axisXXmlOperator;
        XmlStyler _axisY1XmlOperator;
        XmlStyler _axisY2XmlOperator;
        XmlStyler _axisY3XmlOperator;
        XmlStyler _axisY4XmlOperator;
        XmlStyler _lX1XmlOperator;
        XmlStyler _lX2XmlOperator;
        XmlStyler _lY1XmlOperator;
        XmlStyler _lY2XmlOperator;
        XmlStyler _lY3XmlOperator;
        XmlStyler _lY4XmlOperator;


        public PlotAreaStyler(PlotArea element)
            : this(element, "PlotArea")
        {
        }

        public PlotAreaStyler(PlotArea element, string header)
            : base(element, header)
        {
            _axisXXmlOperator = new AxisStyler(element.AxisX, "AxisX");
            _axisY1XmlOperator = new AxisStyler(element.AxisY1, "AxisY1");
            _axisY2XmlOperator = new AxisStyler(element.AxisY2, "AxisY2");
            _axisY3XmlOperator = new AxisStyler(element.AxisY3, "AxisY3");
            _axisY4XmlOperator = new AxisStyler(element.AxisY4, "AxisY4");

            _lX1XmlOperator = new GridLineStyler(element.GridLineX1, "GridLineX1");
            _lX2XmlOperator = new GridLineStyler(element.GridLineX2, "GridLineX2");
            _lY1XmlOperator = new GridLineStyler(element.GridLineY1, "GridLineY1");
            _lY2XmlOperator = new GridLineStyler(element.GridLineY2, "GridLineY2");
            _lY3XmlOperator = new GridLineStyler(element.GridLineY3, "GridLineY3");
            _lY4XmlOperator = new GridLineStyler(element.GridLineY4, "GridLineY4");
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => PlotArea.XAlignmentProperty);
            AddAttribute(() => PlotArea.TopVisibilityProperty);
            AddAttribute(() => PlotArea.BottomVisibilityProperty);

            AddAttribute(() => PlotArea.LengthProperty);
            AddAttribute(() => PlotArea.TopHeightProperty);
            AddAttribute(() => PlotArea.BottomHeightProperty);
            AddAttribute(() => PlotArea.MarginProperty);
            AddAttribute(() => PlotArea.VerticalAlignmentProperty);
            AddAttribute(() => PlotArea.HorizontalAlignmentProperty);

            AddAttribute(() => PlotArea.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => PlotArea.StrokeStyleProperty);
            AddAttribute(() => PlotArea.StrokeThicknessProperty);
            AddAttribute(() => PlotArea.StrokeVisibilityProperty);
            AddAttribute(() => PlotArea.BorderBrushProperty, new Brush2ColorConverter());
            AddAttribute(() => PlotArea.BorderVisibilityProperty);

            AddAttribute(() => PlotArea.RenderTransformProperty, new CompositeTransformTranslateToPointConverter());

            AddAttribute(() => PlotArea.InternalPlotYProperty);

            _xml.Add(_axisXXmlOperator.CreateXml());
            _xml.Add(_axisY1XmlOperator.CreateXml());
            _xml.Add(_axisY2XmlOperator.CreateXml());
            _xml.Add(_axisY3XmlOperator.CreateXml());
            _xml.Add(_axisY4XmlOperator.CreateXml());

            _xml.Add(_lX1XmlOperator.CreateXml());
            _xml.Add(_lX2XmlOperator.CreateXml());
            _xml.Add(_lY1XmlOperator.CreateXml());
            _xml.Add(_lY2XmlOperator.CreateXml());
            _xml.Add(_lY3XmlOperator.CreateXml());
            _xml.Add(_lY4XmlOperator.CreateXml());

            return _xml;
        }

        public override void ReadXml(XElement xml)
        {
            base.ReadXml(xml);

            _axisXXmlOperator.ReadXml(xml.Element(_axisXXmlOperator.Header));
            _axisY1XmlOperator.ReadXml(xml.Element(_axisY1XmlOperator.Header));
            _axisY2XmlOperator.ReadXml(xml.Element(_axisY2XmlOperator.Header));
            _axisY3XmlOperator.ReadXml(xml.Element(_axisY3XmlOperator.Header));
            _axisY4XmlOperator.ReadXml(xml.Element(_axisY4XmlOperator.Header));

            _lX1XmlOperator.ReadXml(xml.Element(_lX1XmlOperator.Header));
            _lX2XmlOperator.ReadXml(xml.Element(_lX2XmlOperator.Header));
            _lY1XmlOperator.ReadXml(xml.Element(_lY1XmlOperator.Header));
            _lY2XmlOperator.ReadXml(xml.Element(_lY2XmlOperator.Header));
            _lY3XmlOperator.ReadXml(xml.Element(_lY3XmlOperator.Header));
            _lY4XmlOperator.ReadXml(xml.Element(_lY4XmlOperator.Header));
        }
    }

}
