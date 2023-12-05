using System.Windows;
using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class StrokeXmlOperator : XmlOperator
    {
        IStroke _pElement;

        public StrokeXmlOperator(IStroke element)
            : this(element, "Stroke")
        {
        }

        public StrokeXmlOperator(IStroke element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("Stroke", new Brush2ColorConverter().Convert(_pElement.Stroke, null, null, null));
            element.SetAttributeValue("StrokeStyle", _pElement.StrokeStyle);
            element.SetAttributeValue("StrokeThickness", _pElement.StrokeThickness);
            element.SetAttributeValue("StrokeVisibility", _pElement.StrokeVisibility);

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            var stroke = XAttributeConverter.Convert2Brush(element.Attribute("Stroke"));
            if (stroke != null)
                _pElement.Stroke = stroke;

            var strokeStyle = XAttributeConverter.Convert2String(element.Attribute("StrokeStyle"));
            if (strokeStyle != null)
                _pElement.StrokeStyle = strokeStyle;

            var strokeThickness = XAttributeConverter.Convert2Double(element.Attribute("StrokeThickness"));
            if (strokeThickness != null && !double.IsNaN(strokeThickness.Value))
                _pElement.StrokeThickness = strokeThickness.Value;

            var strokeVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("StrokeVisibility"));
            if (strokeVisibility != null)
                _pElement.StrokeVisibility = strokeVisibility.Value;
        }
    }
}
