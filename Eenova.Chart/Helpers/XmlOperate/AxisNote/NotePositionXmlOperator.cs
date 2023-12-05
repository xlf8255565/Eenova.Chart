using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class NotePositionXmlOperator : XmlOperator
    {
        INotePosition _pElement;

        public NotePositionXmlOperator(INotePosition element)
            : this(element, "NotePosition")
        {
        }

        public NotePositionXmlOperator(INotePosition element, string header)
            : base(header)
        {
            _pElement = element;
        }

        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.SetAttributeValue("VerticalOffset", _pElement.VerticalOffset);
            element.SetAttributeValue("HorizontalOffset", _pElement.HorizontalOffset);
            element.SetAttributeValue("NoteLocation", _pElement.NoteLocation);

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
        {
            if (element == null)
                return;

            var verticalOffset = XAttributeConverter.Convert2Double(element.Attribute("VerticalOffset"));
            if (verticalOffset != null && !double.IsNaN(verticalOffset.Value))
                _pElement.VerticalOffset = verticalOffset.Value;

            var horizontalOffset = XAttributeConverter.Convert2Double(element.Attribute("HorizontalOffset"));
            if (horizontalOffset != null && !double.IsNaN(horizontalOffset.Value))
                _pElement.HorizontalOffset = horizontalOffset.Value;

            var noteLocation = XAttributeConverter.Convert2Enum<NoteLocation>(element.Attribute("NoteLocation"));
            if (noteLocation != null)
                _pElement.NoteLocation = noteLocation.Value;
        }
    }
}
