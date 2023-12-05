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


using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace Eenova.Chart.Elements
{
    public class AlarmBoard : Grid
    {
        internal bool IsAutoExtend { get; private set; }
        internal Axis MainAxis { get; private set; }
        internal Axis SubAxis { get { return MainAxis.AlarmSubAxis; } }

        public AlarmBoard(Axis mainAxis)
        {
            MainAxis = mainAxis;

            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            //this.SizeChanged += (s, e) => this.SetClip();
        }

        public void Draw(IEnumerable<AlarmGroup> alarmGroups, bool isAutoExtend = true)
        {
            IsAutoExtend = isAutoExtend;
            AlarmGroups = alarmGroups;
            this.AddChildren();
        }

        public void MergeDraw(IEnumerable<AlarmGroup> alarmGroups)
        {
            if (alarmGroups == null)
                return;

            if (AlarmGroups == null)
            {
                AlarmGroups = alarmGroups;
            }
            else
            {
                var groups = new List<AlarmGroup>(AlarmGroups);
                foreach (var group in alarmGroups)
                {
                    var oldGroup = groups.Where(p => p.Deep == group.Deep).FirstOrDefault();
                    if (oldGroup != null)
                    {
                        if (oldGroup.NeedDraw && group.NeedDraw)
                        {
                            oldGroup.Start = Math.Min(oldGroup.Start, group.Start);
                            oldGroup.End = Math.Max(oldGroup.End, group.End);
                        }
                        else if (oldGroup.NeedDraw == false && group.NeedDraw)
                        {
                            oldGroup.Start = group.Start;
                            oldGroup.End = group.End;
                            oldGroup.NeedDraw = true;
                        }
                    }
                    else
                    {
                        groups.Add(group);
                    }
                }

                groups = groups.OrderBy(p => p.Deep).ToList();
                for (int i = 0; i < groups.Count - 1; i++)
                {
                    var g1 = groups[i];
                    var g2 = groups[i + 1];
                    if (g1.NeedDraw && g2.NeedDraw)
                    {
                        g2.Start = g1.End;
                    }
                }

                AlarmGroups = groups;
            }
            this.AddChildren();
        }

        public void Clear()
        {
            this.AlarmGroups = null;
            this.Children.Clear();
        }

        private void AddChildren()
        {
            this.Children.Clear();

            if (AlarmGroups == null || AlarmGroups.Count() == 0)
                return;

            if (IsAutoExtend == true)
            {
                this.DrawMax();
            }
            else
            {
                this.DrawAll();
            }

            this.Load();
        }

        internal void Load()
        {
            if (this.IsAutoExtend)
                this.DrawMax();

            foreach (AlarmRegion alarmRegion in this.Children)
            {
                alarmRegion.Load();
            }
            //this.UpdateLayout();
            //this.SetClip();
            this.SetTransform();
        }

        private void DrawMax()
        {
            //var maxDeep = AlarmGroups.Max(p => p.Deep);
            //var alarmGroup = AlarmGroups.FirstOrDefault(p => p.Deep == maxDeep);
            //var alarmRegion = new AlarmRegion(this, alarmGroup);
            //this.Children.Add(alarmRegion);

            if (this.AlarmGroups == null ||
                this.AlarmGroups.Count() == 0)
                return;

            if (this.MainAxis == null ||
                this.MainAxis.DataLinks == null ||
                this.MainAxis.DataLinks.Count() == 0)
                return;

            var maxDeep = this.MainAxis.DataLinks.Max(p => p.Deep);
            var group = this.AlarmGroups.FirstOrDefault(p => p.Deep == maxDeep);
            if (group == null)
            {
                group = new AlarmGroup { Deep = maxDeep };
                var list = this.AlarmGroups.ToList();
                list.Add(group);
                this.AlarmGroups = list;
            }

            if (this.Children.Count == 0)
            {
                var region = new AlarmRegion(this, group);
                this.Children.Add(region);
            }
            else
            {
                var region = this.Children[0] as AlarmRegion;
                if (region.Deep == maxDeep)
                {
                    region.Load();
                }
                else
                {
                    this.Children.Clear();
                    this.Children.Add(new AlarmRegion(this, group));
                }
            }
        }

        private void DrawAll()
        {
            foreach (var group in AlarmGroups)
            {
                if (group.NeedDraw)
                {
                    var region = new AlarmRegion(this, group);
                    this.Children.Add(region);
                }
            }
        }

        private void SetClip()
        {
            this.Clip = new RectangleGeometry { Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight) };
        }

        private void SetTransform()
        {
            if (MainAxis.IsAxisX)
            {
                ((CompositeTransform)this.RenderTransform).ScaleX = MainAxis.IsDesc ? -1 : 1;
            }
            else
            {
                ((CompositeTransform)this.RenderTransform).ScaleY = MainAxis.IsDesc ? 1 : -1;
            }

            if (this.IsAutoExtend == false && this.SubAxis != null)
            {
                if (SubAxis.IsAxisX)
                {
                    ((CompositeTransform)this.RenderTransform).ScaleX = SubAxis.IsDesc ? -1 : 1;
                }
                else
                {
                    ((CompositeTransform)this.RenderTransform).ScaleY = SubAxis.IsDesc ? 1 : -1;
                }
            }
        }

        internal IEnumerable<AlarmGroup> AlarmGroups
        {
            get { return (IEnumerable<AlarmGroup>)GetValue(AlarmGroupsProperty); }
            set { SetValue(AlarmGroupsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmGroups.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AlarmGroupsProperty =
            DependencyProperty.Register("AlarmGroups", typeof(IEnumerable<AlarmGroup>), typeof(AlarmBoard), null);
    }
}
