using System.Windows.Controls;
using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class LegendAlignmentXmlOperator : XmlOperator
    {
        ILegendAlignment _pElement;

        public LegendAlignmentXmlOperator(ILegendAlignment element)
            : this(element, "LegendAlignment")
        {
        }

        public LegendAlignmentXmlOperator(ILegendAlignment element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("Orientation", _pElement.Orientation);

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            var orientation = XAttributeConverter.Convert2Enum<Orientation>(element.Attribute("Orientation"));
            if (orientation != null)
                _pElement.Orientation = orientation.Value;
        }
    }
}
