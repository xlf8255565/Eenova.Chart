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
using Eenova.Chart.Converters;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class PlotAreaPositionXmlOperator : XmlOperator
    {
        IPlotAreaPosition _pElement;

        public PlotAreaPositionXmlOperator(IPlotAreaPosition element)
            : this(element, "PlotAreaPosition")
        {
        }

        public PlotAreaPositionXmlOperator(IPlotAreaPosition element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("Length", _pElement.Length);
            element.SetAttributeValue("TopHeight", _pElement.TopHeight);
            element.SetAttributeValue("BottomHeight", _pElement.BottomHeight);
            element.SetAttributeValue("Margin", _pElement.Margin);
            element.SetAttributeValue("HorizontalAlignment", _pElement.HorizontalAlignment);
            element.SetAttributeValue("VerticalAlignment", _pElement.VerticalAlignment);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var topHeight = XAttributeConverter.Convert2Double(element.Attribute("TopHeight"));
            if (topHeight != null && !double.IsNaN(topHeight.Value))
                _pElement.TopHeight = topHeight.Value;

            var bottomHeight = XAttributeConverter.Convert2Double(element.Attribute("BottomHeight"));
            if (bottomHeight != null && !double.IsNaN(bottomHeight.Value))
                _pElement.BottomHeight = bottomHeight.Value;

            var length = XAttributeConverter.Convert2Double(element.Attribute("Length"));
            if (length != null && !double.IsNaN(length.Value))
                _pElement.Length = length.Value;

            var margin = XAttributeConverter.Convert2Thickness(element.Attribute("Margin"));
            if (margin != null)
                _pElement.Margin = margin.Value;

            var horizontalAlignment = XAttributeConverter.Convert2Enum<HorizontalAlignment>(element.Attribute("HorizontalAlignment"));
            if (horizontalAlignment != null)
                _pElement.HorizontalAlignment = horizontalAlignment.Value;

            var verticalAlignment = XAttributeConverter.Convert2Enum<VerticalAlignment>(element.Attribute("VerticalAlignment"));
            if (verticalAlignment != null)
                _pElement.VerticalAlignment = verticalAlignment.Value;
        }
    }
}

