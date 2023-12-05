using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 绘图区里X轴方位选择.
    /// </summary>
    public class XAlignmentComboBox : ComboBox
    {
        public XAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, VerticalAlignment>();
            dict.Add("上部轴", VerticalAlignment.Top);
            dict.Add("中间轴", VerticalAlignment.Center);
            dict.Add("下部轴", VerticalAlignment.Bottom);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
