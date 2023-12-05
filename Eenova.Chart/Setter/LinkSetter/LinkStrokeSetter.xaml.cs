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
    public partial class LinkStrokeSetter : BaseSetter
    {
        public LinkStrokeSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeStyleProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeThicknessProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeVisibilityProperty);
            this.AddBindingProperty(this.cbShadowVisibility, CheckBox.IsCheckedProperty);
        }
    }
}
