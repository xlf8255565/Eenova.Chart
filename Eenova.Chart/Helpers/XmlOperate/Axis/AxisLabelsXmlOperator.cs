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
    public class AxisLabelsXmlOperator : XmlOperator
    {
        IAxisLabels _pElement;
        XmlOperator _fontXmlOperator;
        XmlOperator _alignmentXmlOperator;


        public AxisLabelsXmlOperator(IAxisLabels element)
            : this(element, "AxisLabels")
        {
        }

        public AxisLabelsXmlOperator(IAxisLabels element, string header)
            : base(header)
        {
            _fontXmlOperator = new FontXmlOperator(element);
            _alignmentXmlOperator = new TitleAlignmentXmlOperator(element);
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_fontXmlOperator.CreateXml());
            element.Add(_alignmentXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _fontXmlOperator.ReadXml(element.Element(_fontXmlOperator.Header));
            _alignmentXmlOperator.ReadXml(element.Element(_alignmentXmlOperator.Header));
        }
    }
}
