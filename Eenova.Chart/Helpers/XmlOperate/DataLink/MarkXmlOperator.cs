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
    public class MarkXmlOperator : XmlOperator
    {
        IMark _pElement;

        public MarkXmlOperator(IMark element)
            : this(element, "Mark")
        {
        }

        public MarkXmlOperator(IMark element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("MarkBrush", new Brush2ColorConverter().Convert(_pElement.MarkBrush, null, null, null));
            element.SetAttributeValue("MarkSize", _pElement.MarkSize);
            element.SetAttributeValue("MarkType", _pElement.MarkType);
            element.SetAttributeValue("MarkVisibility", _pElement.MarkVisibility);

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            var markBrush = XAttributeConverter.Convert2Brush(element.Attribute("MarkBrush"));
            if (markBrush != null)
                _pElement.MarkBrush = markBrush;

            var markSize = XAttributeConverter.Convert2Double(element.Attribute("MarkSize"));
            if (markSize != null && !double.IsNaN(markSize.Value))
                _pElement.MarkSize = markSize.Value;

            var markType = XAttributeConverter.Convert2Enum<ShapeType>(element.Attribute("MarkType"));
            if (markType != null)
                _pElement.MarkType = markType.Value;

            var markVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("MarkVisibility"));
            if (markVisibility != null)
                _pElement.MarkVisibility = markVisibility.Value;
        }
    }
}
