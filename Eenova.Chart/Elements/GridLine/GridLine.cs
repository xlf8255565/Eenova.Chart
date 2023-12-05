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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Controls;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public abstract class GridLine : UIControl
    {
        IList<Polyline> _lineCache = new List<Polyline>();
        IList<FrameworkElement> _effectCache = new List<FrameworkElement>();
        protected const double EFFECT_SIZE = 8;
        protected Panel _linePanel;

        protected override string SettingTitle
        {
            get { return "设置网格线格式"; }
        }

        public GridLine()
        {
            this.DefaultStyleKey = typeof(GridLine);
            this.Init();
        }

        #region protected method

        protected override void LoadSetter()
        {
            SettingWindow.Add("图案", new CommonStrokeSetter { DataContext = this });
        }

        protected abstract void SetLineSize(Polyline line, double offset);
        protected abstract void SetEffectOffset(UIElement effect1, UIElement effect2, double offset);
        protected abstract void SetOffset(double offset);
        protected abstract void SetTransform();
        protected abstract void SetLineTransform(Polyline line);

        #endregion

        #region private method

        private void Init()
        {
            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            _linePanel = new Canvas();
            this.LineHost = new Canvas();
            this.SelectedEffect = new Canvas() { Visibility = Visibility.Collapsed };
            this.LineHost.Children.Add(_linePanel);
            this.LineHost.Children.Add(this.SelectedEffect);

            this.Intervals = new List<double>() { 0, 20, 40, 60, 80, 100 };
            this.SizeChanged += (s, e) => this.Load();
        }

        private void Load()
        {
            this.LoadChildren();
            this.SetOffset();
            this.SetTransform();
        }

        private void LoadChildren()
        {
            if (Intervals == null || Intervals.Count < 2)
            {
                this.ClearChildren();
                return;
            }
            for (var i = 0; i < Intervals.Count - 1; i++)
            {
                this.AddChilren(i);
            }
            this.RemoveSurplusChildren();
        }

        private void ClearChildren()
        {
            _linePanel.Children.Clear();
            ((Panel)SelectedEffect).Children.Clear();
        }

        private void AddChilren(int index)
        {
            var effectPanel = (Panel)SelectedEffect;
            while (index >= _linePanel.Children.Count)
            {
                _linePanel.Children.Add(CreateLine(_linePanel.Children.Count));
                effectPanel.Children.Add(CreateEffect(effectPanel.Children.Count));
                effectPanel.Children.Add(CreateEffect(effectPanel.Children.Count));
            }

            this.SetLineSize(
                (Polyline)_linePanel.Children[index],
                (int)(Intervals[index] + StrokeThickness / 2));

            this.SetEffectOffset(
                effectPanel.Children[index * 2],
                effectPanel.Children[index * 2 + 1],
                Intervals[index + 1] - (EFFECT_SIZE - StrokeThickness) / 2);
        }

        private Polyline CreateLine(int index)
        {
            if (index >= _lineCache.Count)
            {
                var line = new Polyline() { Points = new PointCollection() { new Point(), new Point() } };
                line.SetBinding(Polyline.StrokeThicknessProperty, new Binding("StrokeThickness") { Source = this });
                line.SetBinding(Polyline.StrokeProperty, new Binding("Stroke") { Source = this });
                line.SetBinding(Polyline.StrokeDashArrayProperty, new Binding("StrokeStyle") { Source = this });
                line.SetBinding(Polyline.VisibilityProperty, new Binding("StrokeVisibility") { Source = this });
                this.SetLineTransform(line);
                _lineCache.Add(line);
            }
            return _lineCache[index];
        }

        private FrameworkElement CreateEffect(int index)
        {
            if (index >= _effectCache.Count)
            {
                _effectCache.Add(new EffectCircle
                {
                    Width = EFFECT_SIZE,
                    Height = EFFECT_SIZE
                });
            }
            return _effectCache[index];
        }

        private void RemoveSurplusChildren()
        {
            var effectPanel = (Panel)SelectedEffect;
            while (Intervals.Count <= _linePanel.Children.Count)
            {
                _linePanel.Children.RemoveAt(_linePanel.Children.Count - 1);
                effectPanel.Children.RemoveAt(effectPanel.Children.Count - 1);
                effectPanel.Children.RemoveAt(effectPanel.Children.Count - 1);
            }
        }

        private void SetOffset()
        {
            var offset = -(this.StrokeThickness % 2 == 0 ? this.StrokeThickness / 2 : this.StrokeThickness / 2 + 0.5);
            this.SetOffset(offset);
        }

        private void SetLineTransform()
        {
            foreach (var line in _lineCache)
            {
                this.SetLineTransform(line);
            }
        }

        #endregion private method

        #region dp

        #region LineHost

        internal Panel LineHost
        {
            get { return (Panel)GetValue(LineHostProperty); }
            private set { SetValue(LineHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty LineHostProperty =
            DependencyProperty.Register("LineHost", typeof(Panel), typeof(GridLine), null);

        #endregion LineHost

        #region Intervals

        /// <summary>
        /// 网格线的间距。
        /// </summary>
        public IList<double> Intervals
        {
            get { return (IList<double>)GetValue(IntervalsProperty); }
            set { SetValue(IntervalsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Intervals.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalsProperty =
            DependencyProperty.Register("Intervals", typeof(IList<double>), typeof(GridLine),
            new PropertyMetadata(OnIntervalsChanged));

        private static void OnIntervalsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as GridLine;
            source.Load();
        }

        #endregion

        #region IsDesc

        /// <summary>
        /// 是否次序反转,默认false。
        /// </summary>
        public bool IsDesc
        {
            get { return (bool)GetValue(IsDescProperty); }
            set { SetValue(IsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDescProperty =
            DependencyProperty.Register("IsDesc", typeof(bool), typeof(GridLine),
            new PropertyMetadata(false, OnIsDescChanged));

        private static void OnIsDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as GridLine;
            source.SetOffset();
            source.SetTransform();
        }

        #endregion IsDesc

        #region StrokeThickness

        /// <summary>
        /// 网格线的厚度，默认1.
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(GridLine),
            new PropertyMetadata((double)1, OnStrokeThicknessChanged));

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as GridLine;
            var style = source.StrokeStyle;
            source.StrokeStyle = StrokeStyles.Rush;
            source.StrokeStyle = style;
            source.Load();
            source.SetLineTransform();
        }

        #endregion StrokeThickness

        #region Stroke

        /// <summary>
        /// 网格线的颜色，默认Color.FromArgb(255, 23, 55, 95)。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(GridLine),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 23, 55, 95))));

        #endregion Stroke

        #region StrokeVisibility

        /// <summary>
        /// 网格线是否可见，默认Visible。
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(GridLine),
            new PropertyMetadata(Visibility.Visible));

        #endregion StrokeVisibility

        #region StrokeStyle

        /// <summary>
        /// 网格线样式，默认Dashed。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(GridLine),
            new PropertyMetadata(StrokeStyles.Dashed));

        #endregion

        #endregion
    }
}
