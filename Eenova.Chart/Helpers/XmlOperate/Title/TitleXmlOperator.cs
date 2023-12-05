using System.Xml.Linq;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class TitleXmlOperator : XmlOperator
    {
        ITitle _pElement;
        XmlOperator _positionXmlOperator;
        XmlOperator _borderXmlOperator;
        XmlOperator _fontXmlOperator;
        XmlOperator _alignmentXmlOperator;


        public TitleXmlOperator(ITitle element)
            : this(element, "Title")
        {
        }

        public TitleXmlOperator(ITitle element, string header)
            : base(header)
        {
            _positionXmlOperator = new PositionXmlOperator(element);
            _borderXmlOperator = new BorderXmlOperator(element);
            _fontXmlOperator = new FontXmlOperator(element);
            _alignmentXmlOperator = new TitleAlignmentXmlOperator(element);
            _pElement = element;
        }

        internal override XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_positionXmlOperator.CreateXml());
            element.Add(_borderXmlOperator.CreateXml());
            element.Add(_fontXmlOperator.CreateXml());
            element.Add(_alignmentXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(XElement element)
        {
            if (element == null)
                return;

            _positionXmlOperator.ReadXml(element.Element(_positionXmlOperator.Header));
            _borderXmlOperator.ReadXml(element.Element(_borderXmlOperator.Header));
            _fontXmlOperator.ReadXml(element.Element(_fontXmlOperator.Header));
            _alignmentXmlOperator.ReadXml(element.Element(_alignmentXmlOperator.Header));
        }
    }
}
