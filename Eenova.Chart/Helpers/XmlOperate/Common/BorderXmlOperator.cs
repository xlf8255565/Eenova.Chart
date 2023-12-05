using System.Windows;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class BorderXmlOperator : StrokeXmlOperator
    {
        IBorder _pElement;

        public BorderXmlOperator(IBorder element)
            : this(element, "Border")
        {
        }

        public BorderXmlOperator(IBorder element, string header)
            : base(element, header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = base.CreateXml();

            element.SetAttributeValue("BorderBrush", new Brush2ColorConverter().Convert(_pElement.BorderBrush, null, null, null));
            element.SetAttributeValue("BorderVisibility", _pElement.BorderVisibility);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var borderBrush = XAttributeConverter.Convert2Brush(element.Attribute("BorderBrush"));
            if (borderBrush != null)
                _pElement.BorderBrush = borderBrush;

            var borderVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("BorderVisibility"));
            if (borderVisibility != null)
                _pElement.BorderVisibility = borderVisibility.Value;

            base.ReadXml(element);
        }
    }
}
