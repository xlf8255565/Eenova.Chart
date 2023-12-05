/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/


using System.Xml.Linq;
using Eenova.Chart.Converters;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Styler
{
    class AxisStyler : XmlStyler
    {
        XmlStyler _titleStyler;
        XmlStyler _labelseStyler;

        public AxisStyler(Axis element)
            : this(element, "Axis")
        {
        }

        public AxisStyler(Axis element, string header)
            : base(element, header)
        {
            _titleStyler = new TitleStyler(element.Title);
            _labelseStyler = new AxisLabelsStyler(element.Labels);
        }

        public override XElement CreateXml()
        {
            base.CreateXml();

            AddAttribute(() => Axis.VisibilityProperty);
            AddAttribute(() => Axis.LabelLocationProperty);
            AddAttribute(() => Axis.LabelOffsetProperty);
            AddAttribute(() => Axis.TitleLocationProperty);

            AddAttribute(() => Axis.StrokeProperty, new Brush2ColorConverter());
            AddAttribute(() => Axis.StrokeStyleProperty);
            AddAttribute(() => Axis.StrokeThicknessProperty);
            AddAttribute(() => Axis.StrokeVisibilityProperty);
            AddAttribute(() => Axis.MainTicksShowProperty);
            AddAttribute(() => Axis.SubTicksShowProperty);
            AddAttribute(() => Axis.DataTypeProperty);
            AddAttribute(() => Axis.FormatProperty);

            AddAttribute(() => Axis.IsDescProperty, true);
            bool isMaxAutoSaved = AddAttribute(() => Axis.IsMaxValueAutoProperty);
            bool isMinAutoSaved = AddAttribute(() => Axis.IsMinValueAutoProperty);
            bool isMainAutoSaved = AddAttribute(() => Axis.IsMainUnitAutoProperty);
            bool isSubAutoSaved = AddAttribute(() => Axis.IsSubUnitAutoProperty);

            if (isMainAutoSaved)
                AddAttribute(() => Axis.MainUnitProperty);
            if (isMinAutoSaved)
                AddAttribute(() => Axis.MinValueProperty);
            if (isMaxAutoSaved)
                AddAttribute(() => Axis.MaxValueProperty);
            if (isSubAutoSaved)
                AddAttribute(() => Axis.SubUnitProperty);

            AddAttribute(() => Axis.IsFixPositionProperty);
            AddAttribute(() => Axis.NumericPositionProperty);
            AddAttribute(() => Axis.TextPositionProperty);


            _xml.Add(_titleStyler.CreateXml());
            _xml.Add(_labelseStyler.CreateXml());

            return _xml;
        }

        public override void ReadXml(XElement xml)
        {
            base.ReadXml(xml);
            _titleStyler.ReadXml(xml.Element(_titleStyler.Header));
            _labelseStyler.ReadXml(xml.Element(_labelseStyler.Header));
        }
    }

}
