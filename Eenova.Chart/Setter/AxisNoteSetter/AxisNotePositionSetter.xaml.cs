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
    public partial class AxisNotePositionSetter : BaseSetter
    {
        public AxisNotePositionSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbNoteLocation, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.nbHorizontalOffset, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbVerticalOffset, NumericUpDown.ValueProperty);
        }
    }
}
