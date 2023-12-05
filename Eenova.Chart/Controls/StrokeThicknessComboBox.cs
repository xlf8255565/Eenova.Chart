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

namespace Eenova.Chart.Controls
{
    public class StrokeThicknessComboBox : ComboBox
    {
        public StrokeThicknessComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            for (int i = 1; i < 6; i++)
            {
                this.Items.Add((double)i);
            }
        }

        private void ApplyConfig()
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("/Eenova.Chart;component/Themes/Generic.xaml", System.UriKind.Relative);
            this.ItemTemplate = resources["StrokeThicknessItemTemplate"] as DataTemplate;
        }
    }
}
