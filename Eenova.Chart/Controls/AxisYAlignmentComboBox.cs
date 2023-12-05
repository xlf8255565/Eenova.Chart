using System.Collections.Generic;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
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
