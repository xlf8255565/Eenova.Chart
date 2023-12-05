using System.Collections.Generic;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// Y轴标题和标签方位选择。
    /// </summary>
    public class AxisYLocationComboBox : ComboBox
    {
        public AxisYLocationComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, AxisLocation>();
            dict.Add("左侧", AxisLocation.TopOrLeft);
            dict.Add("右侧", AxisLocation.BottomOrRight);
            dict.Add("无", AxisLocation.None);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
