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
    public class TicksStrokeXmlOperator : StrokeXmlOperator
    {
        ITicksStroke _pElement;

        public TicksStrokeXmlOperator(ITicksStroke element)
            : this(element, "TicksStroke")
        {
        }

        public TicksStrokeXmlOperator(ITicksStroke element, string header)
            : base(element, header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = base.CreateXml();

            element.SetAttributeValue("MainTicksShow", _pElement.MainTicksShow);
            element.SetAttributeValue("SubTicksShow", _pElement.SubTicksShow);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var mainTicksShow = XAttributeConverter.Convert2Enum<TicksShow>(element.Attribute("MainTicksShow"));
            if (mainTicksShow != null)
                _pElement.MainTicksShow = mainTicksShow.Value;

            var subTicksShow = XAttributeConverter.Convert2Enum<TicksShow>(element.Attribute("SubTicksShow"));
            if (subTicksShow != null)
                _pElement.SubTicksShow = subTicksShow.Value;

            base.ReadXml(element);
        }
    }
}
