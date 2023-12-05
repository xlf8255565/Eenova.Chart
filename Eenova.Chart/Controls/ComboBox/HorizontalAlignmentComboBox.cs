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


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class HorizontalAlignmentComboBox : BindingComboBox
    {
        public HorizontalAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, HorizontalAlignment>();
            dict.Add("居左", HorizontalAlignment.Left);
            dict.Add("居中", HorizontalAlignment.Center);
            dict.Add("居右", HorizontalAlignment.Right);
            dict.Add("拉伸", HorizontalAlignment.Stretch);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
