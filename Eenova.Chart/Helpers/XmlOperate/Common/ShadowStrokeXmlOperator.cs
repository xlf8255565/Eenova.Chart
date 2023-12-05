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
using Eenova.Chart.Converters;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class ShadowStrokeXmlOperator : StrokeXmlOperator
    {
        IShadowStroke _pElement;

        public ShadowStrokeXmlOperator(IShadowStroke element)
            : this(element, "ShadowStroke")
        {
        }

        public ShadowStrokeXmlOperator(IShadowStroke element, string header)
            : base(element, header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = base.CreateXml();

            element.SetAttributeValue("ShadowVisibility", _pElement.ShadowVisibility);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var shadowVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("ShadowVisibility"));
            if (shadowVisibility != null)
                _pElement.ShadowVisibility = shadowVisibility.Value;

            base.ReadXml(element);
        }
    }
}
