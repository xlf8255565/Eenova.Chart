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
    public partial class LinkMarkSetter : BaseSetter
    {
        public LinkMarkSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbMarkVisibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbbMarkType, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.nbMarkSize, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.cpMarkBrush, ColorPicker.SelectedColorProperty);
        }
    }
}
