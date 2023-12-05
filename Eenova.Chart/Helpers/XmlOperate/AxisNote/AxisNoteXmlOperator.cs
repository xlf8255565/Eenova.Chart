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
using Eenova.Chart.Adapters;
using Eenova.Chart.Converters;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class AxisNoteXmlOperator : XmlOperator
    {
        IAxisNote _pElement;
        XmlOperator _pointingStrokeXmlOperator;
        XmlOperator _strokeXmlOperator;
        XmlOperator _leftStrokeXmlOperator;
        XmlOperator _rightStrokeXmlOperator;
        XmlOperator _fontXmlOperator;
        XmlOperator _notePositionXmlOperator;

        public AxisNoteXmlOperator(IAxisNote element)
            : this(element, "AxisNote")
        {
        }

        public AxisNoteXmlOperator(IAxisNote element, string header)
            : base(header)
        {
            _pElement = element;

            _notePositionXmlOperator = new NotePositionXmlOperator(_pElement);
            _fontXmlOperator = new FontXmlOperator(_pElement.Note);
            _pointingStrokeXmlOperator = new StrokeXmlOperator(new StrokeAdapter(_pElement.PointingLine), "PointingLine");
            _strokeXmlOperator = new StrokeXmlOperator(new StrokeAdapter(_pElement.StrokeLine), "StrokeLine");
            _leftStrokeXmlOperator = new StrokeXmlOperator(new StrokeAdapter(_pElement.LeftSideLine), "LeftSideLine");
            _rightStrokeXmlOperator = new StrokeXmlOperator(new StrokeAdapter(_pElement.RightSideLine), "RightSideLine");
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("SideHeight", _pElement.SideHeight);
            element.SetAttributeValue("ArrowSize", _pElement.ArrowSize);
            element.SetAttributeValue("Top", _pElement.Top);

            element.Add(_notePositionXmlOperator.CreateXml());
            element.Add(_fontXmlOperator.CreateXml());
            element.Add(_pointingStrokeXmlOperator.CreateXml());
            element.Add(_strokeXmlOperator.CreateXml());
            element.Add(_leftStrokeXmlOperator.CreateXml());
            element.Add(_rightStrokeXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            var sideHeight = XAttributeConverter.Convert2Double(element.Attribute("SideHeight"));
            if (sideHeight != null && !double.IsNaN(sideHeight.Value))
                _pElement.SideHeight = sideHeight.Value;

            var arrowSize = XAttributeConverter.Convert2Double(element.Attribute("ArrowSize"));
            if (arrowSize != null && !double.IsNaN(arrowSize.Value))
                _pElement.ArrowSize = arrowSize.Value;

            var top = XAttributeConverter.Convert2Double(element.Attribute("Top"));
            if (top != null && !double.IsNaN(top.Value))
                _pElement.Top = top.Value;

            _notePositionXmlOperator.ReadXml(element.Element(_notePositionXmlOperator.Header));
            _fontXmlOperator.ReadXml(element.Element(_fontXmlOperator.Header));
            _pointingStrokeXmlOperator.ReadXml(element.Element(_pointingStrokeXmlOperator.Header));
            _strokeXmlOperator.ReadXml(element.Element(_strokeXmlOperator.Header));
            _leftStrokeXmlOperator.ReadXml(element.Element(_leftStrokeXmlOperator.Header));
            _rightStrokeXmlOperator.ReadXml(element.Element(_rightStrokeXmlOperator.Header));
        }
    }
}
