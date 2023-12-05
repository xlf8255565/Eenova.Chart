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
    public partial class AxisTicksStrokeSetter : BaseSetter
    {
        public AxisTicksStrokeSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeStyleProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeThicknessProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeVisibilityProperty);
            this.AddBindingProperty(this.cbMainTicksShowX, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.cbSubTicksShowX, ComboBox.SelectedValueProperty);
        }
    }
}
