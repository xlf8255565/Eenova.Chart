using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;
using System.Windows;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class PositionXmlOperator : XmlOperator
    {
        IPosition _pElement;

        public PositionXmlOperator(IPosition element)
            : this(element, "Position")
        {
        }

        public PositionXmlOperator(IPosition element, string header)
            : base(header)
        {
            if (element == null)
                return;
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("Height", _pElement.Height);
            element.SetAttributeValue("Width", _pElement.Width);
            element.SetAttributeValue("HorizontalAlignment", _pElement.HorizontalAlignment);
            element.SetAttributeValue("VerticalAlignment", _pElement.VerticalAlignment);
            element.SetAttributeValue("Margin", _pElement.Margin);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var height = XAttributeConverter.Convert2Double(element.Attribute("Height"));
            if (height != null)
                _pElement.Height = height.Value;

            var width = XAttributeConverter.Convert2Double(element.Attribute("Width"));
            if (width != null)
                _pElement.Width = width.Value;

            var horizontalAlignment = XAttributeConverter.Convert2Enum<HorizontalAlignment>(element.Attribute("HorizontalAlignment"));
            if (horizontalAlignment != null)
                _pElement.HorizontalAlignment = horizontalAlignment.Value;

            var verticalAlignment = XAttributeConverter.Convert2Enum<VerticalAlignment>(element.Attribute("VerticalAlignment"));
            if (verticalAlignment != null)
                _pElement.VerticalAlignment = verticalAlignment.Value;

            var margin = XAttributeConverter.Convert2Thickness(element.Attribute("Margin"));
            if (margin != null)
                _pElement.Margin = margin.Value;
        }
    }
}
