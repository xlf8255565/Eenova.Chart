using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Eenova.Chart.Controls
{
    public class HorizontalAlignmentComboBox : ComboBox
    {
        public HorizontalAlignmentComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, HorizontalAlignment>();
            dict.Add("居左", HorizontalAlignment.Left);
            dict.Add("居中", HorizontalAlignment.Center);
            dict.Add("居右", HorizontalAlignment.Right);
            dict.Add("拉伸", HorizontalAlignment.Stretch);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
