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
    /// <summary>
    /// 竖直对齐方式选择框.
    /// </summary>
    public class VerticalAlignmentComboBox : BindingComboBox
    {
        public VerticalAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, VerticalAlignment>();
            dict.Add("居上", VerticalAlignment.Top);
            dict.Add("居中", VerticalAlignment.Center);
            dict.Add("居下", VerticalAlignment.Bottom);
            dict.Add("拉伸", VerticalAlignment.Stretch);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }

}
