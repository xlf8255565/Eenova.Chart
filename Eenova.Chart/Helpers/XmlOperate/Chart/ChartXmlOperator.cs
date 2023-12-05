using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Interfaces;
using System.Xml.Linq;
using Eenova.Chart.Elements;
using System.Windows.Data;
using Eenova.Chart.Converters;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class ChartXmlOperator : XmlOperator
    {
        const string TitleNotes = "TitleNotes";
        const string TitleNote = "TitleNote";
        const string PlotAreas = "PlotAreas";
        const string PlotArea = "PlotArea";
        const string Legends = "Legends";
        const string Legend = "Legend";
        const string BindingIndex = "BindingIndex";

        IChart _pElement;
        XmlOperator _borderXmlOperator;
        XmlOperator _positionXmlOperator;

        public ChartXmlOperator(IChart element)
            : this(element, "Chart")
        {
        }

        public ChartXmlOperator(IChart element, string header)
            : base(header)
        {
            _borderXmlOperator = new BorderXmlOperator(element);
            _positionXmlOperator = new PositionXmlOperator(element);
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_positionXmlOperator.CreateXml());
            element.Add(_borderXmlOperator.CreateXml());
            element.Add(this.CreateTitleNotesXml());
            element.Add(this.CreatePlotAreasXml());
            element.Add(this.CreateLegendsXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _positionXmlOperator.ReadXml(element.Element(_positionXmlOperator.Header));
            _borderXmlOperator.ReadXml(element.Element(_borderXmlOperator.Header));

            this.ReadTitleNotesXml(element.Element(ChartXmlOperator.TitleNotes));
            this.ReadPlotAreasXml(element.Element(ChartXmlOperator.PlotAreas));
            this.ReadLegendsXml(element.Element(ChartXmlOperator.Legends));
        }

        private XElement CreateTitleNotesXml()
        {
            var element = new XElement(ChartXmlOperator.TitleNotes);
            XmlOperator xmlOperator;
            foreach (var item in _pElement.TitleNotes)
            {
                xmlOperator = new TitleXmlOperator(item, ChartXmlOperator.TitleNote);
                element.Add(xmlOperator.CreateXml());
            }
            return element;
        }

        private XElement CreatePlotAreasXml()
        {
            var element = new XElement(ChartXmlOperator.PlotAreas);
            XmlOperator xmlOperator;
            foreach (var item in _pElement.PlotAreas)
            {
                xmlOperator = new PlotAreaXmlOperator(item);
                element.Add(xmlOperator.CreateXml());
            }
            return element;
        }

        private XElement CreateLegendsXml()
        {
            var element = new XElement(ChartXmlOperator.Legends);
            XmlOperator xmlOperator;
            XElement legendElement;
            foreach (var item in _pElement.Legends)
            {
                xmlOperator = new LegendXmlOperator(item);
                legendElement = xmlOperator.CreateXml();
                legendElement.SetAttributeValue(ChartXmlOperator.BindingIndex, this.GetBindingPlotAreaIndex(item));
                element.Add(legendElement);
            }
            return element;
        }

        private void ReadTitleNotesXml(XElement element)
        {
            if (element == null)
                return;

            var elements = element.Elements(ChartXmlOperator.TitleNote);
            XmlOperator xmlOperator;
            TitleNote titleNote;
            int index = 0;
            foreach (var node in elements)
            {
                if (index < _pElement.TitleNotes.Count)
                    titleNote = _pElement.TitleNotes[index];
                else
                    titleNote = new TitleNote();

                xmlOperator = new TitleXmlOperator(titleNote, ChartXmlOperator.TitleNote);
                xmlOperator.ReadXml(node);

                _pElement.TitleNotes.Add(titleNote);
                index++;
            }
        }

        private void ReadPlotAreasXml(XElement element)
        {
            if (element == null)
                return;

            var elements = element.Elements(ChartXmlOperator.PlotArea);
            XmlOperator xmlOperator;
            PlotArea plotArea;
            int index = 0;
            foreach (var node in elements)
            {
                if (index < _pElement.PlotAreas.Count)
                    plotArea = _pElement.PlotAreas[index];
                else
                    plotArea = new PlotArea();

                xmlOperator = new PlotAreaXmlOperator(plotArea);
                xmlOperator.ReadXml(node);

                _pElement.PlotAreas.Add(plotArea);
                index++;
            }
        }

        private void ReadLegendsXml(XElement element)
        {
            if (element == null)
                return;

            var elements = element.Elements(ChartXmlOperator.Legend);
            XmlOperator xmlOperator;
            Legend legend;
            int index = 0;
            int? bindingIndex;
            foreach (var node in elements)
            {
                if (index < _pElement.Legends.Count)
                    legend = _pElement.Legends[index];
                else
                    legend = new Legend();

                xmlOperator = new LegendXmlOperator(legend);
                xmlOperator.ReadXml(node);

                bindingIndex = (int?)XAttributeConverter.Convert2Double(node.Attribute(ChartXmlOperator.BindingIndex));
                if (bindingIndex != null)
                {
                    this.BindingPlotArea(legend, bindingIndex.Value);
                }

                _pElement.Legends.Add(legend);
                index++;
            }
        }

        private int GetBindingPlotAreaIndex(Legend legend)
        {
            var count = _pElement.PlotAreas.Count;
            PlotArea plotArea;
            for (int i = 0; i < count; i++)
            {
                plotArea = _pElement.PlotAreas[i];
                if (plotArea.DataLinks == legend.DataLinks)
                    return i;
            }
            return -1;
        }

        private void BindingPlotArea(Legend legend, int index)
        {
            if (index < 0 || index >= _pElement.PlotAreas.Count)
                return;

            var plotArea = _pElement.PlotAreas[index];
            var b = new Binding("DataLinks") { Source = plotArea };
            legend.SetBinding(Eenova.Chart.Elements.Legend.DataLinksProperty, b);
        }
    }
}
