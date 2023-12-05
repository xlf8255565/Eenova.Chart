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
    public partial class CommonFontSetter : BaseSetter
    {
        public CommonFontSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbbFontFamily, ComboBox.SelectedValueProperty);
            this.AddBindingProperty(this.cbbFontSize, ComboBox.SelectedItemProperty);
            this.AddBindingProperty(this.cpForeground, ColorPicker.SelectedColorProperty);
            this.AddBindingProperty(this.cpBackground, ColorPicker.SelectedColorProperty);
            this.AddBindingProperty(this.cbTextDecorations, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbFontStyle, CheckBox.IsCheckedProperty);
            this.AddBindingProperty(this.cbFontWeight, CheckBox.IsCheckedProperty);
        }
    }
}
