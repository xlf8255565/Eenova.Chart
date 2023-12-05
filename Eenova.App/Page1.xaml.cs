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
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Eenova.Chart;
using Eenova.Chart.Elements;
using System.Windows.Media;
using Eenova.Chart.Helpers;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Eenova.App
{
    public partial class Page1 : Page
    {
        DataLink link1;
        DataLink link2;
        DataLink link3;

        public Page1()
        {
            InitializeComponent();

            //var chart = Chart.Elements.Chart.Create(XElement.Load("XMLFile2.xml"));

            link1 = new DataLink() { Text = "示例线", XDataType = DataType.DateTime };
            link2 = new DataLink() { Text = "示例线", XDataType = DataType.DateTime };
            link3 = new DataLink() { Text = "示例线", XDataType = DataType.DateTime };

            button1_Click(null, null);
        }

        // 当用户导航到此网页时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("start clear");

            PlotArea p = this.chart.PlotAreas[0];
            p.DataLinks.Clear();
            link1.DataPoints.Clear();
            link2.DataPoints.Clear();
            link3.DataPoints.Clear();

            Debug.WriteLine("start add data");

            link1.DataPoints.Add(new DataPoint("2010-2-5", 0));
            link1.DataPoints.Add(new DataPoint("2010-2-15", 52));
            link1.DataPoints.Add(new DataPoint("2010-2-15", 62));
            link1.DataPoints.Add(new DataPoint("2010-2-15", 72));
            link1.DataPoints.Add(new DataPoint("2010-2-15", 82));
            link1.DataPoints.Add(new DataPoint("2010-2-16", 82));
            link1.DataPoints.Add(new DataPoint("2010-2-16", 72));
            link1.DataPoints.Add(new DataPoint("2010-2-16", 62));
            link1.DataPoints.Add(new DataPoint("2010-2-16", 32));
            link1.Stroke = new SolidColorBrush(Colors.Blue);


            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 1), 10));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 4), 90));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 6), -10));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 8), -20));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 8), 5));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 8), 50));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 10), -20));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 13), -20));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 15), -20));
            link2.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 25), -20));

            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 11), 70));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 14), 80));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 16), -90));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 17), 60));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 19), -140));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 28), -110));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 18), -10));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 18), 50));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 18), -10));
            link3.DataPoints.Add(new DataPoint(new DateTime(2010, 2, 18), 70));

            //设置按照Y轴数据排序。
            //link1.OrderType = OrderType.OrderByY;

            ////Y轴只有一个区域。
            ////数字轴添加警示区
            //Area1.AxisY1.MarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Red), Start = 0.00, End = 20.00 });
            //Area1.AxisY1.MarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Blue), Start = 20.00, End = 50.00 });
            //Area1.AxisY1.MarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Green), Start = 50.00, End = 90.00 });
            //Area1.AxisY1.MarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Cyan), Start = 40.00, End = 60.00 });
            ////数字轴添加警示线
            //Area1.AxisY1.MarkupLine.MarkupItems.Add(new MarkupLineItem { Position = 50, Style = StrokeStyles.Dashed, Thickness = 4 });

            ////X轴分上下半区。
            ////时间轴添加警示区
            //Area1.AxisX.TopMarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Red), Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 7)), End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 8)) });
            //Area1.AxisX.TopMarkupArea.MarkupItems.Add(new MarkupAreaItem { Brush = new SolidColorBrush(Colors.Blue), Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 9)), End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 11)) });
            ////时间轴添加警示线
            //Area1.AxisX.TopMarkupLine.MarkupItems.Add(new MarkupLineItem { Position = TimeHelper.GetSpanTime(new DateTime(2010, 2, 7)) });


            Debug.WriteLine("end 1");
            p.DataLinks.Add(link1);
            Debug.WriteLine("end 2");
            p.DataLinks.Add(link2);
            Debug.WriteLine("end 3");
            p.DataLinks.Add(link3);
            Debug.WriteLine("end 4");

            //link3.LinkedY = PlotY.Y2;
            //p.AxisY3.DataType = DataType.DateTime;
            //p.AxisY2.DataType = DataType.Text;


            //添加分布线。
            var groups = new List<AlarmGroup>();

            //深度为5.
            var group = new AlarmGroup { Deep = 0 };
            group.Alarms.Add(new Alarm(Colors.Red, 30, 50));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 10, Max = 30 });
            groups.Add(group);

            group = new AlarmGroup { Deep = 0 };
            //group.Alarms.Add(new Alarm(Colors.Red, 80, 85));
            //group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 60, Max = 80 });
            groups.Add(group);

            group = new AlarmGroup { Deep = 0 };
            group.Alarms.Add(new Alarm(Colors.Red, 10, 300));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = -200, Max = 10 });
            groups.Add(group);

            //绘制过程线。自动绘制深度最大的那组数据。

            link1.Deep = 0;
            link1.Text = "报警范围10";
            link2.Deep = 0;
            link2.Text = "报警范围5";
            link3.Deep = 0;
            link3.Text = "报警范围20";
            p.AxisY1.AlarmBoard.Draw(groups);



            if (true)
            {
                //添加过程线。
                groups = new List<AlarmGroup>();
                group = new AlarmGroup
                {
                    Deep = 5,
                    //把时间转化为对应的double类型数据。
                    Start = TimeHelper.GetSpanTime(new DateTime(2005, 2, 1)),
                    End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 6)),
                };
                group.Alarms.Add(new Alarm(Colors.Red, 0, 200));
                group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 10, Max = 30 });
                groups.Add(group);

                group = new AlarmGroup
                {
                    Deep = 10,
                    Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 6)),
                    End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 8)),
                };
                group.Alarms.Add(new Alarm(Colors.Red, 80, 85));
                group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 60, Max = 80 });
                groups.Add(group);

                group = new AlarmGroup
                {
                    Deep = 20,
                    Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 8)),
                    End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 12)),
                };
                group.Alarms.Add(new Alarm(Colors.Red, 10, 40));
                group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 0, Max = 10 });
                groups.Add(group);

                group = new AlarmGroup
                {
                    Deep = 30,
                    Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 12)),
                    End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 20)),
                };
                group.Alarms.Add(new Alarm(Colors.Red, 60, 80));
                group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 20, Max = 60 });
                groups.Add(group);

                //group = new AlarmGroup(40, null, new DateTime(2010, 2, 12));
                //group.Alarms.Add(new Alarm(Colors.Red, 60, 80));
                //group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 20, Max = 60 });
                //groups.Add(group);

                //添加参数false，绘制所有数据。
                p.AxisY1.AlarmBoard.Draw(groups, false);
                //p.AxisY1.AlarmBoard.MergeDraw(groups);
            }

            //var note = new AxisNote(new DateTime(2010, 1, 31), new DateTime(2010, 2, 15));
            //note.Text = "绘制所有数据";//标签内容。
            //note.Top = 100;//上边偏移距离。
            //p.Notes.Add(note);

            //p.AxisY1.VerticalNote.AddItem(-100, "你好 -100.0");//添加标签（位置，内容）
            //p.AxisY1.VerticalNote.AddItem(0.0, "你好 0");
            //p.AxisY1.VerticalNote.AddItem(50.0, "你好 50.0");
            //p.AxisY1.VerticalNote.AddItem(100.0, "你好 100.0");
            //p.AxisY1.VerticalNote.AddItem(-150.0, "你好 -150.0 -150.0 -150.0");
            //p.AxisY1.VerticalNote.AddItem(-200.0, "你好 -200.0");
            //p.AxisY1.VerticalNote.X = 100;//左边偏移距离。

            ////p.AxisY1.IsDesc = true;
            ////p.AxisY1.VerticalNote.Background = new SolidColorBrush(Colors.Red);

            //p.AxisY4.VerticalNote.AddItem(-100, "你好 -100.0");
            //p.AxisY4.VerticalNote.AddItem(0.0, "你好 0");
            //p.AxisY4.VerticalNote.AddItem(50.0, "你好 50.0");
            //p.AxisY4.VerticalNote.AddItem(100.0, "你好 100.0");
            //p.AxisY4.VerticalNote.AddItem(-150.0, "你好 -150.0 -150.0 -150.0");
            //p.AxisY4.VerticalNote.AddItem(-200.0, "你好 -200.0");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Chart.Elements.Chart c = Chart.Elements.Chart.Create(XElement.Load("XMLFile1.xml"));

            var c = new Chart.Elements.Chart();
            c.Load(XElement.Load("XMLFile1.xml"));
            //c.PlotAreas.Add(new PlotArea());
            c.Tag = Guid.NewGuid();
            Debug.WriteLine(c.Tag);
            ((Panel)this.border.Child).Children.Add(c);
            //var rd = new Random();
            //for (int i = 0; i < 60; i++)
            //{
            //    var dl = new DataLink
            //    {
            //        XDataType = DataType.Text,
            //        YDataType = DataType.Numberic
            //    };
            //    dl.DataPoints.Add(new DataPoint { XValue = "Jan", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "Feb", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "Mar", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "Apr", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "May", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "June", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "July", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "Aug", YValue = rd.Next(3000, 8000) });
            //    dl.DataPoints.Add(new DataPoint { XValue = "Sept", YValue = rd.Next(3000, 8000) });
            //    c.PlotAreas[0].DataLinks.Add(dl);
            //}
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            var groups = new List<AlarmGroup>();
            var group = new AlarmGroup
             {
                 Deep = 5,
                 //把时间转化为对应的double类型数据。
                 Start = TimeHelper.GetSpanTime(new DateTime(2010, 1, 19)),
                 End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 6)),
             };
            group.Alarms.Add(new Alarm(Colors.Red, 30, 50));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 10, Max = 30 });
            groups.Add(group);

            group = new AlarmGroup
            {
                Deep = 10,
                Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 1)),
                End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 8)),
            };
            group.Alarms.Add(new Alarm(Colors.Red, 80, 85));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 60, Max = 80 });
            groups.Add(group);

            group = new AlarmGroup
            {
                Deep = 20,
                Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 9)),
                End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 13)),
            };
            group.Alarms.Add(new Alarm(Colors.Red, 10, 40));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 0, Max = 10 });
            groups.Add(group);

            group = new AlarmGroup
            {
                Deep = 30,
                Start = TimeHelper.GetSpanTime(new DateTime(2010, 2, 11)),
                End = TimeHelper.GetSpanTime(new DateTime(2010, 2, 21)),
            };
            group.Alarms.Add(new Alarm(Colors.Red, 60, 80));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 20, Max = 60 });
            groups.Add(group);

            group = new AlarmGroup(40, new DateTime(2010, 2, 20), new DateTime(2010, 2, 22));
            group.Alarms.Add(new Alarm(Colors.Red, 60, 90));
            group.Alarms.Add(new Alarm { Color = Colors.Yellow, Min = 10, Max = 60 });
            groups.Add(group);

            PlotArea p = this.chart.PlotAreas[0];
            //添加参数false，绘制所有数据。
            p.AxisY1.AlarmBoard.MergeDraw(groups);
        }
    }
}
