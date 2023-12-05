﻿/*****************************************************************************
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



namespace Eenova.Chart.Setter
{
    public partial class CommonBorderSetter : BaseSetter
    {
        public CommonBorderSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeStyleProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeThicknessProperty);
            this.AddBindingProperty(this.sStroke, StrokeSelector.SStrokeVisibilityProperty);
            this.AddBindingProperty(this.sBorder, BorderSelector.SBorderBrushProperty);
            this.AddBindingProperty(this.sBorder, BorderSelector.SBorderVisibilityProperty);
        }
    }
}
