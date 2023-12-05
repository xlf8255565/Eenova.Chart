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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Helpers;
using System.Diagnostics;

namespace Eenova.Chart.Elements
{
    public abstract class AxisTicks : ContentControl
    {
        #region field

        Panel _topHost = new Grid();
        Panel _centerHost = new Grid();
        Panel _bottomHost = new Grid();

        IList<Line> _topCache = new List<Line>();
        IList<Line> _centerCache = new List<Line>();
        IList<Line> _bottomCache = new List<Line>();

        protected Border _topFixer = new Border();
        protected Border _centerFixer = new Border();
        protected Border _bottomFixer = new Border();

        #endregion

        public AxisTicks()
        {
            this.Init();
        }

        #region protected

        protected abstract Panel LoadRootHost();
        protected abstract void FixSize();
        protected abstract void SetTransform();
        protected abstract void SetTickCoord(Line line, double offset, double height);

        #endregion

        #region private

        private void Init()
        {
            this.VerticalAlignment = VerticalAlignment.Center;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Content = this.LoadRootHost();
            this.Ticks = new ObservableCollection<double>();
            _topFixer.Child = _topHost;
            _centerFixer.Child = _centerHost;
            _bottomFixer.Child = _bottomHost;
            ((Panel)this.Content).Children.Add(_topFixer);
            ((Panel)this.Content).Children.Add(_centerFixer);
            ((Panel)this.Content).Children.Add(_bottomFixer);

            this.Load();
            this.SetTransform();
        }

        private void Load()
        {
            //Debug.WriteLine("Load top ticks");
            this.LoadTicks(_topHost, _topCache);
            //Debug.WriteLine("Load center ticks");
            this.LoadTicks(_centerHost, _centerCache);
            //Debug.WriteLine("Load bottom ticks");
            this.LoadTicks(_bottomHost, _bottomCache);
            //Debug.WriteLine("Load ticks over");
            this.FixSize();
        }

        private void LoadTicks(Panel panel, IList<Line> lines)
        {
            if (this.Ticks == null)
            {
                panel.Children.Clear();
                return;
            }
            for (var i = 0; i < this.Ticks.Count; i++)
            {
                this.AddTicks(panel, lines, i);
            }
            this.RemoveSurplusTicks(panel);
        }

        private void AddTicks(Panel panel, IList<Line> lines, int index)
        {
            while (index >= panel.Children.Count)
            {
                panel.Children.Add(this.CreateTick(lines, panel.Children.Count));
            }
            this.SetTickSize((Line)panel.Children[index], this.Ticks[index]);
        }

        private Line CreateTick(IList<Line> lines, int index)
        {
            if (index >= lines.Count)
            {
                var line = new Line() ;
                line.SetBinding(Line.StrokeProperty, new Binding("Foreground") { Source = this });
                line.SetBinding(Line.StrokeThicknessProperty, new Binding("Thickness") { Source = this });
                lines.Add(line);
            }
            return lines[index];
        }

        private void SetTickSize(Line line, double offset)
        {
            if (!double.IsNaN(offset))
            {
                var height = Math.Max(this.Thickness, this.TickHeight) + 1;//线段的高度，只需大于最大值，最终显示是在fixer中限制的。
                offset += this.Thickness / 2;//线段的偏移值，计算线段中心。解决模糊效果。
                this.SetTickCoord(line, offset, height);
            }
        }

        private void RemoveSurplusTicks(Panel panel)
        {
            while (this.Ticks.Count < panel.Children.Count)
            {
                panel.Children.RemoveAt(panel.Children.Count - 1);
            }
        }

        #endregion

        #region dp

        #region Ticks

        /// <summary>
        /// ticks之间的间距。
        /// </summary>
        public IList<double> Ticks
        {
            get { return (IList<double>)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TicksProperty.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TicksProperty =
            DependencyProperty.Register("Ticks", typeof(IList<double>), typeof(AxisTicks),
            new PropertyMetadata(OnTicksChanged));

        private static void OnTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisTicks;
            if (Utility.IsDoubleCollectionMatch((IList<double>)e.OldValue, (IList<double>)e.NewValue))
                return;
            source.Load();
        }

        #endregion Ticks

        #region IsDesc

        /// <summary>
        /// 是否需要逆序。
        /// </summary>
        public bool IsDesc
        {
            get { return (bool)GetValue(IsDescProperty); }
            set { SetValue(IsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDescProperty =
            DependencyProperty.Register("IsDesc", typeof(bool), typeof(AxisTicks),
            new PropertyMetadata(false, OnIsDescChanged));

        private static void OnIsDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisTicks;
            source.SetTransform();
        }

        #endregion IsDesc

        #region Length

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(double), typeof(AxisTicks),
            new PropertyMetadata((double)150));

        #endregion

        #region Thickness

        /// <summary>
        /// tick的宽度。
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(AxisTicks),
            new PropertyMetadata((double)2, OnSizeChanged));

        #endregion Thickness

        #region TickHeight

        /// <summary>
        /// tick的高度。
        /// </summary>
        public double TickHeight
        {
            get { return (double)GetValue(TickHeightProperty); }
            set { SetValue(TickHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickHeightProperty =
            DependencyProperty.Register("TickHeight", typeof(double), typeof(AxisTicks),
            new PropertyMetadata((double)10, OnSizeChanged));

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisTicks;
            source.Load();
        }

        #endregion TickHeight

        #region TicksShow

        /// <summary>
        /// tick的显示方式，默认All。
        /// </summary>
        public TicksShow TicksShow
        {
            get { return (TicksShow)GetValue(TicksShowProperty); }
            set { SetValue(TicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TicksShowProperty =
            DependencyProperty.Register("TicksShow", typeof(TicksShow), typeof(AxisTicks),
            new PropertyMetadata(TicksShow.All, OnTicksShowChanged));

        private static void OnTicksShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisTicks;
            switch ((TicksShow)e.NewValue)
            {
                case TicksShow.All:
                    source._bottomHost.Visibility = Visibility.Visible;
                    source._topHost.Visibility = Visibility.Visible;
                    source._centerHost.Visibility = Visibility.Visible;
                    break;
                case TicksShow.TopOrLeft:
                    source._bottomHost.Visibility = Visibility.Collapsed;
                    source._topHost.Visibility = Visibility.Visible;
                    source._centerHost.Visibility = Visibility.Visible;
                    break;
                case TicksShow.BottomOrRight:
                    source._bottomHost.Visibility = Visibility.Visible;
                    source._topHost.Visibility = Visibility.Collapsed;
                    source._centerHost.Visibility = Visibility.Visible;
                    break;
                default:
                    source._bottomHost.Visibility = Visibility.Collapsed;
                    source._topHost.Visibility = Visibility.Collapsed;
                    source._centerHost.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        #endregion TickShow

        #endregion dp
    }
}
