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
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class AxisXAlignmentComboBox : ComboBox
    {
        /// <summary>
        /// X轴标题的对齐方式选择。
        /// </summary>
        public AxisXAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, AxisAlignment>();
            dict.Add("左侧", AxisAlignment.TopOrLeft);
            dict.Add("中间", AxisAlignment.Center);
            dict.Add("右侧", AxisAlignment.BottomOrRight);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }

    /// <summary>
    /// Y轴标题对齐方式的选择框。
    /// </summary>
    public class AxisYAlignmentComboBox : ComboBox
    {
        public AxisYAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, AxisAlignment>();
            dict.Add("上方", AxisAlignment.TopOrLeft);
            dict.Add("中间", AxisAlignment.Center);
            dict.Add("下方", AxisAlignment.BottomOrRight);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }

}
