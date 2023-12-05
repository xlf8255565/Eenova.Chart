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


using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Elements
{
    public partial class AxisNoteAddWindow : ChildWindow
    {
        PlotArea _plotArea;

        private AxisNoteAddWindow(PlotArea plotArea)
        {
            InitializeComponent();
            this.HasCloseButton = false;

            _plotArea = plotArea;
            this.nbStart.Value = _plotArea.AxisX.MinValue;
            this.nbEnd.Value = _plotArea.AxisX.MaxValue;
        }

        public static void Show(PlotArea plotArea)
        {
            if (plotArea.AxisX.DataType == DataType.Text)
            {
                MessageBox.Show("文本轴暂不支持此项设置！");
            }
            else
            {
                var window = new AxisNoteAddWindow(plotArea);
                window.ShowWindow();
            }
        }

        private void ShowWindow()
        {
            Eenova.Chart.Setter.SettingWindow.NotifyOpening();

            if (_plotArea.AxisX.DataType == DataType.DateTime)
            {
                this.DateTimePanel.Visibility = Visibility.Visible;
                this.NumbricPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.DateTimePanel.Visibility = Visibility.Collapsed;
                this.NumbricPanel.Visibility = Visibility.Visible;
            }

            this.Show();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (this.DialogResult.HasValue &&
                this.DialogResult.Value)
            {
                var note = new AxisNote
                {
                    StartValue = this.nbStart.Value,
                    EndValue = this.nbEnd.Value,
                    Top = this.nbOffset.Value
                };
                _plotArea.Notes.Add(note);
            }

            _plotArea = null;

            base.OnClosing(e);

            Eenova.Chart.Setter.SettingWindow.NotifyClosing();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

