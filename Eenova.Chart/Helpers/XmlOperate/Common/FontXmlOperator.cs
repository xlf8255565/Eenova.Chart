using System.Windows;
using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class FontXmlOperator : XmlOperator
    {
        IFont _pElement;

        public FontXmlOperator(IFont element)
            : this(element, "Font")
        {
        }

        public FontXmlOperator(IFont element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("FontFamily", _pElement.FontFamily);
            element.SetAttributeValue("FontSize", _pElement.FontSize);
            element.SetAttributeValue("FontStyle", _pElement.FontStyle);
            element.SetAttributeValue("FontWeight", _pElement.FontWeight);
            element.SetAttributeValue("TextDecorations", new Underline2BoolConverter().Convert(_pElement.TextDecorations, null, null, null));

            var brush2ColorConverter = new Brush2ColorConverter();
            element.SetAttributeValue("Foreground", brush2ColorConverter.Convert(_pElement.Foreground, null, null, null));
            element.SetAttributeValue("Background", brush2ColorConverter.Convert(_pElement.Background, null, null, null));

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var fontFamily = XAttributeConverter.Convert2FontFamily(element.Attribute("FontFamily"));
            if (fontFamily != null)
                _pElement.FontFamily = fontFamily;

            var fontSize = XAttributeConverter.Convert2Double(element.Attribute("FontSize"));
            if (fontSize != null && !double.IsNaN(fontSize.Value))
                _pElement.FontSize = fontSize.Value;

            var fontStyle = XAttributeConverter.Convert2FontStyle(element.Attribute("FontStyle"));
            if (fontStyle != null)
                _pElement.FontStyle = fontStyle.Value;

            var fontWeight = XAttributeConverter.Convert2FontWeight(element.Attribute("FontWeight"));
            if (fontWeight != null)
                _pElement.FontWeight = fontWeight.Value;

            var foreground = XAttributeConverter.Convert2Brush(element.Attribute("Foreground"));
            if (foreground != null)
                _pElement.Foreground = foreground;

            var background = XAttributeConverter.Convert2Brush(element.Attribute("Background"));
            if (background != null)
                _pElement.Background = background;

            var textDecorations = XAttributeConverter.Convert2Bool(element.Attribute("Background"));
            if (textDecorations != null)
            {
                if (textDecorations.Value)
                    _pElement.TextDecorations = TextDecorations.Underline;
                else
                    _pElement.TextDecorations = null;
            }
        }
    }
}
