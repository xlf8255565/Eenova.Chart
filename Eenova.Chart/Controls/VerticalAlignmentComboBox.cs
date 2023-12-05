using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 竖直对齐方式选择框.
    /// </summary>
    public class VerticalAlignmentComboBox : ComboBox
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
