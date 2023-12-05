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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Eenova.Chart.Elements;

namespace Eenova.App
{
    public partial class MemoryTestPage : Page
    {
        private static Random _random = new Random();

        public MemoryTestPage()
        {
            InitializeComponent();
            this.NewButton.Click += new RoutedEventHandler(NewButton_Click);
            this.ClearButton.Click += (s, e) => this.ChartContainer.Child = null;
        }

        void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var chart = new Chart.Elements.Chart();
            var title = new Chart.Elements.TitleNote
            {
                Text = "内存释放测试",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5)
            };
            chart.TitleNotes.Add(title);

            var plotArea = new Chart.Elements.PlotArea
            {
                Margin = new Thickness(20)
            };
            chart.PlotAreas.Add(plotArea);

            var legend = new Legend
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5),
                DataLinks = plotArea.DataLinks,
            };
            chart.Legends.Add(legend);

            int i = 5;
            while (i > 0)
            {
                plotArea.DataLinks.Add(CreateDataLink());
                i--;
            };

            this.ChartContainer.Child = chart;
        }

        private static DataLink CreateDataLink()
        {
            var dataLink = new Chart.Elements.DataLink();

            var i = 5;
            while (i > 0)
            {
                dataLink.DataPoints.Add(new Chart.Elements.DataPoint(_random.Next(10, 100), _random.Next(100, 999)));
                i--;
            }
            dataLink.Stroke = new SolidColorBrush(Color.FromArgb(255, (byte)_random.Next(0, 255), (byte)_random.Next(0, 255), (byte)_random.Next(0, 255)));

            return dataLink;
        }
    }
}
