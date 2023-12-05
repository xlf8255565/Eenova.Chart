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
using System.Windows.Input;
using System.Windows.Threading;

namespace Eenova.Chart.Setter
{
    public partial class SettingWindow : ChildWindow
    {
        public static event EventHandler Closing;
        public static event EventHandler Opening;

        private static Style NEW_STYLE;
        private static int i = 0;

        internal int SelectedIndex
        {
            get { return this.ItemList.SelectedIndex; }
            set
            {
                if (this.ItemList.Items.Count > value)
                {
                    this.ItemList.SelectedIndex = value;
                }
            }
        }

        public SettingWindow()
        {
            InitializeComponent();

            Debug.WriteLine(i);
            i++;

            if (NEW_STYLE == null)
            {
                var resources = new ResourceDictionary
                {
                    Source = new Uri("/Eenova.Chart;component/Themes/Generic.xaml", UriKind.Relative)
                };
                NEW_STYLE = resources["SettingWindowStyle"] as Style;
            }

            this.Style = NEW_STYLE;
        }

        public new void Show()
        {
            NotifyOpening();

            base.Show();
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CancelButton_Click(null, null);
                return;
            }

            base.OnKeyDown(e);
        }

        internal void Add(string header, ISetter setter)
        {
            this.ItemList.Items.Add(new SettingItem
            {
                Header = header,
                Content = setter
            });
        }

        internal void Apply()
        {
            foreach (SettingItem item in this.ItemList.Items)
            {
                if (item.Content != null)
                    item.Content.Apply();
            }
        }

        internal void Load()
        {
            foreach (SettingItem item in this.ItemList.Items)
            {
                if (item.Content != null)
                    item.Content.Load();
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            this.Apply();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Apply();
            NotifyClosing();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            NotifyClosing();
        }

        internal static void NotifyOpening()
        {
            if (Opening != null)
            {
                System.Diagnostics.Debug.WriteLine("NotifyOpening");
                Opening(null, null);
            }
        }

        internal static void NotifyClosing()
        {
            var handle = Closing;
            if (handle != null)
            {
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    System.Diagnostics.Debug.WriteLine("NotifyClosing");
                    handle.Invoke(null, null);
                };
                timer.Start();
            }
        }
    }
}

