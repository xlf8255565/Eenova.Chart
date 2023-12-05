using System.Collections.Generic;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 关联的Y轴选择框。
    /// </summary>
    public class AxisYTypeComboBox : ComboBox
    {
        public AxisYTypeComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, PlotY>();
            dict.Add("左上纵轴", PlotY.Y1);
            dict.Add("右上纵轴", PlotY.Y3);
            dict.Add("左下纵轴", PlotY.Y2);
            dict.Add("右下纵轴", PlotY.Y4);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
