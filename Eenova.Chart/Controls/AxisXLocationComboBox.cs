using System.Collections.Generic;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class AxisXLocationComboBox : ComboBox
    {
        /// <summary>
        /// X轴标题和标签方位的选择。
        /// </summary>
        public AxisXLocationComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, AxisLocation>();
            dict.Add("上方", AxisLocation.TopOrLeft);
            dict.Add("下方", AxisLocation.BottomOrRight);
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
