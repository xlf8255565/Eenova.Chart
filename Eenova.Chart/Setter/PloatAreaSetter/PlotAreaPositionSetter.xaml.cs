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
    public partial class PlotAreaPositionSetter : BaseSetter
    {
        public PlotAreaPositionSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbbAlignment, AlignmentSelector.SHorizontalAlignmentProperty);
            this.AddBindingProperty(this.cbbAlignment, AlignmentSelector.SVerticalAlignmentProperty);
            this.AddBindingProperty(this.mgSetter, MarginSelector.SMarginProperty);
            this.AddBindingProperty(this.nbLength, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbTopHeight, NumericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbBottomHeight, NumericUpDown.ValueProperty);
        }
    }
}
