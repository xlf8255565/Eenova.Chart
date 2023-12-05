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


using Eenova.Chart.Controls;

namespace Eenova.Chart.Setter
{
    public partial class CommonPositionSetter : BaseSetter
    {
        public CommonPositionSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbbAlignment, AlignmentSelector.SHorizontalAlignmentProperty);
            this.AddBindingProperty(this.cbbAlignment, AlignmentSelector.SVerticalAlignmentProperty);
            this.AddBindingProperty(this.mgSetter, MarginSelector.SMarginProperty);
            this.AddBindingProperty(this.nbWidth, AutoValueNumbericUpDown.ValueProperty);
            this.AddBindingProperty(this.nbHeight, AutoValueNumbericUpDown.ValueProperty);
        }
    }
}
