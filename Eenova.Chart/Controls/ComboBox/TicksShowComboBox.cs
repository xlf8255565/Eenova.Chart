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
    public class TicksShowXComboBox : ComboBox
    {
        public TicksShowXComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, TicksShow>();
            dict.Add("全部", TicksShow.All);
            dict.Add("上方", TicksShow.TopOrLeft);
            dict.Add("下方", TicksShow.BottomOrRight);
            dict.Add("无", TicksShow.None);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }

    public class TicksShowYComboBox : ComboBox
    {
        public TicksShowYComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, TicksShow>();
            dict.Add("全部", TicksShow.All);
            dict.Add("左侧", TicksShow.TopOrLeft);
            dict.Add("右侧", TicksShow.BottomOrRight);
            dict.Add("无", TicksShow.None);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }

}
