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
    public class PlotAreaDisplayXmlOperator : XmlOperator
    {
        IPlotAreaDisplay _pElement;

        public PlotAreaDisplayXmlOperator(IPlotAreaDisplay element)
            : this(element, "PlotAreaDisplay")
        {
        }

        public PlotAreaDisplayXmlOperator(IPlotAreaDisplay element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("TopVisibility", _pElement.TopVisibility);
            element.SetAttributeValue("BottomVisibility", _pElement.BottomVisibility);

            element.SetAttributeValue("XAlignment", _pElement.XAlignment);
            element.SetAttributeValue("IsXVisible", _pElement.IsXVisible);
            element.SetAttributeValue("IsY1Visible", _pElement.IsY1Visible);
            element.SetAttributeValue("IsY2Visible", _pElement.IsY2Visible);
            element.SetAttributeValue("IsY3Visible", _pElement.IsY3Visible);
            element.SetAttributeValue("IsY4Visible", _pElement.IsY4Visible);

            element.SetAttributeValue("LX1Visibility", _pElement.LX1Visibility);
            element.SetAttributeValue("LX2Visibility", _pElement.LX2Visibility);
            element.SetAttributeValue("LY1Visibility", _pElement.LY1Visibility);
            element.SetAttributeValue("LY2Visibility", _pElement.LY2Visibility);
            element.SetAttributeValue("LY3Visibility", _pElement.LY3Visibility);
            element.SetAttributeValue("LY4Visibility", _pElement.LY4Visibility);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var topVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("TopVisibility"));
            if (topVisibility != null)
                _pElement.TopVisibility = topVisibility.Value;

            var bottomVisibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("BottomVisibility"));
            if (bottomVisibility != null)
                _pElement.BottomVisibility = bottomVisibility.Value;

            var xAlignment = XAttributeConverter.Convert2Enum<VerticalAlignment>(element.Attribute("XAlignment"));
            if (xAlignment != null)
                _pElement.XAlignment = xAlignment.Value;

            var lX1Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LX1Visibility"));
            if (lX1Visibility != null)
                _pElement.LX1Visibility = lX1Visibility.Value;

            var lX2Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LX2Visibility"));
            if (lX2Visibility != null)
                _pElement.LX2Visibility = lX2Visibility.Value;

            var lY1Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LY1Visibility"));
            if (lY1Visibility != null)
                _pElement.LY1Visibility = lY1Visibility.Value;

            var lY2Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LY2Visibility"));
            if (lY2Visibility != null)
                _pElement.LY2Visibility = lY2Visibility.Value;

            var lY3Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LY3Visibility"));
            if (lY3Visibility != null)
                _pElement.LY3Visibility = lY3Visibility.Value;

            var lY4Visibility = XAttributeConverter.Convert2Enum<Visibility>(element.Attribute("LY4Visibility"));
            if (lY4Visibility != null)
                _pElement.LY4Visibility = lY4Visibility.Value;

            var isXVisible = XAttributeConverter.Convert2Bool(element.Attribute("IsXVisible"));
            if (isXVisible != null)
                _pElement.IsXVisible = isXVisible.Value;

            var isY1Visible = XAttributeConverter.Convert2Bool(element.Attribute("IsY1Visible"));
            if (isY1Visible != null)
                _pElement.IsY1Visible = isY1Visible.Value;

            var isY2Visible = XAttributeConverter.Convert2Bool(element.Attribute("IsY2Visible"));
            if (isY2Visible != null)
                _pElement.IsY2Visible = isY2Visible.Value;

            var isY3Visible = XAttributeConverter.Convert2Bool(element.Attribute("IsY3Visible"));
            if (isY3Visible != null)
                _pElement.IsY3Visible = isY3Visible.Value;

            var isY4Visible = XAttributeConverter.Convert2Bool(element.Attribute("IsY4Visible"));
            if (isY4Visible != null)
                _pElement.IsY4Visible = isY4Visible.Value;
        }
    }
}
