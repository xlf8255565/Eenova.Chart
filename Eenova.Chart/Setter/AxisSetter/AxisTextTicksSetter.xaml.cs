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
    public partial class AxisTextTicksSetter :BaseSetter
    {
        public AxisTextTicksSetter()
        {
            InitializeComponent();
        }

        protected override void AddBindingProperties()
        {
            this.AddBindingProperty(this.cbIsDesc, CheckBox.IsCheckedProperty);
        }
    }
}
