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


using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Setter
{
    public partial class AxisNumbericTicksSetter : BaseSetter
    {
        public AxisNumbericTicksSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbIsMinValueAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMinValue, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.cbIsMaxValueAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMaxValue, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.cbIsMainUnitAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbMainUnit, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.cbIsSubUnitAuto, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.nbSubUnit, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.cbIsLogarithm, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbIsDesc, CheckBox.IsCheckedProperty);
        }
    }
}
