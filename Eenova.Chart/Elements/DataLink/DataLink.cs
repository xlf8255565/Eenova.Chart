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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Eenova.Chart.Controls;
using Eenova.Chart.Factories;
using Eenova.Chart.Helpers;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public class DataLink : UIControl
    {
        #region field

        Panel _linePanel = new Canvas();
        Panel _markPanel = new Canvas();
        Panel _markEffect = new Canvas();
        Polyline _dataLine = new Polyline();//数据线。
        Effect _lineEffect;

        PlotArea _parentPlotArea;
        Axis _axisX;
        Axis _axisY;

        DataValidator _xDataValidator;//验证X轴数据。
        DataValidator _yDataValidator;//验证Y轴数据。

        IList<Mark> _markCache = new List<Mark>();
        IList<UIElement> _effectCache = new List<UIElement>();

        #endregion field

        #region property

        protected override string SettingTitle
        {
            get { return "设置数据线格式"; }
        }

        public DataPointCollection DataPoints { get; private set; }

        public PlotArea ParentPlotArea
        {
            get { return _parentPlotArea; }
            set
            {
                if (value == null)
                {
                    this.AxisX = null;
                    this.AxisY = null;
                }
                else
                {
                    this.AxisX = value.AxisX;
                    this.AxisY = value.FindAxisY(this.LinkedY);
                }
                _parentPlotArea = value;
            }
        }

        private Axis AxisX
        {
            get { return _axisX; }
            set
            {
                if (_axisX != value)
                {
                    if (_axisX != null)
                    {
                        _axisX.Unregister(this);
                    }
                    if (value != null)
                    {
                        value.Register(this);
                    }
                    _axisX = value;
                }
            }
        }

        internal Axis AxisY
        {
            get { return _axisY; }
            private set
            {
                if (_axisY != value)
                {
                    if (_axisY != null)
                    {
                        _axisY.Unregister(this);
                    }
                    if (value != null)
                    {
                        value.Register(this);
                    }
                    _axisY = value;
                }
            }
        }

        public double Deep { get; set; }

        #endregion property

        #region constructor

        public DataLink()
        {
            this.DefaultStyleKey = typeof(DataLink);
            this.Init();
        }

        private void Init()
        {
            this.RenderTransform = new CompositeTransform();
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            this.LinkHost = new Canvas();
            var effect = new Canvas() { Visibility = Visibility.Collapsed };
            effect.Children.Add(_markEffect);
            base.SelectedEffect = effect;

            _dataLine.SetBinding(Polyline.StrokeProperty, new Binding("Stroke") { Source = this });
            _dataLine.SetBinding(Polyline.StrokeDashArrayProperty, new Binding("StrokeStyle") { Source = this });
            _dataLine.SetBinding(Polyline.VisibilityProperty, new Binding("StrokeVisibility") { Source = this });
            _dataLine.SetBinding(Polyline.StrokeThicknessProperty, new Binding("StrokeThickness") { Source = this });
            _dataLine.SetBinding(ToolTipService.ToolTipProperty, new Binding("Text") { Source = this });

            _lineEffect = new DropShadowEffect { ShadowDepth = 2 };
            this.Effect = this.ShadowVisibility == Visibility.Visible ? _lineEffect : null;

            _linePanel.Children.Add(_dataLine);
            this.LinkHost.Children.Add(_linePanel);
            this.LinkHost.Children.Add(_markPanel);
            this.LinkHost.Children.Add(base.SelectedEffect);

            _xDataValidator = DataValidatorFactory.Create(this.XDataType);
            _yDataValidator = DataValidatorFactory.Create(this.YDataType);
            this.DataPoints = new DataPointCollection();
            this.DataPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(DataPointsChanged);
            this.SizeChanged += (s, e) => this.SetClip(e.NewSize);
            this.SetLineTransform();
        }

        #endregion

        #region internal method

        internal void Load()
        {
            this.LoadPoints();
            this.SetTransform();
        }

        #endregion

        #region protected method

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                this.OnToDelete();
                return;
            }
            base.OnKeyDown(e);
        }

        protected override void LoadMenu()
        {
            base.LoadMenu();

            var item = new ImproveMenuItem() { Header = "删除" };
            item.Click += (s, e) => this.OnToDelete();
            ContextMenu.Items.Insert(0, item);
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("基本信息", new LinkInfoSetter() { DataContext = this });
            SettingWindow.Add("图案", new LinkStrokeSetter() { DataContext = this });
            SettingWindow.Add("标记", new LinkMarkSetter() { DataContext = this });
        }

        #endregion protected method

        #region private method

        private void LoadPoints()
        {
            if (this.AxisX == null || this.AxisY == null)
            {
                this.ClearChilren();
                return;
            }

            DataPoints.OrderPoints(this.OrderType);
            var xValues = this.AxisX.Convert(DataPoints.OrderedXValues);
            var yValues = this.AxisY.Convert(DataPoints.OrderedYValues);

            if (xValues == null || yValues == null)
            {
                this.ClearChilren();
                return;
            }

            var valuePoint = new Point(); ;
            int pointIndex = 0, markIndex = 0;
            for (int i = 0; i < xValues.Count; i++)
            {
                valuePoint.X = xValues[i];
                valuePoint.Y = yValues[i];
                if (AddLinePoints(valuePoint, pointIndex))
                {
                    pointIndex++;
                    if (AddMarkPoints(valuePoint, markIndex, i))
                        markIndex++;
                }
            }

            this.RemoveSurplusLinePoints(pointIndex);
            this.RemoveSurplusMarks(markIndex);
        }

        private void ClearChilren()
        {
            _dataLine.Points.Clear();
            _markPanel.Children.Clear();
            _markEffect.Children.Clear();
        }

        private bool AddLinePoints(Point point, int j)
        {
            if (double.IsNaN(point.X) || double.IsNaN(point.Y))
                return false;

            while (j >= _dataLine.Points.Count)
            {
                _dataLine.Points.Add(new Point());
            }
            _dataLine.Points[j] = point;
            return true;
        }

        private bool AddMarkPoints(Point point, int markIndex, int valueIndex)
        {
            if (point.X < 0 || point.X > AxisX.Length ||
                point.Y < 0 || point.Y > AxisY.Length)
                return false;

            while (markIndex >= _markPanel.Children.Count)
            {
                _markPanel.Children.Add(this.CreateMark(_markPanel.Children.Count));
                _markEffect.Children.Add(this.CreateEffect(_markEffect.Children.Count));
            }
            this.SetMarkSize(_markPanel.Children[markIndex], point.X, point.Y, valueIndex);
            this.SetEffectSize(_markEffect.Children[markIndex], point.X, point.Y, valueIndex);

            return true;
        }

        private Mark CreateMark(int index)
        {
            while (index >= _markCache.Count)
            {
                var mark = MarkFactory.Create(this);
                mark.SetBinding(Mark.IsXDescProperty, new Binding("IsDesc") { Source = this.AxisX });
                mark.SetBinding(Mark.IsYDescProperty, new Binding("IsDesc") { Source = this.AxisY });
                Canvas.SetZIndex(mark, 99);
                _markCache.Add(mark);
            }
            return _markCache[index];
        }

        private UIElement CreateEffect(int index)
        {
            while (index >= _effectCache.Count)
            {
                var effect = new EffectCircle();
                effect.SetBinding(EffectCircle.HeightProperty, new Binding("MarkSize") { Source = this });
                effect.SetBinding(EffectCircle.WidthProperty, new Binding("MarkSize") { Source = this });
                Canvas.SetZIndex(effect, 100);
                _effectCache.Add(effect);
            }
            return _effectCache[index];
        }

        private void SetMarkSize(UIElement mark, double x, double y, int index)
        {
            Canvas.SetLeft(mark, Math.Round(x - this.MarkSize / 2));
            Canvas.SetTop(mark, Math.Round(y - this.MarkSize / 2));
            ToolTipService.SetToolTip(mark, this.DataPoints.OrderedPoints[index]);
            mark.SetValue(FrameworkElement.TagProperty, index);
        }

        private void SetEffectSize(UIElement effect, double x, double y, int index)
        {
            Canvas.SetLeft(effect, Math.Round(x - this.MarkSize / 2));
            Canvas.SetTop(effect, Math.Round(y - this.MarkSize / 2));
            effect.SetValue(FrameworkElement.TagProperty, index);
        }

        private void RemoveSurplusLinePoints(int pointIndex)
        {
            while (pointIndex < _dataLine.Points.Count)
            {
                _dataLine.Points.RemoveAt(_dataLine.Points.Count - 1);
            }
        }

        private void RemoveSurplusMarks(int markIndex)
        {
            while (markIndex < _markPanel.Children.Count)
            {
                _markPanel.Children.RemoveAt(_markPanel.Children.Count - 1);
                _markEffect.Children.RemoveAt(_markEffect.Children.Count - 1);
            }
        }

        private void SetLineTransform()
        {
            _dataLine.RenderTransform = this.StrokeThickness % 2 == 0 ? null : new TranslateTransform() { X = 0.5, Y = 0.5 };
        }

        private void SetTransform()
        {
            if (AxisX == null || AxisY == null)
                return;
            ((CompositeTransform)this.RenderTransform).ScaleX = AxisX.IsDesc ? -1 : 1;
            ((CompositeTransform)this.RenderTransform).ScaleY = AxisY.IsDesc ? 1 : -1;
        }

        private void SetClip(Size size)
        {
            _linePanel.Clip = new RectangleGeometry
            {
                Rect = new Rect(-this.StrokeThickness, -1 - this.StrokeThickness, size.Width + 2 + this.StrokeThickness, size.Height + this.StrokeThickness + 2)
            };
        }

        #endregion private method

        #region events

        private void DataPointsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.AddItems(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems == null)
                    return;

                foreach (DataPoint point in e.OldItems)
                {
                    point.PropertyChanged -= new PropertyChangedEventHandler(DataPointPropertyChanged);
                }
            }

            this.OnPointsChanged();
            this.Load();
        }

        private void AddItems(IList items)
        {
            if (items == null)
                return;

            foreach (DataPoint point in items)
            {
                if (!this.ValidateData(point))
                    this.Remove(point);
                else
                    point.PropertyChanged += new PropertyChangedEventHandler(DataPointPropertyChanged);
            }
        }

        private bool ValidateData(DataPoint point)
        {
            if (point == null)
                return false;

            bool result = true;
            var x = _xDataValidator.Validate(point.XValue);
            if (x == null)
                result = false;
            else
                point.XValue = x;

            var y = _yDataValidator.Validate(point.YValue);
            if (y == null)
                result = false;
            else
                point.YValue = y;

            if (!result)
                MessageBox.Show(string.Format("添加的数据({0},{1})与已有的数据类型不符", point.XValue, point.YValue));

            return result;
        }

        private void Remove(DataPoint point)
        {
            if (point == null)
                return;
            this.Dispatcher.BeginInvoke(() => this.DataPoints.Remove(point));
        }

        private void DataPointPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var point = sender as DataPoint;
            if (point == null)
                return;

            if (this.ValidateData(point))
            {
                this.OnPointsChanged();
                this.Load();
            }
            else
                this.Remove(point);
        }

        internal event EventHandler PointsChanged;
        internal event EventHandler ToDelete;
        internal event EventHandler LinkedYChanged;

        private void OnPointsChanged()
        {
            var handle = this.PointsChanged;
            if (handle != null)
            {
                handle.Invoke(this, new EventArgs());
            }
        }

        private void OnToDelete()
        {
            var handle = ToDelete;
            if (handle != null)
            {
                handle.Invoke(this, new EventArgs());
            }
        }

        private void OnLinkedYChanged()
        {
            var handle = LinkedYChanged;
            if (handle != null)
            {
                handle.Invoke(this, new EventArgs());
            }
        }

        #endregion events

        #region dp

        #region LinkHost

        internal Panel LinkHost
        {
            get { return (Panel)GetValue(LinkHostProperty); }
            private set { SetValue(LinkHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty LinkHostProperty =
            DependencyProperty.Register("LinkHost", typeof(Panel), typeof(DataLink), null);

        #endregion

        #region MarkVisibility

        /// <summary>
        /// 标记是否可见，默认。
        /// </summary>
        public Visibility MarkVisibility
        {
            get { return (Visibility)GetValue(MarkVisibilityProperty); }
            set { SetValue(MarkVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkVisibilityProperty =
            DependencyProperty.Register("MarkVisibility", typeof(Visibility), typeof(DataLink),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region MarkType

        /// <summary>
        /// 标记类型，默认ShapeType.Circle。
        /// </summary>
        public ShapeType MarkType
        {
            get { return (ShapeType)GetValue(MarkTypeProperty); }
            set { SetValue(MarkTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkTypeProperty =
            DependencyProperty.Register("MarkType", typeof(ShapeType), typeof(DataLink),
            new PropertyMetadata(ShapeType.Circle));

        #endregion

        #region MarkSize

        /// <summary>
        /// 标记大小，默认4。
        /// </summary>
        public double MarkSize
        {
            get { return (double)GetValue(MarkSizeProperty); }
            set { SetValue(MarkSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSizeProperty =
            DependencyProperty.Register("MarkSize", typeof(double), typeof(DataLink),
            new PropertyMetadata((double)4, OnMarkSizeChanged));

        private static void OnMarkSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            source.Load();
        }

        #endregion

        #region MarkBrush

        /// <summary>
        /// 标记颜色，默认Color.FromArgb(255, 30, 80, 130)。
        /// </summary>
        public Brush MarkBrush
        {
            get { return (Brush)GetValue(MarkBrushProperty); }
            set { SetValue(MarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushProperty =
            DependencyProperty.Register("MarkBrush", typeof(Brush), typeof(DataLink),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 30, 80, 130))));

        #endregion

        #region StrokeVisibility

        /// <summary>
        /// 数据线是否可见，默认Visible。
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(DataLink),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region StrokeStyle

        /// <summary>
        /// 线条样式，默认StrokeStyles.Solid。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(DataLink),
            new PropertyMetadata(StrokeStyles.Solid));


        #endregion

        #region StrokeThickness

        /// <summary>
        /// 线条厚度，默认1。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(DataLink),
            new PropertyMetadata((double)1, OnStrokeThicknessChanged));

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            //改变线宽的时候不会改变。重新设置下线形就会改变了。先用这种方法，原因再去排查。
            var style = source.StrokeStyle;
            source.StrokeStyle = StrokeStyles.Rush;
            source.StrokeStyle = style;
            source.SetLineTransform();
        }


        #endregion

        #region Stroke

        /// <summary>
        /// 线条颜色，默认Black。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(DataLink),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #region ShadowVisibility

        /// <summary>
        /// 数据线是否有阴影，默认Collapsed.
        /// </summary>
        public Visibility ShadowVisibility
        {
            get { return (Visibility)GetValue(ShadowVisibilityProperty); }
            set { SetValue(ShadowVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowVisibilityProperty =
            DependencyProperty.Register("ShadowVisibility", typeof(Visibility), typeof(DataLink),
            new PropertyMetadata(Visibility.Collapsed, OnShadowVisibilityChanged));

        private static void OnShadowVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            source.Effect = (Visibility)e.NewValue == Visibility.Visible ? source._lineEffect : null;
        }

        #endregion

        #region LinkedY

        /// <summary>
        /// 关联的Y轴类型，默认Y1。
        /// </summary>
        public PlotY LinkedY
        {
            get { return (PlotY)GetValue(LinkedYProperty); }
            set { SetValue(LinkedYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkedY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkedYProperty =
            DependencyProperty.Register("LinkedY", typeof(PlotY), typeof(DataLink),
            new PropertyMetadata(PlotY.Y1, OnLinkedYChanged));

        private static void OnLinkedYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            source.OnLinkedYChanged();
        }

        #endregion

        #region Text

        /// <summary>
        /// 数据线的名字，默认"数据线"。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DataLink),
            new PropertyMetadata("数据线"));

        #endregion

        #region XDataType

        /// <summary>
        /// X轴数据类型，默认DataType.Numberic。
        /// </summary>
        public DataType XDataType
        {
            get { return (DataType)GetValue(XDataTypeProperty); }
            set { SetValue(XDataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XDataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XDataTypeProperty =
            DependencyProperty.Register("XDataType", typeof(DataType), typeof(DataLink),
            new PropertyMetadata(DataType.Numberic, OnXDataTypeChanged));

        private static void OnXDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            if (source.DataPoints.Count > 0)
                throw new ArgumentException("已存在数据，切换数据线类型请先清空数据。");
            if (source.AxisX != null)
                throw new ArgumentException("已存在关联X轴，切换数据线类型请先删除关联坐标轴。");
            source._xDataValidator = DataValidatorFactory.Create((DataType)e.NewValue);
        }

        #endregion

        #region YDataType

        /// <summary>
        /// Y轴数据类型，默认DataType.Numberic。
        /// </summary>
        public DataType YDataType
        {
            get { return (DataType)GetValue(YDataTypeProperty); }
            set { SetValue(YDataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YDataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YDataTypeProperty =
            DependencyProperty.Register("YDataType", typeof(DataType), typeof(DataLink),
            new PropertyMetadata(DataType.Numberic, OnYDataTypeChanged));

        private static void OnYDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            if (source.DataPoints.Count > 0)
                throw new ArgumentException("已存在数据，切换数据线类型请先清空数据。");
            if (source.AxisY != null)
                throw new ArgumentException("已存在关联Y轴，切换数据线类型请先清空数据。");
            source._yDataValidator = DataValidatorFactory.Create((DataType)e.NewValue);
        }

        #endregion

        #region OrderType

        public OrderType OrderType
        {
            get { return (OrderType)GetValue(OrderTypeProperty); }
            set { SetValue(OrderTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderTypeProperty =
            DependencyProperty.Register("OrderType", typeof(OrderType), typeof(DataLink),
            new PropertyMetadata(OrderType.Default, OnOrderTypeChanged));

        private static void OnOrderTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataLink;
            source.Load();
        }

        #endregion

        #endregion
    }
}
