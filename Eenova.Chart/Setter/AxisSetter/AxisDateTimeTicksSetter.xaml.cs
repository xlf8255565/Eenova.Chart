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


using System.Windows.Controls;
using Eenova.Chart.Controls;

namespace Eenova.Chart.Setter
{
    public partial class AxisDateTimeTicksSetter : BaseSetter
    {
        public AxisDateTimeTicksSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbIsMinValueAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMinValue, SpanDateTimePicker.SpanSecondsProperty);
            this.AddBindingProperty(this.cbIsMaxValueAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMaxValue, SpanDateTimePicker.SpanSecondsProperty);
            this.AddBindingProperty(this.cbIsMainUnitAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMainUnit, SpanSecondsPicker.SpanSecondsProperty);
            this.AddBindingProperty(this.cbIsSubUnitAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbSubUnit, SpanSecondsPicker.SpanSecondsProperty);
            this.AddBindingProperty(this.cbIsDesc, CheckBox.IsCheckedProperty);
        }
    }
}
