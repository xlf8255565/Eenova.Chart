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
    public class StrokeStyleComboBox : ComboBox
    {
        public StrokeStyleComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            this.Items.Add(StrokeStyles.Solid);//实线。
            //this.Items.Add("1 0");//虚线。
            this.Items.Add(StrokeStyles.Dashed);//虚线。
            this.Items.Add(StrokeStyles.Dotted);//点线。
            this.Items.Add(StrokeStyles.Chain);//点划线。
        }

        private void ApplyConfig()
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("/Eenova.Chart;component/Themes/Generic.xaml", System.UriKind.Relative);
            this.ItemTemplate = resources["StrokeStyleItemTemplate"] as DataTemplate;
        }
    }
}
