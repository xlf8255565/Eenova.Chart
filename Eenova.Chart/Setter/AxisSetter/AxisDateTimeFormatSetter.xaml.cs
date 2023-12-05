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



namespace Eenova.Chart.Setter
{
    public partial class AxisDateTimeFormatSetter : BaseSetter
    {
        public AxisDateTimeFormatSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.formater, FormatSelector.FormatProperty);
        }
    }
}
