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
    public class NumbericXmlOperator : XmlOperator
    {
        INumberic _pElement;

        public NumbericXmlOperator(INumberic element)
            : this(element, "Numberic")
        {
        }

        public NumbericXmlOperator(INumberic element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("Value", _pElement.Value);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var value = XAttributeConverter.Convert2Double(element.Attribute("Value"));
            if (value != null && !double.IsNaN(value.Value))
                _pElement.Value = value.Value;
        }
    }
}
