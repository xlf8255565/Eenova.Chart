using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class AxisTitlePositionXmlOperator : XmlOperator
    {
        IAxisTitlePosition _pElement;

        public AxisTitlePositionXmlOperator(IAxisTitlePosition element)
            : this(element, "AxisTitlePosition")
        {
        }

        public AxisTitlePositionXmlOperator(IAxisTitlePosition element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("TitleAlignment", _pElement.TitleAlignment);
            element.SetAttributeValue("TitleLocation", _pElement.TitleLocation);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var titleAlignment = XAttributeConverter.Convert2Enum<AxisAlignment>(element.Attribute("TitleAlignment"));
            if (titleAlignment != null)
                _pElement.TitleAlignment = titleAlignment.Value;

            var titleLocation = XAttributeConverter.Convert2Enum<AxisLocation>(element.Attribute("TitleLocation"));
            if (titleLocation != null)
                _pElement.TitleLocation = titleLocation.Value;
        }
    }
}
