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
}
