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
using Eenova.Chart.Behaviors;

namespace Eenova.App
{
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();

            var chart = new Chart.Elements.Chart();
            var area = new Chart.Elements.PlotArea();
            var link = new Chart.Elements.DataLink();
            new DataLinkMovePointBehavior().Attach(link);
            link.MarkSize = 10;

            link.Stroke = new SolidColorBrush(Colors.Red);
            link.DataPoints.Add(new Chart.Elements.DataPoint(10, -50));
            link.DataPoints.Add(new Chart.Elements.DataPoint(10, 10));
            link.DataPoints.Add(new Chart.Elements.DataPoint(20, 20));
            link.DataPoints.Add(new Chart.Elements.DataPoint(30, -80));
            link.DataPoints.Add(new Chart.Elements.DataPoint(35, -80));
            link.DataPoints.Add(new Chart.Elements.DataPoint(40, 0));
            link.DataPoints.Add(new Chart.Elements.DataPoint(50, 0));
            link.DataPoints.Add(new Chart.Elements.DataPoint(50, -60));
            area.DataLinks.Add(link);
            chart.PlotAreas.Add(area);

            this.LayoutRoot.Children.Add(chart);
        }

        // 当用户导航到此页面时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
