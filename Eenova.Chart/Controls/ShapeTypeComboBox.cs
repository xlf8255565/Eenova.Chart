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
    public class ShapeTypeComboBox : ComboBox
    {
        public ShapeTypeComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, ShapeType>();
            dict.Add("圆形", ShapeType.Circle);
            dict.Add("正方形", ShapeType.Square);
            dict.Add("三角形", ShapeType.Triangle);
            dict.Add("菱形", ShapeType.Diamond);
            dict.Add("五边形", ShapeType.Pentagon);
            dict.Add("六边形", ShapeType.Hexagon);
            dict.Add("五角星形", ShapeType.Star);
            dict.Add("十字星形", ShapeType.CrossStar);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
