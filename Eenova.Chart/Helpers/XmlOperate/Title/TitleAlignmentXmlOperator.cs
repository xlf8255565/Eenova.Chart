using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class TitleAlignmentXmlOperator : XmlOperator
    {
        ITitleAlignment _pElement;

        public TitleAlignmentXmlOperator(ITitleAlignment element)
            : this(element, "TitleAlignment")
        {
        }

        public TitleAlignmentXmlOperator(ITitleAlignment element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("TextRotation", _pElement.TextRotation);
            element.SetAttributeValue("Orientation", _pElement.Orientation);
            element.SetAttributeValue("HorizontalContentAlignment", _pElement.HorizontalContentAlignment);
            element.SetAttributeValue("VerticalContentAlignment", _pElement.VerticalContentAlignment);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var rotation = XAttributeConverter.Convert2Double(element.Attribute("TextRotation"));
            if (rotation != null && !double.IsNaN(rotation.Value))
                _pElement.TextRotation = rotation.Value;

            var orientation = XAttributeConverter.Convert2Enum<Orientation>(element.Attribute("Orientation"));
            if (orientation != null)
                _pElement.Orientation = orientation.Value;

            var horizontalContentAlignment = XAttributeConverter.Convert2Enum<HorizontalAlignment>(element.Attribute("HorizontalContentAlignment"));
            if (horizontalContentAlignment != null)
                _pElement.HorizontalContentAlignment = horizontalContentAlignment.Value;

            var verticalAlignment = XAttributeConverter.Convert2Enum<VerticalAlignment>(element.Attribute("VerticalContentAlignment"));
            if (verticalAlignment != null)
                _pElement.VerticalContentAlignment = verticalAlignment.Value;

        }
    }
}
