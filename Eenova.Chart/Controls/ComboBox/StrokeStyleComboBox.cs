/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/


using System;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class StrokeStyleComboBox : BindingComboBox
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
