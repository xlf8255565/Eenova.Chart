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
using System.Windows.Data;

namespace Eenova.Chart.Styler
{
    class ChartStyler : XmlStyler
    {
        Chart.Elements.Chart _element;

        public ChartStyler(Chart.Elements.Chart element)
            : this(element, "Chart")
        {
        }

        public ChartStyler(Chart.Elements.Chart element, string header)
            : base(element, header)
        {
            _element = element;
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => Chart.Elements.Chart.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => Chart.Elements.Chart.StrokeStyleProperty);
            AddAttribute(() => Chart.Elements.Chart.StrokeThicknessProperty);
            AddAttribute(() => Chart.Elements.Chart.StrokeVisibilityProperty);
            AddAttribute(() => Chart.Elements.Chart.BorderBrushProperty, new Brush2ColorConverter());
            AddAttribute(() => Chart.Elements.Chart.BorderVisibilityProperty);

            AddAttribute(() => Chart.Elements.Chart.WidthProperty);
            AddAttribute(() => Chart.Elements.Chart.HeightProperty);
            AddAttribute(() => Chart.Elements.Chart.MarginProperty);
            AddAttribute(() => Chart.Elements.Chart.VerticalAlignmentProperty);
            AddAttribute(() => Chart.Elements.Chart.HorizontalAlignmentProperty);

            AddAttribute(() => Chart.Elements.Chart.RenderTransformProperty, new CompositeTransformTranslateToPointConverter());

            _xml.Add(this.CreateTitleNotesXml());
            _xml.Add(this.CreatePlotAreasXml());
            _xml.Add(this.CreateLegendsXml());

            return _xml;
        }

        public override void ReadXml(XElement xml)
        {
            base.ReadXml(xml);
            this.ReadTitleNotesXml();
            this.ReadPlotAreasXml();
            this.ReadLegendsXml();
        }

        private XElement CreateTitleNotesXml()
        {
            var xml = new XElement("TitleNotes");
            XmlStyler styler;
            foreach (var item in _element.TitleNotes)
            {
                styler = new TitleNoteStyler(item);
                xml.Add(styler.CreateXml());
            }
            return xml;
        }

        private XElement CreatePlotAreasXml()
        {
            var xml = new XElement("PlotAreas");
            XmlStyler styler;
            foreach (var item in _element.PlotAreas)
            {
                styler = new PlotAreaStyler(item);
                xml.Add(styler.CreateXml());
            }
            return xml;
        }

        private XElement CreateLegendsXml()
        {
            var element = new XElement("Legends");
            XmlStyler styler;
            XElement xml;
            foreach (var item in _element.Legends)
            {
                styler = new LegendStyler(item);
                xml = styler.CreateXml();
                xml.SetAttributeValue("BindingIndex", this.GetBindingPlotAreaIndex(item));
                element.Add(xml);
            }
            return element;
        }

        private int GetBindingPlotAreaIndex(Legend legend)
        {
            var count = _element.PlotAreas.Count;
            PlotArea plotArea;
            for (int i = 0; i < count; i++)
            {
                plotArea = _element.PlotAreas[i];
                if (plotArea.DataLinks == legend.DataLinks)
                    return i;
            }
            return -1;
        }

        private void ReadTitleNotesXml()
        {
            var xmls = _xml.Element("TitleNotes").Elements();
            XmlStyler styler;
            TitleNote titleNote;
            int index = 0;
            foreach (var node in xmls)
            {
                if (index >= _element.TitleNotes.Count)
                    _element.TitleNotes.Add(new TitleNote());
                titleNote = _element.TitleNotes[index];
                styler = new TitleNoteStyler(titleNote);
                styler.ReadXml(node);
                index++;
            }
        }

        private void ReadPlotAreasXml()
        {
            var xmls = _xml.Element("PlotAreas").Elements();
            XmlStyler styler;
            PlotArea plotArea;
            int index = 0;
            foreach (var node in xmls)
            {
                if (index >= _element.PlotAreas.Count)
                    _element.PlotAreas.Add(new PlotArea());
                plotArea = _element.PlotAreas[index];
                styler = new PlotAreaStyler(plotArea);
                styler.ReadXml(node);
                index++;
            }
        }

        private void ReadLegendsXml()
        {
            var xmls = _xml.Element("Legends").Elements();
            XmlStyler styler;
            Legend legend;
            int index = 0;
            XAttribute bindingAttribute;
            foreach (var node in xmls)
            {
                if (index >= _element.Legends.Count)
                    _element.Legends.Add(new Legend());
                legend = _element.Legends[index];
                styler = new LegendStyler(legend);
                styler.ReadXml(node);
                bindingAttribute = node.Attribute("BindingIndex");
                if (bindingAttribute != null && !string.IsNullOrWhiteSpace(bindingAttribute.Value))
                    this.BindingPlotArea(legend, int.Parse(bindingAttribute.Value));
                index++;
            }
        }

        private void BindingPlotArea(Legend legend, int index)
        {
            if (index < 0 || index >= _element.PlotAreas.Count)
                return;

            var plotArea = _element.PlotAreas[index];
            legend.DataLinks = plotArea.DataLinks;
            //legend.SetBinding(Legend.DataLinksProperty, new Binding("DataLinks") { Source = plotArea });
        }
    }
}
