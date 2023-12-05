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

namespace Eenova.Chart.Setter
{
    public partial class AxisTitlePositionSetter : BaseSetter
    {
        public AxisTitlePositionSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbLocationX, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.cbAlignmentX, ComboBox.SelectedValueProperty);
        }
    }
}
