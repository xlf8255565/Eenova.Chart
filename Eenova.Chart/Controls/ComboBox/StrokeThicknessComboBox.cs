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
    public class StrokeThicknessComboBox : BindingComboBox
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
