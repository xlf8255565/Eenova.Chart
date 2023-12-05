using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Eenova.Chart.Controls;
using Eenova.Chart.Factories;
using Eenova.Chart.Helpers;
using Eenova.Chart.Interfaces;
using Eenova.Chart.Setter;
using Eenova.Chart.Common;

namespace Eenova.Chart.Elements
{
    public class DataLink : Canvas, IDataLink
    {
        internal event Action PointsChanged;

        #region field

        bool _isSelectionControlLoaded;
        GridControl _selectionControl;
        Canvas _lineCanvas;
        Canvas _pointsCanvas;
        Canvas _effectCanvas;
        Polyline _line;//数据线。
        CompositeTransform _transform;//翻转，为了适应逆序。

        DataValidator _xDataValidator;//验证X轴数据。
        DataValidator _yDataValidator;//验证Y轴数据。
        DataType _xDataType;//X轴的数据类型。
        DataType _yDataType;//Y轴的数据类型。

        Axis _linkAxisX;
        Axis _linkAxisY;
        DataLinkCollection _parent;
        public PlotArea _parentArea;


        #endregion field


        #region property

        public DataPointCollection DataPoints { get; private set; }

        #endregion property


        public DataLink()
        {
            this.LoadLine();

            this.SetSelectionControl();

            _transform = new CompositeTransform();
            this.RenderTransform = _transform;
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            _xDataValidator = DataValidatorFactory.Create(this.XDataType);
            _yDataValidator = DataValidatorFactory.Create(this.YDataType);

            this.DataPoints = new DataPointCollection();

            this.Loaded += new RoutedEventHandler(DataLink_Loaded);
        }

        void DataLink_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(DataPoints_CollectionChanged);
            this.SizeChanged += new SizeChangedEventHandler(DataLink_SizeChanged);
        }

        void DataLink_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SetClip(e.NewSize);
        }


        #region public method

        /// <summary>
        /// 加载数据线。
        /// </summary>
        public void Load()
        {
            this.LoadPoints();
            this.SetXTransform();
            this.SetYTransform();
        }

        #endregion public method


        #region internal method

        internal void SetLinkAxisX(Axis axisX)
        {
            if (_linkAxisX != null)
            {
                _linkAxisX.UnregisterData(this);
            }

            if (axisX != null)
            {
                axisX.RegisterData(this);
                _linkAxisX = axisX;
            }
        }

        internal void SetLinkAxisY(Axis axisY)
        {
            if (_linkAxisY != null)
            {
                _linkAxisY.UnregisterData(this);
            }

            if (axisY != null)
            {
                axisY.RegisterData(this);
                _linkAxisY = axisY;
            }
        }

        internal void SetParent(DataLinkCollection parent)
        {
            _parent = parent;
        }

        internal void Delete()
        {
            if (_linkAxisX != null)
                _linkAxisX.UnregisterData(this);

            if (_linkAxisY != null)
                _linkAxisY.UnregisterData(this);

            if (_parent != null)
                _parent.Remove(this);

            ElementOperator.DeleteFromPanelParent(this);
        }

        #endregion


        #region private method

        private void SetSelectionControl()
        {
            _lineCanvas = new Canvas();
            _lineCanvas.Children.Add(_line);
            _pointsCanvas = new Canvas();
            _effectCanvas = new Canvas() { Visibility = Visibility.Collapsed };
            _selectionControl = new GridControl();
            this.Children.Add(_selectionControl);

            _selectionControl.Loaded += new RoutedEventHandler(SelectionControl_Loaded);
        }

        void SelectionControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isSelectionControlLoaded)
            {
                _selectionControl.Add(_lineCanvas);
                _selectionControl.Add(_pointsCanvas);
                _selectionControl.Add(_effectCanvas);
                _selectionControl.SelectedEffectElement = _effectCanvas;

                _selectionControl.SettingWindow.Title = "设置数据线格式";
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "名称", Content = new TextSetter(this) });
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "图案", Content = new ShadowStrokeSetter(this) });
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "标记", Content = new MarkSetter(this) });
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "坐标系", Content = new LinkSetter(this) });

                var item = new MenuItem() { Header = "删除" };
                item.Click += (s1, e1) => this.Delete();
                _selectionControl.ContextMenu.Items.Insert(0, item);

                _isSelectionControlLoaded = true;
            }
        }

        /// <summary>
        /// 绑定数据线。
        /// </summary>
        private void BindingLine()
        {
            var b = new Binding("StrokeStyle") { Source = this };
            _line.SetBinding(Polyline.StrokeDashArrayProperty, b);

            b = new Binding("StrokeThickness") { Source = this, Mode = BindingMode.TwoWay };
            _line.SetBinding(Polyline.StrokeThicknessProperty, b);

            b = new Binding("Stroke") { Source = this };
            _line.SetBinding(Polyline.StrokeProperty, b);

            b = new Binding("StrokeVisibility") { Source = this };
            _line.SetBinding(Polyline.VisibilityProperty, b);
        }

        /// <summary>
        /// 初始化数据线的提示信息。
        /// </summary>
        private void SetLineTip()
        {
            var tip = new ToolTip();

            var b = new Binding("Text");
            b.Source = this;
            tip.SetBinding(ToolTip.ContentProperty, b);

            ToolTipService.SetToolTip(_line, tip);
        }

        /// <summary>
        /// 给数据线添加效果。
        /// </summary>
        /// <param name="line"></param>
        private void RenderLineEffect()
        {
            if (this.ShadowVisibility == Visibility.Visible)
            {
                var dse = new DropShadowEffect();
                dse.BlurRadius = 5;
                dse.Opacity = 1;
                dse.ShadowDepth = 2;
                dse.Color = Colors.Black;
                _line.Effect = dse;
            }
            else
            {
                _line.Effect = null;
            }
        }

        /// <summary>
        /// 初始化数据线。
        /// </summary>
        private void LoadLine()
        {
            _line = new Polyline();

            this.BindingLine();
            this.SetLineTip();
            this.RenderLineEffect();
        }

        /// <summary>
        /// 加载数据。
        /// </summary>
        private void LoadPoints()
        {
            _line.Points.Clear();
            _pointsCanvas.Children.Clear();
            _effectCanvas.Children.Clear();

            if (_linkAxisX == null || _linkAxisX.CoordConverter == null)
                return;

            if (_linkAxisY == null || _linkAxisY.CoordConverter == null)
                return;

            var xValues = _linkAxisX.CoordConverter.Convert(DataPoints.XValues);
            var yValues = _linkAxisY.CoordConverter.Convert(DataPoints.YValues);

            if (xValues == null || yValues == null)
                return;

            Point p;
            Mark mark;
            EffectCircle circle;
            double x = double.NaN;
            double y = double.NaN;
            double left, top;
            for (int i = 0; i < xValues.Count; i++)
            {
                x = xValues[i];
                if (double.IsNaN(x))
                    continue;

                y = yValues[i];
                if (double.IsNaN(y))
                    continue;

                p = new Point(x, y);

                _line.Points.Add(p);

                if (p.X < 0 || p.X > _linkAxisX.Length ||
                    p.Y < 0 || p.Y > _linkAxisY.Length)
                    continue;

                mark = MarkFactory.Create(this);
                left = p.X - mark.Width / 2;
                top = p.Y - mark.Height / 2;
                Canvas.SetLeft(mark, left);
                Canvas.SetTop(mark, top);
                Canvas.SetZIndex(mark, 99);
                ToolTipService.SetToolTip(mark, string.Format("{0},{1}", DataPoints.XValues[i], DataPoints.YValues[i]));
                _pointsCanvas.Children.Add(mark);

                circle = new EffectCircle() { Height = mark.Height + 4, Width = mark.Width + 4 };
                left = p.X - circle.Width / 2;
                top = p.Y - circle.Height / 2;
                Canvas.SetLeft(circle, left);
                Canvas.SetTop(circle, top);
                Canvas.SetZIndex(circle, 100);
                _effectCanvas.Children.Add(circle);
            }
        }

        /// <summary>
        /// X方向翻转控制。
        /// </summary>
        private void SetXTransform()
        {
            if (_linkAxisX == null)
                return;

            if (_linkAxisX.IsDesc)
            {
                _transform.ScaleX = -1;
            }
            else
            {
                _transform.ScaleX = 1;
            }

            SetYTransform();
        }

        /// <summary>
        /// Y方向翻转控制。
        /// </summary>
        private void SetYTransform()
        {
            if (_linkAxisY == null)
                return;

            if (_linkAxisY.IsDesc)
            {
                _transform.ScaleY = 1;
            }
            else
            {
                _transform.ScaleY = -1;
            }
        }

        /// <summary>
        /// 裁剪超出边缘的部分。
        /// </summary>
        /// <param name="size"></param>
        private void SetClip(Size size)
        {
            _lineCanvas.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), size) };
        }

        /// <summary>
        /// 数据集合发生变化时，验证数据是否符合要求。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (this.ValidateData(e.NewItems) == false)
                {
                    this.BeginInvokeRemove(e.NewItems);
                    return;
                }

            }

            this.OnPointsChanged();
            this.Load();
        }

        private void OnPointsChanged()
        {
            if (PointsChanged != null)
                this.PointsChanged();
        }

        private bool ValidateData(IEnumerable points)
        {
            if (points == null)
                return false;

            bool result = true;
            foreach (DataPoint point in points)
            {
                if (this.ValidateData(point) == false)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool ValidateData(DataPoint point)
        {
            if (point == null)
                return false;

            bool result = true;
            if (_xDataValidator.Validate(point.XValue) == false)
                result = false;

            if (_yDataValidator.Validate(point.YValue) == false)
                result = false;

            if (result == false)
                MessageBox.Show(string.Format("添加的数据({0},{1})与已有的数据类型不符", point.XValue.ToString(), point.YValue.ToString()));

            return result;
        }

        private void BeginInvokeRemove(IEnumerable points)
        {
            if (points == null)
                return;

            foreach (DataPoint point in points)
            {
                this.BeginInvokeRemove(point);
            }
        }

        private void BeginInvokeRemove(DataPoint point)
        {
            if (point == null)
                return;

            this.Dispatcher.BeginInvoke(() => this.DataPoints.Remove(point));
        }


        #endregion private method


        #region dp

        #region Mark
        /// <summary>
        /// 标记是否可见。
        /// </summary>
        public Visibility MarkVisibility
        {
            get { return (Visibility)GetValue(MarkVisibilityProperty); }
            set { SetValue(MarkVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkVisibilityProperty =
            DependencyProperty.Register("MarkVisibility", typeof(Visibility), typeof(DataLink), null);

        /// <summary>
        /// 标记类型。
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

        /// <summary>
        /// 标记大小。
        /// </summary>
        public double MarkSize
        {
            get { return (double)GetValue(MarkSizeProperty); }
            set { SetValue(MarkSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkSizeProperty =
            DependencyProperty.Register("MarkSize", typeof(double), typeof(DataLink),
            new PropertyMetadata((double)6, OnMarkSizeChanged));

        /// <summary>
        /// 标记颜色。
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

        #endregion mark

        #region Stroke


        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(DataLink), null);



        /// <summary>
        /// 线条样式。
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


        /// <summary>
        /// 线条厚度。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(DataLink),
            new PropertyMetadata((double)2, OnStrokeThicknessChanged));


        /// <summary>
        /// 线条颜色。
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



        public Visibility ShadowVisibility
        {
            get { return (Visibility)GetValue(ShadowVisibilityProperty); }
            set { SetValue(ShadowVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowVisibilityProperty =
            DependencyProperty.Register("ShadowVisibility", typeof(Visibility), typeof(DataLink),
            new PropertyMetadata(OnShadowVisibilityChanged));



        #endregion

        #region Axis

        /// <summary>
        /// 预设的Y轴类型。
        /// </summary>
        public PlotY LinkedY
        {
            get { return (PlotY)GetValue(LinkedYProperty); }
            set { SetValue(LinkedYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkedY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkedYProperty =
            DependencyProperty.Register("LinkedY", typeof(PlotY), typeof(DataLink),
            new PropertyMetadata(OnLinkedYChanged));

        /// <summary>
        /// 数据线的名字。
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

        #region Data


        public DataType XDataType
        {
            get { return (DataType)GetValue(XDataTypeProperty); }
            set { SetValue(XDataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XDataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XDataTypeProperty =
            DependencyProperty.Register("XDataType", typeof(DataType), typeof(DataLink),
            new PropertyMetadata(DataType.Numberic, OnXDataTypeChanged));



        public DataType YDataType
        {
            get { return (DataType)GetValue(YDataTypeProperty); }
            set { SetValue(YDataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YDataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YDataTypeProperty =
            DependencyProperty.Register("YDataType", typeof(DataType), typeof(DataLink),
            new PropertyMetadata(DataType.Numberic, OnYDataTypeChanged));


        #endregion

        #endregion dp


        #region OnChanged static method

        private static void OnLinkedYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnLinkedYChanged((PlotY)e.OldValue, (PlotY)e.NewValue);
        }

        private static void OnShadowVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnShadowVisibilityChanged((Visibility)e.OldValue, (Visibility)e.NewValue);
        }

        private static void OnMarkSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnMarkSizeChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnXDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnXDataTypeChanged((DataType)e.OldValue, (DataType)e.NewValue);
        }

        private static void OnYDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnYDataTypeChanged((DataType)e.OldValue, (DataType)e.NewValue);
        }

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as DataLink;
            element.OnStrokeThicknessChanged((double)e.OldValue, (double)e.NewValue);
        }

        #endregion


        #region OnChanged Method

        private void OnLinkedYChanged(PlotY oldValue, PlotY newValue)
        {
            if (_parentArea != null)
            {
                var axisY = _parentArea.GetAxisY(newValue);
                if (axisY.IsDataTypeMatch(this.XDataType))
                {
                    this.SetLinkAxisY(axisY);
                    _parentArea.LoadDataLine(this);
                }
            }
        }

        private void OnShadowVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
            this.RenderLineEffect();
        }

        private void OnStrokeThicknessChanged(double oldValue, double newValue)
        {
            //改变线宽的时候不会改变。重新设置下线形就会改变了。先用这种方法，原因再去排查。
            string style = this.StrokeStyle;
            this.StrokeStyle = "0.1 0";
            this.StrokeStyle = style;
        }

        private void OnMarkSizeChanged(double oldValue, double newValue)
        {
            this.Load();
        }

        private void OnXDataTypeChanged(DataType oldValue, DataType newValue)
        {
            if (this.DataPoints.Count > 0)
                throw new ArgumentException("已存在数据，切换数据线类型请先清空数据。");

            if (_linkAxisY != null)
                throw new ArgumentException("已存在关联Y轴，切换数据线类型请先删除关联坐标轴。");

            _xDataType = newValue;
            _xDataValidator = DataValidatorFactory.Create(newValue);
        }

        private void OnYDataTypeChanged(DataType oldValue, DataType newValue)
        {
            if (this.DataPoints.Count > 0)
                throw new ArgumentException("已存在数据，切换数据线类型请先清空数据。");

            if (_linkAxisY != null)
                throw new ArgumentException("已存在关联Y轴，切换数据线类型请先清空数据。");

            _yDataType = newValue;
            _yDataValidator = DataValidatorFactory.Create(newValue);
        }

        #endregion OnChanged Method
    }
}
