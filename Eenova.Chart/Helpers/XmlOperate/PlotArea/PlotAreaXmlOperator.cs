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
    public class PlotAreaXmlOperator : XmlOperator
    {
        IPlotArea _pElement;
        XmlOperator _displayXmlOperator;
        XmlOperator _positionXmlOperator;
        XmlOperator _borderXmlOperator;

        XmlOperator _axisXXmlOperator;
        XmlOperator _axisY1XmlOperator;
        XmlOperator _axisY2XmlOperator;
        XmlOperator _axisY3XmlOperator;
        XmlOperator _axisY4XmlOperator;
        XmlOperator _lX1XmlOperator;
        XmlOperator _lX2XmlOperator;
        XmlOperator _lY1XmlOperator;
        XmlOperator _lY2XmlOperator;
        XmlOperator _lY3XmlOperator;
        XmlOperator _lY4XmlOperator;


        public PlotAreaXmlOperator(IPlotArea element)
            : this(element, "PlotArea")
        {
        }

        public PlotAreaXmlOperator(IPlotArea element, string header)
            : base(header)
        {
            _displayXmlOperator = new PlotAreaDisplayXmlOperator(element);
            _positionXmlOperator = new PlotAreaPositionXmlOperator(element);
            _borderXmlOperator = new BorderXmlOperator(element);

            _axisXXmlOperator = new AxisXmlOperator(element.AxisX, "AxisX");
            _axisY1XmlOperator = new AxisXmlOperator(element.AxisY1, "AxisY1");
            _axisY2XmlOperator = new AxisXmlOperator(element.AxisY2, "AxisY2");
            _axisY3XmlOperator = new AxisXmlOperator(element.AxisY3, "AxisY3");
            _axisY4XmlOperator = new AxisXmlOperator(element.AxisY4, "AxisY4");

            _lX1XmlOperator = new StrokeXmlOperator(element.GridLineX1, "GridLineX1");
            _lX2XmlOperator = new StrokeXmlOperator(element.GridLineX2, "GridLineX2");
            _lY1XmlOperator = new StrokeXmlOperator(element.GridLineY1, "GridLineY1");
            _lY2XmlOperator = new StrokeXmlOperator(element.GridLineY2, "GridLineY2");
            _lY3XmlOperator = new StrokeXmlOperator(element.GridLineY3, "GridLineY3");
            _lY4XmlOperator = new StrokeXmlOperator(element.GridLineY4, "GridLineY4");

            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_displayXmlOperator.CreateXml());
            element.Add(_positionXmlOperator.CreateXml());
            element.Add(_borderXmlOperator.CreateXml());

            element.Add(_axisXXmlOperator.CreateXml());
            element.Add(_axisY1XmlOperator.CreateXml());
            element.Add(_axisY2XmlOperator.CreateXml());
            element.Add(_axisY3XmlOperator.CreateXml());
            element.Add(_axisY4XmlOperator.CreateXml());

            element.Add(_lX1XmlOperator.CreateXml());
            element.Add(_lX2XmlOperator.CreateXml());
            element.Add(_lY1XmlOperator.CreateXml());
            element.Add(_lY2XmlOperator.CreateXml());
            element.Add(_lY3XmlOperator.CreateXml());
            element.Add(_lY4XmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _displayXmlOperator.ReadXml(element.Element(_displayXmlOperator.Header));
            _positionXmlOperator.ReadXml(element.Element(_positionXmlOperator.Header));
            _borderXmlOperator.ReadXml(element.Element(_borderXmlOperator.Header));

            //_axisXXmlOperator.ReadXml(element.Element(_axisXXmlOperator.Header));
            //_axisY1XmlOperator.ReadXml(element.Element(_axisY1XmlOperator.Header));
            //_axisY2XmlOperator.ReadXml(element.Element(_axisY2XmlOperator.Header));
            //_axisY3XmlOperator.ReadXml(element.Element(_axisY3XmlOperator.Header));
            //_axisY4XmlOperator.ReadXml(element.Element(_axisY4XmlOperator.Header));

            //_lX1XmlOperator.ReadXml(element.Element(_lX1XmlOperator.Header));
            //_lX2XmlOperator.ReadXml(element.Element(_lX2XmlOperator.Header));
            //_lY1XmlOperator.ReadXml(element.Element(_lY1XmlOperator.Header));
            //_lY2XmlOperator.ReadXml(element.Element(_lY2XmlOperator.Header));
            //_lY3XmlOperator.ReadXml(element.Element(_lY3XmlOperator.Header));
            //_lY4XmlOperator.ReadXml(element.Element(_lY4XmlOperator.Header));

        }
    }
}

