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
    public partial class PlotAreaShowSetter : BaseSetter
    {
        public PlotAreaShowSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbTopVisibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbBottomVisibility, CheckBox.IsCheckedProperty);

            this.AddBindingProperty(this.cbXVisibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbY1Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbY2Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbY3Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbY4Visibility, CheckBox.IsCheckedProperty);

            this.AddBindingProperty(this.cbGridLineX1Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbGridLineX2Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbGridLineY1Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbGridLineY2Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbGridLineY3Visibility, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbGridLineY4Visibility, CheckBox.IsCheckedProperty);

            //this.AddBindingProperty(this.cbbXAlignment, ComboBox.SelectedValueProperty);
        }
    }
}
