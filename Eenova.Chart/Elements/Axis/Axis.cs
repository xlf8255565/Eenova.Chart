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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Eenova.Chart.Controls;
using Eenova.Chart.Factories;
using Eenova.Chart.Helpers;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    /// <summary>
    /// 如要准确定位，需放在canvas中。
    /// </summary>
    public abstract class Axis : UIControl
    {
        #region field

        /// <summary>
        /// 是否正在计算值。
        /// </summary>
        bool _isValueCalculating;
        bool _ignoreChange;

        TicksHelper _ticksHelper;
        LabelsBuilder _labelsBuilder;
        ValueCalculator _valueCalculator;
        CoordConverter _coordConverter;
        Formater _formater;
        TabItem _formatSettingItem;
        TabItem _tickSettingItem;

        IList<DataLink> _dataLinks = new List<DataLink>();

        #endregion field

        #region property

        protected override string SettingTitle
        {
            get { return "设置坐标轴格式"; }
        }

        protected abstract AxisType AxisType { get; }
        protected abstract Point StartPoint { get; }// 坐标轴线条的开始点。
        protected abstract Point EndPoint { get; }// 坐标轴线条的结束点。

        internal abstract double Left { get; }
        internal abstract double Top { get; }
        internal abstract double Right { get; }
        internal abstract double Bottom { get; }

        protected virtual double FixX { get { return 0; } }// 因为标签线有厚度，所以需要在X方向上有个偏移。X轴需重写。
        protected virtual double FixY { get { return 0; } }// 因为标签线有厚度，所以需要在Y方向上有个偏移。Y轴需重写。

        public AxisLabels Labels { get; private set; }
        public Title Title { get; private set; }

        public double MaxData { get; private set; }
        public double MinData { get; private set; }
        public IEnumerable<string> Texts { get; private set; }

        public IEnumerable<DataLink> DataLinks { get { return _dataLinks; } }
        public bool IsAxisX { get { return this.AxisType == AxisType.X; } }

        internal IList<Axis> ExtendAxes { get; private set; }
        internal IList<AlarmBoard> ExtendAlarms { get; private set; }

        #endregion

        #region constructor

        public Axis()
        {
            this.DataType = DataType.Numberic;
            this.Init();
        }

        private void Init()
        {
            this.InitParts();
            //this.InitMarkups();

            this.LoadHelpers();
            this.LoadValueCalculator();
            this.CalculateData();
            this.SizeChanged += (s, e) => SetFix();
        }

        protected virtual void InitParts()
        {
            this.SelectedEffect = new EffectRect();
            this.PositionPresenter = new ContentPresenter();

            this.ExtendAxes = new List<Axis>();
            this.ExtendAlarms = new List<AlarmBoard>();

            this.Labels = this.CreateLabels();
            this.Labels.SetBinding(AxisLabels.IsDescProperty, new Binding("IsDesc") { Source = this });
            this.TopLabelsPresenter = new ContentPresenter();
            this.BottomLabelsPresenter = new ContentPresenter();
            this.SetLabelsLocation();

            this.Title = new Title();
            this.TopTitlePresenter = new ContentPresenter();
            this.BottomTitlePresenter = new ContentPresenter();
            this.SetTitleLocation();
            this.SetTitleAlignment();
        }

        #endregion

        #region abstract method

        internal abstract void FixPosition();
        internal abstract IList<object> GetLinkData(DataLink link);

        protected abstract void LoadMarkups();
        protected abstract GridLine CreateGridLine();
        protected abstract AxisLabels CreateLabels();
        protected abstract void SetTitleAlignment();
        protected abstract DataType GetLinkDataType(DataLink link);
        //protected abstract void InitMarkups();

        #endregion abstract

        #region internal method

        internal IList<double> Convert(IEnumerable data)
        {
            return _coordConverter.Convert(data);
        }

        internal GridLine GenerateGridLine()
        {
            var gridLine = this.CreateGridLine();
            gridLine.SetBinding(GridLine.IsDescProperty, new Binding("IsDesc") { Source = this });
            gridLine.SetBinding(GridLine.IntervalsProperty, new Binding("MainTicks") { Source = this });
            return gridLine;
        }

        internal void Register(DataLink link)
        {
            if (link != null &&
                _dataLinks.Contains(link) == false)
            {
                this.DataType = this.GetLinkDataType(link);
                _dataLinks.Add(link);
                link.PointsChanged += new EventHandler(link_PointsChanged);
                this.CalculateData();
            }
        }

        void link_PointsChanged(object sender, EventArgs e)
        {
            CalculateData();
        }

        internal void Unregister(DataLink link)
        {
            if (link != null &&
                _dataLinks.Contains(link))
            {
                _dataLinks.Remove(link);
                link.PointsChanged -= new EventHandler(link_PointsChanged);
                this.CalculateData();
            }
        }

        internal bool IsDataTypeMatch(DataType dataType)
        {
            return this.DataType == dataType || _dataLinks.Count == 0;
        }

        #endregion

        #region Load Setter method

        protected override void LoadSetter()
        {
            this.LoadTitleSettingItems();
            this.LoadLabelsSettingItems();
            this.LoadTicksSettingItems();
        }

        private void LoadTitleSettingItems()
        {
            var items = new List<TabItem>();
            items.Add(new TabItem { Content = new CommonTextSetter { DataContext = Title }, Header = "名称" });
            items.Add(new TabItem { Content = new AxisTitlePositionSetter { DataContext = this }, Header = "位置" });
            items.Add(new TabItem { Content = new CommonFontSetter { DataContext = Title }, Header = "字体" });
            items.Add(new TabItem { Content = new CommonBorderSetter { DataContext = Title }, Header = "图案" });
            items.Add(new TabItem { Content = new CommonAlignmentSetter { DataContext = Title }, Header = "对齐" });
            SettingWindow.Add("标题", new SettingGroup { ItemsSource = items });
        }

        private void LoadLabelsSettingItems()
        {
            var items = new List<TabItem>();
            items.Add(new TabItem { Content = new AxisLabelsPositionSetter { DataContext = this }, Header = "位置" });
            items.Add(new TabItem { Content = new CommonFontSetter { DataContext = this.Labels }, Header = "字体" });
            items.Add(new TabItem { Content = new CommonAlignmentSetter { DataContext = this.Labels }, Header = "对齐" });

            _formatSettingItem = new TabItem { Header = "格式化" };
            this.LoadFormatSetterContent();
            items.Add(_formatSettingItem);

            SettingWindow.Add("标签", new SettingGroup { ItemsSource = items });
        }

        private void LoadFormatSetterContent()
        {
            if (_formatSettingItem != null)
            {
                switch (this.DataType)
                {
                    case DataType.DateTime:
                        _formatSettingItem.Content = new AxisDateTimeFormatSetter() { DataContext = this };
                        break;
                    case DataType.Numberic:
                        _formatSettingItem.Content = new AxisNumbericFormatSetter() { DataContext = this };
                        break;
                    case DataType.Text:
                        _formatSettingItem.Content = new AxisTextFormatSetter() { DataContext = this };
                        break;
                }
            }
        }

        private void LoadTicksSettingItems()
        {
            var items = new List<TabItem>();
            items.Add(new TabItem { Content = new AxisTicksStrokeSetter { DataContext = this }, Header = "图案" });

            _tickSettingItem = new TabItem { Header = "刻度" };
            this.LoadTicSetterContent();
            items.Add(_tickSettingItem);

            SettingWindow.Add("刻度线", new SettingGroup { ItemsSource = items });
        }

        private void LoadTicSetterContent()
        {
            if (_tickSettingItem != null)
            {
                switch (this.DataType)
                {
                    case DataType.DateTime:
                        _tickSettingItem.Content = new AxisDateTimeTicksSetter() { DataContext = this };
                        break;
                    case DataType.Numberic:
                        _tickSettingItem.Content = new AxisNumbericTicksSetter() { DataContext = this };
                        break;
                    case DataType.Text:
                        _tickSettingItem.Content = new AxisTextTicksSetter() { DataContext = this };
                        break;
                }
            }
        }

        #endregion Load Setter method

        #region private method

        private void LoadHelpers()
        {
            _coordConverter = CoordConverterFactory.Create(this);
            _ticksHelper = IntervalsBuilderFactory.Create(this);
            _labelsBuilder = LabelsBuilderFactory.Create(this);
            _formater = FormaterFactory.Create(this.DataType);
        }

        private void CalculateData()
        {
            var calculator = DataCalculatorFactory.Create(this);
            calculator.Calculate();
            MaxData = calculator.MaxData;
            MinData = calculator.MinData;
            Texts = calculator.Texts;
            this.CalculateLoad();
        }

        private void CalculateSubUnit()
        {
            //需要计算最小刻度
            if (this.DataType == DataType.Numberic && this.IsLogarithm)
                this.SubUnit = this.MainUnit * 2;//todo
            else
                this.SubUnit = this.MainUnit / 5;
        }

        protected virtual void Load()
        {
            foreach (var axis in ExtendAxes)
            {
                axis.FixPosition();
            }


            this.LoadLabels();
            this.LoadMainTicks();
            this.LoadSubTicks();
            this.LoadDataLink();
            this.LoadMarkups();

            foreach (var board in ExtendAlarms)
            {
                if (board.IsAutoExtend == false)
                {
                    board.Load();
                    //board.UpdateLayout();
                }
            }

            this.SetFix();
        }

        private void CalculateLoad()
        {
            _isValueCalculating = true;
            _valueCalculator.Caculate();
            this.MaxValue = _valueCalculator.MaxValue;
            this.MinValue = _valueCalculator.MinValue;
            this.MainUnit = _valueCalculator.MainUnit;
            _isValueCalculating = false;

            this.Load();
        }

        private void LoadDataLink()
        {
            foreach (var dataLink in _dataLinks)
            {
                dataLink.Load();
            }
        }

        private void LoadValueCalculator()
        {
            _valueCalculator = ValueCalculatorFactory.Create(this);
        }

        public void SetFix()
        {
            if (this.PositionPresenter.Parent == null)
                return;
            this.UpdateLayout();
            try
            {
                var g = this.PositionPresenter.TransformToVisual(this);
                var p = g.Transform(this.FixPoint == AxisFixPoint.StartPoint ? this.StartPoint : this.EndPoint);
                Canvas.SetLeft(this, Math.Round(this.FixX - (int)p.X));
                Canvas.SetTop(this, Math.Round(this.FixY - (int)p.Y));
            }
            catch { }
        }

        private void SetTitleLocation()
        {
            this.TopTitlePresenter.Content = null;
            this.BottomTitlePresenter.Content = null;

            switch (this.TitleLocation)
            {
                case AxisLocation.TopOrLeft:
                    this.TopTitlePresenter.Content = this.Title;
                    break;
                case AxisLocation.BottomOrRight:
                    this.BottomTitlePresenter.Content = this.Title;
                    break;
            }
        }

        private void SetLabelsLocation()
        {
            this.TopLabelsPresenter.Content = null;
            this.BottomLabelsPresenter.Content = null;

            switch (this.LabelLocation)
            {
                case AxisLocation.TopOrLeft:
                    this.TopLabelsPresenter.Content = this.Labels;
                    break;
                case AxisLocation.BottomOrRight:
                    this.BottomLabelsPresenter.Content = this.Labels;
                    break;
            };
        }

        private void LoadLabels()
        {
            //格式化在这边处理。
            var labels = _labelsBuilder.GetLabels(p => _formater.Format(p, this.Format));
            this.Labels.Source = labels;
        }

        private void LoadMainTicks()
        {
            this.MainTicks = _ticksHelper.GetMainTicks();
        }

        private void LoadSubTicks()
        {
            this.SubTicks = _ticksHelper.GetSubTicks();
        }

        #endregion private method

        #region dp

        #region Visibility

        /// <summary>
        /// 坐标轴是否可见，重写。
        /// </summary>
        public new Visibility Visibility
        {
            get { return (Visibility)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visibility.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register("Visibility", typeof(Visibility), typeof(Axis),
            new PropertyMetadata(Visibility.Visible, OnVisibilityChanged));

        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            var visible = (Visibility)e.NewValue == Visibility.Visible;
            source.Opacity = System.Convert.ToInt32(visible);//设成透明不可见。
            source.IsHitEnable = visible;//设成点击无效。
        }


        #endregion

        #region FixPoint

        /// <summary>
        /// 在Canvas容器中固定的点。坐标轴的起始点或者结束点,默认EndPoint。
        /// </summary>
        public AxisFixPoint FixPoint
        {
            get { return (AxisFixPoint)GetValue(FixPointProperty); }
            set { SetValue(FixPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FixPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FixPointProperty =
            DependencyProperty.Register("FixPoint", typeof(AxisFixPoint), typeof(Axis),
            new PropertyMetadata(AxisFixPoint.EndPoint, OnFixPointChanged));

        private static void OnFixPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.SetFix();
        }

        #endregion FixPoint

        #region TitleLocation

        /// <summary>
        /// 坐标轴标题的位置，默认BottomOrRight。
        /// </summary>
        public AxisLocation TitleLocation
        {
            get { return (AxisLocation)GetValue(TitleLocationProperty); }
            set { SetValue(TitleLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleLocationProperty =
            DependencyProperty.Register("TitleLocation", typeof(AxisLocation), typeof(Axis),
            new PropertyMetadata(AxisLocation.BottomOrRight, OnTitleLocationChanged));

        private static void OnTitleLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.SetTitleLocation();
            source.SetFix();
        }

        #endregion TitleLocation

        #region TitleAlignment

        /// <summary>
        /// 坐标轴标题的对齐方式，默认BottomOrRight。
        /// </summary>
        public AxisAlignment TitleAlignment
        {
            get { return (AxisAlignment)GetValue(TitleAlignmentProperty); }
            set { SetValue(TitleAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleAlignmentProperty =
            DependencyProperty.Register("TitleAlignment", typeof(AxisAlignment), typeof(Axis),
            new PropertyMetadata(AxisAlignment.BottomOrRight, OnTitleAlignmentChanged));

        private static void OnTitleAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.SetTitleAlignment();
            source.SetFix();
        }

        #endregion TitleAlignment

        #region MainTicksShow

        /// <summary>
        /// 坐标轴主刻度显示，默认BottomOrRight。
        /// </summary>
        public TicksShow MainTicksShow
        {
            get { return (TicksShow)GetValue(MainTicksShowProperty); }
            set { SetValue(MainTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainTicksShowProperty =
            DependencyProperty.Register("MainTicksShow", typeof(TicksShow), typeof(Axis),
            new PropertyMetadata(TicksShow.BottomOrRight));

        #endregion

        #region SubTicksShow

        /// <summary>
        /// 坐标轴次刻度显示，默认BottomOrRight。
        /// </summary>
        public TicksShow SubTicksShow
        {
            get { return (TicksShow)GetValue(SubTicksShowProperty); }
            set { SetValue(SubTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubTicksShowProperty =
            DependencyProperty.Register("SubTicksShow", typeof(TicksShow), typeof(Axis),
            new PropertyMetadata(TicksShow.BottomOrRight));

        #endregion

        #region MainTicks

        /// <summary>
        /// 主刻度间距。
        /// </summary>
        internal IList<double> MainTicks
        {
            get { return (IList<double>)GetValue(MainTicksProperty); }
            private set { SetValue(MainTicksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainTicks.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty MainTicksProperty =
            DependencyProperty.Register("MainTicks", typeof(IList<double>), typeof(Axis), null);

        #endregion

        #region SubTicks

        /// <summary>
        /// 次刻度间距。
        /// </summary>
        internal IList<double> SubTicks
        {
            get { return (IList<double>)GetValue(SubTicksProperty); }
            private set { SetValue(SubTicksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubTicks.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SubTicksProperty =
            DependencyProperty.Register("SubTicks", typeof(IList<double>), typeof(Axis), null);

        #endregion

        #region Format

        /// <summary>
        /// 格式化方式。
        /// </summary>
        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(Axis),
            new PropertyMetadata(OnFormatChanged));

        private static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.LoadLabels();
        }

        #endregion

        #region LabelOffset

        /// <summary>
        /// labels的偏移，根据不同情况在子类中绑定。
        /// </summary>
        public double LabelOffset
        {
            get { return (double)GetValue(LabelOffsetProperty); }
            set { SetValue(LabelOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelOffsetProperty =
            DependencyProperty.Register("LabelOffset", typeof(double), typeof(Axis), null);

        #endregion LabelOffset

        #region LabelLocation

        /// <summary>
        /// labels的位置。默认BottomOrRight。
        /// </summary>
        public AxisLocation LabelLocation
        {
            get { return (AxisLocation)GetValue(LabelLocationProperty); }
            set { SetValue(LabelLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelLocationProperty =
            DependencyProperty.Register("LabelLocation", typeof(AxisLocation), typeof(Axis),
            new PropertyMetadata(AxisLocation.BottomOrRight, OnLabelLocationPropertyChanged));

        private static void OnLabelLocationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.SetLabelsLocation();
            source.SetFix();
        }

        #endregion LabelLocation

        #region StrokeVisibility

        /// <summary>
        /// 数据线是否可见，默认Visible.
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(Axis),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region Stroke

        /// <summary>
        /// 坐标轴线和刻度颜色,默认Black。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Axis),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #region StrokeStyle

        /// <summary>
        /// 轴线样式，StrokeStyles.Solid。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(Axis),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion

        #region StrokeThickness

        /// <summary>
        /// 轴线和刻度线厚度，1。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Axis),
            new PropertyMetadata((double)1, OnStrokeThicknessChanged));

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            var style = source.StrokeStyle;
            source.StrokeStyle = StrokeStyles.Rush;
            source.StrokeStyle = style;
            source.SetFix();
        }

        #endregion

        #region Length

        /// <summary>
        /// 轴线长度,默认300，不是控件长度。
        /// </summary>
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(double), typeof(Axis),
            new PropertyMetadata((double)300, OnLengthChanged));

        private static void OnLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            if ((double)e.NewValue <= 0)
            {
                source._ignoreChange = true;
                source.Length = (double)e.OldValue;
                return;
            }

            source.Load();//需要重新设置labels和ticks
        }

        #endregion

        #region IsDesc

        /// <summary>
        /// 是否逆序，默认false。
        /// </summary>
        public bool IsDesc
        {
            get { return (bool)GetValue(IsDescProperty); }
            set { SetValue(IsDescProperty, value); }
        }

        public static readonly DependencyProperty IsDescProperty =
            DependencyProperty.Register("IsDesc", typeof(bool), typeof(Axis),
            new PropertyMetadata(false, OnIsDescChanged));

        private static void OnIsDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.Load();
        }

        #endregion

        #region IsMinValueAuto IsMaxValueAuto IsMainUnitAuto

        /// <summary>
        /// 最小值自动，默认true.
        /// </summary>
        public bool IsMinValueAuto
        {
            get { return (bool)GetValue(IsMinValueAutoProperty); }
            set { SetValue(IsMinValueAutoProperty, value); }
        }

        public static readonly DependencyProperty IsMinValueAutoProperty =
            DependencyProperty.Register("IsMinValueAuto", typeof(bool), typeof(Axis),
            new PropertyMetadata(true, OnValueAutoChanged));

        /// <summary>
        /// 最大值自动，默认true。
        /// </summary>
        public bool IsMaxValueAuto
        {
            get { return (bool)GetValue(IsMaxValueAutoProperty); }
            set { SetValue(IsMaxValueAutoProperty, value); }
        }

        public static readonly DependencyProperty IsMaxValueAutoProperty =
            DependencyProperty.Register("IsMaxValueAuto", typeof(bool), typeof(Axis),
            new PropertyMetadata(true, OnValueAutoChanged));

        /// <summary>
        /// 主刻度自动，默认true。
        /// </summary>
        public bool IsMainUnitAuto
        {
            get { return (bool)GetValue(IsMainUnitAutoProperty); }
            set { SetValue(IsMainUnitAutoProperty, value); }
        }

        public static readonly DependencyProperty IsMainUnitAutoProperty =
            DependencyProperty.Register("IsMainUnitAuto", typeof(bool), typeof(Axis),
            new PropertyMetadata(true, OnValueAutoChanged));

        private static void OnValueAutoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source.DataType != DataType.Text)
            {
                source.LoadValueCalculator();
                if ((bool)e.NewValue)
                    source.CalculateLoad();
            }
        }

        #endregion

        #region MinValue

        /// <summary>
        /// 最小值，默认0。
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(Axis),
            new PropertyMetadata((double)0, OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            if (source._isValueCalculating || source.DataType == DataType.Text)
                return;

            if ((double)e.NewValue >= source.MaxValue)
            {
                return;
            }

            if (source.DataType == DataType.Numberic && source.IsLogarithm && (double)e.NewValue <= 0)
            {
                source._ignoreChange = true;
                source.MinValue = (double)e.OldValue;
                return;
            }
            source.CalculateLoad();//需要重新设置labels和ticks
        }

        #endregion

        #region MaxValue

        /// <summary>
        /// 最大值，默认100。
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(Axis),
            new PropertyMetadata((double)100, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            if (source._isValueCalculating || source.DataType == DataType.Text)
                return;

            if ((double)e.NewValue <= source.MinValue)
            {
                return;
            }

            if (source.DataType == DataType.Numberic && source.IsLogarithm && (double)e.NewValue <= 0)
            {
                source._ignoreChange = true;
                source.MaxValue = (double)e.OldValue;
                return;
            }
            source.CalculateLoad();//需要重新设置labels和ticks
        }

        #endregion

        #region MainUnit

        /// <summary>
        /// 主刻度，默认20。
        /// </summary>
        public double MainUnit
        {
            get { return (double)GetValue(MainUnitProperty); }
            set { SetValue(MainUnitProperty, value); }
        }

        public static readonly DependencyProperty MainUnitProperty =
            DependencyProperty.Register("MainUnit", typeof(double), typeof(Axis),
            new PropertyMetadata((double)20, OnMainUnitChanged));

        private static void OnMainUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            if (source.IsSubUnitAuto)
                source.CalculateSubUnit();

            if (source._isValueCalculating || source.DataType == DataType.Text)
                return;

            if ((double)e.NewValue <= 0 ||
                (source.DataType == DataType.Numberic && source.IsLogarithm && (double)e.NewValue < 2))
            {
                source._ignoreChange = true;
                source.MainUnit = (double)e.OldValue;
                return;
            }
            source.CalculateLoad();//需要重新设置labels和ticks     
        }

        #endregion

        #region IsSubUnitAuto

        /// <summary>
        /// 次刻度自动，默认true。
        /// </summary>
        public bool IsSubUnitAuto
        {
            get { return (bool)GetValue(IsSubUnitAutoProperty); }
            set { SetValue(IsSubUnitAutoProperty, value); }
        }

        public static readonly DependencyProperty IsSubUnitAutoProperty =
            DependencyProperty.Register("IsSubUnitAuto", typeof(bool), typeof(Axis),
            new PropertyMetadata(true, OnIsSubUnitAutoChanged));

        private static void OnIsSubUnitAutoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source.DataType != DataType.Text &&
                (bool)e.NewValue)
            {
                source.CalculateSubUnit();
            }
        }

        #endregion

        #region SubUnit

        /// <summary>
        /// 次刻度，默认5。
        /// </summary>
        public double SubUnit
        {
            get { return (double)GetValue(SubUnitProperty); }
            set { SetValue(SubUnitProperty, value); }
        }

        public static readonly DependencyProperty SubUnitProperty =
            DependencyProperty.Register("SubUnit", typeof(double), typeof(Axis),
            new PropertyMetadata((double)5, OnSubUnitChanged));

        private static void OnSubUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }

            if (source._isValueCalculating || source.DataType == DataType.Text)
                return;

            if ((double)e.NewValue <= 0)
            {
                source._ignoreChange = true;
                source.SubUnit = (double)e.OldValue;
                return;
            }

            source.LoadSubTicks();
        }

        #endregion

        #region IsLogarithm

        /// <summary>
        /// 对数刻度？
        /// </summary>
        public bool IsLogarithm
        {
            get { return (bool)GetValue(IsLogarithmProperty); }
            set { SetValue(IsLogarithmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLog.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLogarithmProperty =
            DependencyProperty.Register("IsLogarithm", typeof(bool), typeof(Axis),
            new PropertyMetadata(false, OnIsLogarithmChanged));

        private static void OnIsLogarithmChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source.DataType == DataType.Numberic)
            {
                source.LoadValueCalculator();
                if ((bool)e.NewValue)
                {
                    if (source.MainUnit <= 2)
                        source.MainUnit = ValueCalculateAlgorithm.GetLogUnit(source.MinData, source.MaxData);

                    if (source.MinValue <= 0)
                    {
                        if (source.MinData <= 0)
                            source.MinData = source.MainUnit / 10;

                        source.MinValue = ValueCalculateAlgorithm.GetLogMin(source.MainUnit, source.MinData);
                    }

                    if (source.MaxValue <= source.MinValue)
                        source.MinValue = ValueCalculateAlgorithm.GetLogMax(source.MainUnit, source.MaxData);

                    if (source.SubUnit < source.MainUnit)
                        source.CalculateSubUnit();
                }
                source.CalculateLoad();
            }
        }

        #endregion

        #region DataType

        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(Axis),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var axis = d as Axis;
            axis.OnDataTypeChanged((DataType)e.OldValue, (DataType)e.NewValue);
        }

        private void OnDataTypeChanged(DataType oldValue, DataType newValue)
        {
            if (_dataLinks != null && _dataLinks.Count > 0)
                throw new ArgumentException("以存在关联数据，不可再修改数据线类型");

            if (newValue == DataType.DateTime)
            {
                this.Format = "yy-MM-dd";
            }
            else
            {
                this.Format = null;
            }

            this.LoadHelpers();
            this.LoadFormatSetterContent();
            this.LoadTicSetterContent();
            this.CalculateData();
        }

        #endregion DataType

        #region ContentPresenter

        #region PositionPresenter

        /// <summary>
        /// 用于计算坐标位置。
        /// </summary>
        internal ContentPresenter PositionPresenter
        {
            get { return (ContentPresenter)GetValue(PositionPresenterProperty); }
            private set { SetValue(PositionPresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositionPresenter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty PositionPresenterProperty =
            DependencyProperty.Register("PositionPresenter", typeof(ContentPresenter), typeof(Axis), null);

        #endregion PositionPresenter

        #region TopLabelsPresenter

        internal ContentPresenter TopLabelsPresenter
        {
            get { return (ContentPresenter)GetValue(TopLabelsPresenterProperty); }
            private set { SetValue(TopLabelsPresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopLabelsPresenter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TopLabelsPresenterProperty =
            DependencyProperty.Register("TopLabelsPresenter", typeof(ContentPresenter), typeof(Axis), null);

        #endregion TopLabelsPresenter

        #region BottomLabelsPresenter

        internal ContentPresenter BottomLabelsPresenter
        {
            get { return (ContentPresenter)GetValue(BottomLabelsPresenterProperty); }
            private set { SetValue(BottomLabelsPresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomLabelsPresenter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BottomLabelsPresenterProperty =
            DependencyProperty.Register("BottomLabelsPresenter", typeof(ContentPresenter), typeof(Axis), null);

        #endregion BottomLabelsPresenter

        #region TopTitlePresenter

        internal ContentPresenter TopTitlePresenter
        {
            get { return (ContentPresenter)GetValue(TopTitlePresenterProperty); }
            private set { SetValue(TopTitlePresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopTitlePresenter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TopTitlePresenterProperty =
            DependencyProperty.Register("TopTitlePresenter", typeof(ContentPresenter), typeof(Axis), null);

        #endregion TopTitlePresenter

        #region BottomTitlePresenter

        internal ContentPresenter BottomTitlePresenter
        {
            get { return (ContentPresenter)GetValue(BottomTitlePresenterProperty); }
            private set { SetValue(BottomTitlePresenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomTitlePresenter.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BottomTitlePresenterProperty =
            DependencyProperty.Register("BottomTitlePresenter", typeof(ContentPresenter), typeof(Axis), null);

        #endregion BottomTitlePresenter

        #endregion ContentPresenter


        public bool IsFixPosition
        {
            get { return (bool)GetValue(IsFixPositionProperty); }
            set { SetValue(IsFixPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFixPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFixPositionProperty =
            DependencyProperty.Register("IsFixPosition", typeof(bool), typeof(Axis),
            new PropertyMetadata(true, OnIsFixPositionChanged));

        private static void OnIsFixPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.FixPosition();
        }

        public Axis BindingAxis
        {
            get { return (Axis)GetValue(BindingAxisProperty); }
            set { SetValue(BindingAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindingAxis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingAxisProperty =
            DependencyProperty.Register("BindingAxis", typeof(Axis), typeof(Axis),
            new PropertyMetadata(OnBindingAxisChanged));

        private static void OnBindingAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            var oldValue = (Axis)e.OldValue;
            if (oldValue != null &&
                oldValue.ExtendAxes.Contains(source) == true)
            {
                oldValue.ExtendAxes.Remove(source);
            }

            var newValue = (Axis)e.NewValue;
            if (newValue != null &&
                newValue.ExtendAxes.Contains(source) == false)
            {
                newValue.ExtendAxes.Add(source);
            }

            source.FixPosition();
        }

        public double NumericPosition
        {
            get { return (double)GetValue(NumericPositionProperty); }
            set { SetValue(NumericPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumericPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumericPositionProperty =
            DependencyProperty.Register("NumericPosition", typeof(double), typeof(Axis),
            new PropertyMetadata(0.0, OnNumericPositionChanged));

        private static void OnNumericPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source.BindingAxis != null &&
                source.BindingAxis.DataType != DataType.Text)
            {
                source.FixPosition();
            }
        }

        public string TextPosition
        {
            get { return (string)GetValue(TextPositionProperty); }
            set { SetValue(TextPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPositionProperty =
            DependencyProperty.Register("TextPosition", typeof(string), typeof(Axis),
            new PropertyMetadata("绑定的文本", OnTextPositionChanged));

        private static void OnTextPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            if (source.BindingAxis != null &&
                source.BindingAxis.DataType == DataType.Text)
            {
                source.FixPosition();
            }
        }

        public Axis AlarmSubAxis
        {
            get { return (Axis)GetValue(AlarmSubAxisProperty); }
            set { SetValue(AlarmSubAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlarmSubAxis.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlarmSubAxisProperty =
            DependencyProperty.Register("AlarmSubAxis", typeof(Axis), typeof(Axis),
            new PropertyMetadata(OnAlarmSubAxisChanged));

        private static void OnAlarmSubAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Axis;
            source.OnAlarmSubAxisChanged((Axis)e.OldValue, (Axis)e.NewValue);
        }

        protected virtual void OnAlarmSubAxisChanged(Axis oldValue, Axis newValue)
        {
        }

        #endregion dp
    }
}
