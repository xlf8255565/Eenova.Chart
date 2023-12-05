using System.Xml.Linq;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers.XmlOperate
{
    public class LegendXmlOperator : XmlOperator
    {
        Legend _pElement;
        XmlOperator _positionXmlOperator;
        XmlOperator _borderXmlOperator;
        XmlOperator _fontXmlOperator;
        XmlOperator _alignmentXmlOperator;

        public LegendXmlOperator(Legend element)
            : this(element, "Legend")
        {
        }

        public LegendXmlOperator(Legend element, string header)
            : base(header)
        {
            _positionXmlOperator = new PositionXmlOperator(element);
            _borderXmlOperator = new BorderXmlOperator(element);
            _fontXmlOperator = new FontXmlOperator(element);
            _alignmentXmlOperator = new LegendAlignmentXmlOperator(element);
            _pElement = element;
        }


        internal override System.Xml.Linq.XElement CreateXml()
        {
            var element = new XElement(this.Header);

            element.Add(_positionXmlOperator.CreateXml());
            element.Add(_borderXmlOperator.CreateXml());
            element.Add(_fontXmlOperator.CreateXml());
            element.Add(_alignmentXmlOperator.CreateXml());

            return element;
        }

        internal override void ReadXml(System.Xml.Linq.XElement element)
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
