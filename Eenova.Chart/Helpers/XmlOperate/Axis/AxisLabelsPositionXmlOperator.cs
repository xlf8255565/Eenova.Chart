using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class AxisLabelsPositionXmlOperator : XmlOperator
    {
        IAxisLabelsPosition _pElement;

        public AxisLabelsPositionXmlOperator(IAxisLabelsPosition element)
            : this(element, "AxisLabelsPosition")
        {
        }

        public AxisLabelsPositionXmlOperator(IAxisLabelsPosition element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("LabelLocation", _pElement.LabelLocation);
            element.SetAttributeValue("LabelOffset", _pElement.LabelOffset);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var labelOffset = XAttributeConverter.Convert2Double(element.Attribute("LabelOffset"));
            if (labelOffset != null && !double.IsNaN(labelOffset.Value))
                _pElement.LabelOffset = labelOffset.Value;

            var labelLocation = XAttributeConverter.Convert2Enum<AxisLocation>(element.Attribute("LabelLocation"));
            if (labelLocation != null)
                _pElement.LabelLocation = labelLocation.Value;
        }
    }
}
