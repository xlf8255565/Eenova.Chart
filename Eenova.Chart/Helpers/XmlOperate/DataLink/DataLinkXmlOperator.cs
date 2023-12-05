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

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class DataLinkXmlOperator : XmlOperator
    {
        IDataLink _pElement;
        XmlOperator _shadowStrokeXmlOperator;
        XmlOperator _markXmlOperator;


        public DataLinkXmlOperator(IDataLink element)
            : this(element, "DataLink")
        {
        }

        public DataLinkXmlOperator(IDataLink element, string header)
            : base(header)
        {
            _shadowStrokeXmlOperator = new ShadowStrokeXmlOperator(element);
            _markXmlOperator = new MarkXmlOperator(element);
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_shadowStrokeXmlOperator.CreateXml());
            element.Add(_markXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _shadowStrokeXmlOperator.ReadXml(element.Element(_shadowStrokeXmlOperator.Header));
            _markXmlOperator.ReadXml(element.Element(_markXmlOperator.Header));
        }

    }
}
