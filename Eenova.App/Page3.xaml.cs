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
using System.Xml.Linq;
using Eenova.Chart.Elements;
using Eenova.Chart;
using System.Windows.Threading;

namespace Eenova.App
{
    public partial class Page3 : Page
    {
        DispatcherTimer timer;
        public Page3()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(2);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            NewMethod();
        }

        // 当用户导航到此页面时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        int j = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //timer.Start();
            NewMethod();
        }

        Chart.Elements.Chart c;
        private void NewMethod()
        {
            j++;
            ////if (c != null)
            ////    c.PlotAreas[0].AxisY1.CloseSettingWindow();

            c = new Chart.Elements.Chart();
            c.Load(XElement.Load("XMLFile1.xml"));
            //c.PlotAreas.Add(new PlotArea());
            //c.TitleNotes.Add(new TitleNote());
            //c.TitleNotes[0].Text=i.ToString();
            this.Panel.Children.Clear();
            this.Panel.Children.Add(c);
            this.btn.Content = j;
            var rd = new Random();
            for (int i = 0; i < 1; i++)
            {
                var dl = new DataLink
                {
                    XDataType = DataType.Text,
                    YDataType = DataType.Numberic
                };
                c.PlotAreas[0].DataLinks.Add(dl);
                dl.DataPoints.Add(new DataPoint { XValue = "Jan", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "Feb", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "Mar", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "Apr", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "May", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "June", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "July", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "Aug", YValue = rd.Next(3000, 8000) });
                dl.DataPoints.Add(new DataPoint { XValue = "Sept", YValue = rd.Next(3000, 8000) });

                //dl.ToDelete += new EventHandler(dl_Deleted);
            }
            //c.PlotAreas[0].AxisY1.ShowSettingWindow();
            c.ChildRemoved += new EventHandler<ChildRemovedEventArgs>(c_ChildRemoved);
            c.PlotAreas[0].ChildRemoved += new EventHandler<ChildRemovedEventArgs>(Page3_ChildRemoved);
        }

        void Page3_ChildRemoved(object sender, ChildRemovedEventArgs e)
        {
            //throw new NotImplementedException();
            var plotArea = sender as PlotArea;
            var dataLink = e.RemovedChild as DataLink;
        }

        void c_ChildRemoved(object sender, ChildRemovedEventArgs e)
        {
            //throw new NotImplementedException();
        }


        void dl_Deleted(object sender, EventArgs e)
        {
            //MessageBox.Show("ggg");
        }
    }
}
