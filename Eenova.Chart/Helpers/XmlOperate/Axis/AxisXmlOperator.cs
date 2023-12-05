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
    public class AxisXmlOperator : XmlOperator
    {
        IAxis _pElement;
        XmlOperator _titleXmlOperator;
        XmlOperator _titlePositionXmlOperator;
        XmlOperator _labelsXmlOperator;
        XmlOperator _labelsPositonXmlOperator;
        XmlOperator _ticksStrokeXmlOperator;

        public AxisXmlOperator(IAxis element)
            : this(element, "Axis")
        {
        }

        public AxisXmlOperator(IAxis element, string header)
            : base(header)
        {
            _titleXmlOperator = new TitleXmlOperator(element.Title, "AxisTitle");
            _titlePositionXmlOperator = new AxisTitlePositionXmlOperator(element);
            _labelsXmlOperator = new AxisLabelsXmlOperator(element.Labels);
            _labelsPositonXmlOperator = new AxisLabelsPositionXmlOperator(element);
            _ticksStrokeXmlOperator = new TicksStrokeXmlOperator(element);
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_titleXmlOperator.CreateXml());
            element.Add(_titlePositionXmlOperator.CreateXml());
            element.Add(_labelsXmlOperator.CreateXml());
            element.Add(_labelsPositonXmlOperator.CreateXml());
            element.Add(_ticksStrokeXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _titleXmlOperator.ReadXml(element.Element(_titleXmlOperator.Header));
            _titlePositionXmlOperator.ReadXml(element.Element(_titlePositionXmlOperator.Header));
            _labelsXmlOperator.ReadXml(element.Element(_labelsXmlOperator.Header));
            _labelsPositonXmlOperator.ReadXml(element.Element(_labelsPositonXmlOperator.Header));
            _ticksStrokeXmlOperator.ReadXml(element.Element(_ticksStrokeXmlOperator.Header));
        }
    }
}
