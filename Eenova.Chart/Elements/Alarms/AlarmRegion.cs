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
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Helpers;
using Eenova.Chart.Setter;
using System.Windows.Input;

namespace Eenova.Chart.Elements
{
    public class AlarmRegion : UIControl
    {
        bool _ignoreChange = false;
        AlarmGroup _alarmGroup;

        protected override bool HasContextMenu { get { return false; } }
        protected override string SettingTitle { get { return "设置报警范围"; } }

        public AlarmRegion(AlarmBoard bord, AlarmGroup group)
        {
            this.DefaultStyleKey = typeof(AlarmRegion);

            this.RegionHost = new Grid();

            ParentBoard = bord;

            this.Deep = group.Deep;

            _alarmGroup = new AlarmGroup
            {
                Deep = group.Deep,
                Start = group.Start,
                End = group.End,
                Alarms = group.Alarms
            };

            _ignoreChange = true;
            AlarmGroup = group;
            _ignoreChange = false;

            this.SetToolTip();
        }

        internal void Load()
        {
            this.RegionHost.Children.Clear();

            if (ParentBoard.MainAxis == null || ParentBoard.MainAxis.DataType == DataType.Text)
                return;

            if (ParentBoard.IsAutoExtend)
            {
                this.RegionHost.Children.Add(this.CreateBack());

                foreach (var item in _alarmGroup.Alarms)
                {
                    var values = this.GetConvertValues(ParentBoard.MainAxis, item.Min, item.Max);
                    if (values == null || values[0] >= values[1])
                        continue;

                    var element = this.CreateArea(values[0], values[1]);
                    element.Fill = new SolidColorBrush(item.Color);
                    this.RegionHost.Children.Add(element);
                }
            }
            else
            {
                if (ParentBoard.SubAxis == null || ParentBoard.SubAxis.DataType == DataType.Text)
                    return;

                var valuesEx = this.GetConvertValues(ParentBoard.SubAxis, _alarmGroup.Start, _alarmGroup.End);
                if (valuesEx == null || valuesEx[0] >= valuesEx[1])
                    return;

                var rect = this.CreateBack(valuesEx[0], valuesEx[1]);
                this.RegionHost.Children.Add(rect);

                foreach (var item in _alarmGroup.Alarms)
                {
                    var values = this.GetConvertValues(ParentBoard.MainAxis, item.Min, item.Max);
                    if (values == null || values[0] >= values[1])
                        continue;

                    var element = this.CreateArea(values[0], values[1], valuesEx[0], valuesEx[1]);
                    element.Fill = new SolidColorBrush(item.Color);
                    this.RegionHost.Children.Add(element);
                }
            }
            this.RegionHost.UpdateLayout();
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("切换报警", new AlarmRegionSetter { DataContext = this });
        }

        protected override void UIControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsHitEnable && !e.Handled)
            {
                e.Handled = this.CheckDoubleClick();
            }
        }

        private Rectangle CreateBack()
        {
            var rect = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Transparent),
            };
            return rect;
        }

        private Rectangle CreateArea(double start, double end)
        {
            var rect = new Rectangle();
            if (ParentBoard.MainAxis.IsAxisX)
            {
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                //rect.Width = Math.Max(end - start, 0);
                //rect.Margin = new Thickness(start, 0, 0, 0);
                rect.Width = start < 0 ? end : end - start;
                rect.Margin = new Thickness(Math.Max(start, 0), 0, 0, 0);
            }
            else
            {
                rect.VerticalAlignment = VerticalAlignment.Top;
                //rect.Height = Math.Max(end - start, 0);
                //rect.Margin = new Thickness(0, start, 0, 0);
                rect.Height = start < 0 ? end : end - start;
                rect.Margin = new Thickness(0, Math.Max(start, 0), 0, 0);
            }
            return rect;
        }

        private Rectangle CreateBack(double startEx, double endEx)
        {
            var rect = this.CreateBack();

            //var v1 = Math.Max(endEx - startEx, 0);
            if (ParentBoard.MainAxis.IsAxisX)
            {
                rect.Height = startEx < 0 ? endEx : endEx - startEx;
                rect.VerticalAlignment = VerticalAlignment.Top;
                rect.Margin = new Thickness(0, Math.Max(startEx, 0), 0, 0);
            }
            else
            {
                //rect.Width = v1;
                rect.Width = startEx < 0 ? endEx : endEx - startEx;
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                //rect.Margin = new Thickness(startEx, 0, 0, 0);
                rect.Margin = new Thickness(Math.Max(startEx, 0), 0, 0, 0);
            }
            return rect;
        }

        private Rectangle CreateArea(double start, double end, double startEx, double endEx)
        {
            var rect = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            var v1 = Math.Max(end - start, 0);
            if (ParentBoard.MainAxis.IsAxisX)
            {
                rect.Width = start < 0 ? end : end - start;
                rect.Height = startEx < 0 ? endEx : endEx - startEx;
                rect.Margin = new Thickness(Math.Max(start, 0), Math.Max(startEx, 0), 0, 0);
            }
            else
            {
                rect.Height = start < 0 ? end : end - start;
                //rect.Width = Math.Max(endEx - startEx, 0);
                rect.Width = startEx < 0 ? endEx : endEx - startEx;
                //rect.Margin = new Thickness(startEx, start, 0, 0);
                rect.Margin = new Thickness(Math.Max(startEx, 0), Math.Max(start, 0), 0, 0);
            }
            return rect;
        }

        private IList<double> GetConvertValues(Axis axis, double min, double max)
        {
            if (min >= axis.MaxValue || max <= axis.MinValue)
                return null;

            min = Math.Max(axis.MinValue, min);
            max = Math.Min(axis.MaxValue, max);

            IEnumerable region;
            if (axis.DataType == DataType.Numberic)
                region = new List<double> { min, max };
            else
                region = new List<DateTime> { TimeHelper.GetTime(min), TimeHelper.GetTime(max) };

            var values = axis.Convert(region);
            if (double.IsNaN(values[1]))
                return null;

            if (double.IsNaN(values[0]))
                values[0] = 0;

            values[0] = Math.Round(values[0]);
            values[1] = Math.Round(values[1]);

            return values;
        }

        private void SetToolTip()
        {
            //ToolTipService.SetToolTip(this, string.Format("初始报警范围：{0}", Deep));
            ToolTipService.SetToolTip(this, string.Format("初始报警范围：{0}\r\n当前报警范围：{1}", Deep, _alarmGroup.Deep));
        }

        #region RegionHost

        internal Panel RegionHost
        {
            get { return (Panel)GetValue(RegionHostProperty); }
            set { SetValue(RegionHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegionHost.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty RegionHostProperty =
            DependencyProperty.Register("RegionHost", typeof(Panel), typeof(AlarmRegion), null);

        #endregion

        #region ParentBoard

        internal AlarmBoard ParentBoard
        {
            get { return (AlarmBoard)GetValue(ParentBoardProperty); }
            private set { SetValue(ParentBoardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentBoard.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ParentBoardProperty =
            DependencyProperty.Register("ParentBoard", typeof(AlarmBoard), typeof(AlarmRegion), null);

        #endregion

        #region AlarmGroup

        internal AlarmGroup AlarmGroup
        {
            get { return (AlarmGroup)GetValue(AlarmGroupProperty); }
            set { SetValue(AlarmGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmGroup.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AlarmGroupProperty =
            DependencyProperty.Register("AlarmGroup", typeof(AlarmGroup), typeof(AlarmRegion),
            new PropertyMetadata(OnAlarmGroupChanged));

        private static void OnAlarmGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AlarmRegion;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            var group = ((AlarmGroup)e.NewValue);
            source._alarmGroup.Deep = group.Deep;
            source._alarmGroup.Alarms = group.Alarms;
            source.SetToolTip();
            source.Load();
        }

        #endregion

        #region Deep

        public double Deep
        {
            get { return (double)GetValue(DeepProperty); }
            set { SetValue(DeepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Deep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeepProperty =
            DependencyProperty.Register("Deep", typeof(double), typeof(AlarmRegion), null);

        #endregion

    }
}
