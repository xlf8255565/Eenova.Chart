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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Eenova.Chart.Behaviors;
using Eenova.Chart.Controls;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public class PlotArea : ChartChild
    {
        #region field

        bool _ignoreChanged;
        MouseDragElementBehavior _drag;

        #endregion

        #region proterty

        #region Axis

        /// <summary>
        /// X轴。
        /// </summary>
        public AxisX AxisX { get; private set; }

        /// <summary>
        /// 左上纵轴。
        /// </summary>
        public AxisY AxisY1 { get; private set; }

        /// <summary>
        /// 左下纵轴。
        /// </summary>
        public AxisY AxisY2 { get; private set; }

        /// <summary>
        /// 右上纵轴。
        /// </summary>
        public AxisY AxisY3 { get; private set; }

        /// <summary>
        /// 右下横轴。
        /// </summary>
        public AxisY AxisY4 { get; private set; }

        #endregion

        #region GridLine

        /// <summary>
        /// 上半区X轴网格线。
        /// </summary>
        public GridLine GridLineX1 { get; private set; }

        /// <summary>
        /// 下半区X轴网格线。
        /// </summary>
        public GridLine GridLineX2 { get; private set; }

        /// <summary>
        /// 左上纵轴网格线。
        /// </summary>
        public GridLine GridLineY1 { get; private set; }

        /// <summary>
        /// 左下纵轴网格线。
        /// </summary>
        public GridLine GridLineY2 { get; private set; }

        /// <summary>
        /// 右上纵轴网格线。
        /// </summary>
        public GridLine GridLineY3 { get; private set; }

        /// <summary>
        /// 右下纵轴网格线。
        /// </summary>
        public GridLine GridLineY4 { get; private set; }

        #endregion

        /// <summary>
        /// 数据线集合。
        /// </summary>
        public DataLinkCollection DataLinks { get; private set; }

        /// <summary>
        /// 标注集合。
        /// </summary>
        public AxisNoteCollection Notes { get; private set; }

        protected override string SettingTitle
        {
            get { return "设置绘图区格式"; }
        }

        #endregion property

        #region constructor

        public PlotArea()
        {
            this.DefaultStyleKey = typeof(PlotArea);
            this.Init();
        }

        private void Init()
        {
            this.RenderTransform = new CompositeTransform();
            _drag = new MouseDragElementBehavior() { ConstrainToParentBounds = true };
            _drag.Attach(this);

            this.NotesHost = new Grid();// { Background = new SolidColorBrush(Colors.Orange) { Opacity = 0.5 } };
            this.SelectedEffect = new EffectRect();
            this.DataLinks = new DataLinkCollection(this);
            this.Notes = new AxisNoteCollection(this);

            InitAxisX();
            InitTopAxisY();
            InitBottomAxisY();
            this.AxisX.BindingAxis = this.FindAxisY(this.InternalPlotY);

            InitGridLines();
            InitMarkupHost();

            this.SizeChanged += (s, e) =>
                {
                    this.SetSize();
                    //this.TopMarkUpHost.UpdateLayout();
                };
        }

        private void InitAxisX()
        {
            this.AxisXHost = new Canvas();
            this.AxisX = new AxisX
            {
                FixPoint = AxisFixPoint.StartPoint,
            };
            this.AxisX.Title.Text = "横轴";
            this.AxisX.SetBinding(Axis.LengthProperty, new Binding("Length") { Source = this });
            this.SetBinding(PlotArea.IsFixAxisXPositionProperty, new Binding("IsFixPosition") { Source = this.AxisX });
            this.AxisXHost.Children.Add(this.AxisX);
            this.SetAxisXLocation();
        }

        private void InitTopAxisY()
        {
            this.TopAxisYHost = new Grid();
            this.AxisY1 = new AxisY
            {
                TitleLocation = AxisLocation.TopOrLeft,
                TitleAlignment = AxisAlignment.TopOrLeft,
                LabelLocation = AxisLocation.TopOrLeft,
                MainTicksShow = TicksShow.TopOrLeft,
                SubTicksShow = TicksShow.TopOrLeft,
                BindingAxis = this.AxisX,
                AlarmSubAxis = this.AxisX,
            };
            this.AxisY1.Title.Text = "左上纵轴";
            this.AxisY1.Title.Orientation = Orientation.Vertical;
            this.AxisY1.SetBinding(Axis.LengthProperty, new Binding("TopHeight") { Source = this });
            var axisY1Host = new Canvas { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            axisY1Host.Children.Add(this.AxisY1);
            this.TopAxisYHost.Children.Add(axisY1Host);


            this.AxisY3 = new AxisY
            {
                TitleAlignment = AxisAlignment.TopOrLeft,
                IsRightSide = true,
                BindingAxis = this.AxisX,
                AlarmSubAxis = this.AxisX,
            };
            this.AxisY3.Title.Text = "右上纵轴";
            this.AxisY3.Title.Orientation = Orientation.Vertical;
            this.AxisY3.SetBinding(Axis.LengthProperty, new Binding("TopHeight") { Source = this });
            var axisY3Host = new Canvas { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top };
            axisY3Host.Children.Add(this.AxisY3);
            this.TopAxisYHost.Children.Add(axisY3Host);
        }

        private void InitBottomAxisY()
        {
            this.BottomAxisYHost = new Grid();
            this.AxisY2 = new AxisY()
            {
                IsDesc = true,
                TitleLocation = AxisLocation.TopOrLeft,
                LabelLocation = AxisLocation.TopOrLeft,
                MainTicksShow = TicksShow.TopOrLeft,
                SubTicksShow = TicksShow.TopOrLeft,
                BindingAxis = this.AxisX,
                AlarmSubAxis = this.AxisX,
            };
            this.AxisY2.Title.Text = "左下纵轴";
            this.AxisY2.Title.Orientation = Orientation.Vertical;
            this.AxisY2.SetBinding(Axis.LengthProperty, new Binding("BottomHeight") { Source = this });
            var axisY2Host = new Canvas() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            axisY2Host.Children.Add(this.AxisY2);
            this.BottomAxisYHost.Children.Add(axisY2Host);

            this.AxisY4 = new AxisY()
            {
                IsDesc = true,
                IsRightSide = true,
                BindingAxis = this.AxisX,
                AlarmSubAxis = this.AxisX,
            };
            this.AxisY4.Title.Text = "右下纵轴";
            this.AxisY4.Title.Orientation = Orientation.Vertical;
            this.AxisY4.SetBinding(Axis.LengthProperty, new Binding("BottomHeight") { Source = this });
            var axisY4Host = new Canvas() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top };
            axisY4Host.Children.Add(this.AxisY4);
            this.BottomAxisYHost.Children.Add(axisY4Host);
        }

        private void InitGridLines()
        {
            this.TopLinesHost = new Grid();
            var host = new Grid();
            this.TopLinesHost.Children.Add(host);

            this.GridLineX1 = this.AxisX.GenerateGridLine();
            host.Children.Add(this.GridLineX1);
            this.GridLineY1 = this.AxisY1.GenerateGridLine();
            host.Children.Add(this.GridLineY1);
            this.GridLineY3 = this.AxisY3.GenerateGridLine();
            host.Children.Add(this.GridLineY3);

            this.BottomLinesHost = new Grid();
            host = new Grid();
            this.BottomLinesHost.Children.Add(host);

            this.GridLineX2 = this.AxisX.GenerateGridLine();
            host.Children.Add(this.GridLineX2);
            this.GridLineY2 = this.AxisY2.GenerateGridLine();
            host.Children.Add(this.GridLineY2);
            this.GridLineY4 = this.AxisY4.GenerateGridLine();
            host.Children.Add(this.GridLineY4);
        }

        private void InitMarkupHost()
        {
            this.TopMarkUpHost = new Grid();
            TopMarkUpHost.Children.Add(AxisY1.MarkupArea);
            TopMarkUpHost.Children.Add(AxisY3.MarkupArea);
            TopMarkUpHost.Children.Add(AxisX.TopMarkupArea);

            TopMarkUpHost.Children.Add(AxisY1.AlarmBoard);
            TopMarkUpHost.Children.Add(AxisY3.AlarmBoard);
            TopMarkUpHost.Children.Add(AxisX.TopAlarmBoard);

            this.TopMarkUpHost.Children.Add(this.AxisY1.VerticalNote);
            this.TopMarkUpHost.Children.Add(this.AxisY3.VerticalNote);

            this.BottomMarkupHost = new Grid();
            BottomMarkupHost.Children.Add(AxisY2.MarkupArea);
            BottomMarkupHost.Children.Add(AxisY4.MarkupArea);
            BottomMarkupHost.Children.Add(AxisX.BottomMarkupArea);

            BottomMarkupHost.Children.Add(AxisY2.AlarmBoard);
            BottomMarkupHost.Children.Add(AxisY4.AlarmBoard);
            BottomMarkupHost.Children.Add(AxisX.BottomAlarmBoard);

            this.BottomMarkupHost.Children.Add(this.AxisY2.VerticalNote);
            this.BottomMarkupHost.Children.Add(this.AxisY4.VerticalNote);

            this.TopMarkLinesHost = new Grid();
            TopMarkLinesHost.Children.Add(AxisY1.MarkupLine);
            TopMarkLinesHost.Children.Add(AxisY3.MarkupLine);
            TopMarkLinesHost.Children.Add(AxisX.TopMarkupLine);

            this.BottomMarkLinesHost = new Grid();
            BottomMarkLinesHost.Children.Add(AxisY2.MarkupLine);
            BottomMarkLinesHost.Children.Add(AxisY4.MarkupLine);
            BottomMarkLinesHost.Children.Add(AxisX.BottomMarkupLine);
        }

        #endregion Init

        #region internal method

        internal void SetSize()
        {
            this.AxisY1.SetFix();
            this.AxisY2.SetFix();
            this.AxisY3.SetFix();
            this.AxisY4.SetFix();
            this.AxisX.SetFix();

            var left = this.GetLeftWidth();
            var right = this.GetRightWidth();
            var top = this.GetTopHeight();
            var bottom = this.GetBottomHeight();
            this.Width = left + right + this.Length;
            this.Height = top + bottom + this.VisualHeight();
            this.Padding = new Thickness(left, top, 0, 0);

            this.AxisY1.SetFix();
            this.AxisY2.SetFix();
            this.AxisY3.SetFix();
            this.AxisY4.SetFix();
            this.AxisX.SetFix();
        }

        #endregion

        #region protected method

        protected override void LoadMenu()
        {
            base.LoadMenu();

            var item = new ImproveMenuItem() { Header = "添加注释" };
            item.Click += (s, e) => AxisNoteAddWindow.Show(this);
            ContextMenu.Items.Insert(0, item);


            //item = new ImproveMenuItem() { Header = "缩小" };
            //item.Click += (s, e) => this.Length /= 2;
            //ContextMenu.Items.Insert(0, item);

            //item = new ImproveMenuItem() { Header = "放大" };
            //item.Click += (s, e) => this.Length *= 2;
            //ContextMenu.Items.Insert(0, item);

#if DEBUG
            item = new ImproveMenuItem() { Header = "删除" };
            item.Click += (s, e) => this.OnToDelete();
            ContextMenu.Items.Insert(0, item);
#endif
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("显示", new PlotAreaShowSetter { DataContext = this });
            SettingWindow.Add("位置", new PlotAreaPositionSetter { DataContext = this });
            SettingWindow.Add("图案", new CommonBorderSetter { DataContext = this });

            BaseSetter setter = new AxisXPositionSetter { DataContext = this };
            setter.SetBinding(AxisXPositionSetter.DataTypeProperty, new Binding("InternalDataType") { Source = this });
            SettingWindow.Add("横轴位置", setter);

            setter = new AxisYPositionSetter { DataContext = this };
            setter.SetBinding(AxisYPositionSetter.DataTypeProperty, new Binding("DataType") { Source = this.AxisX });
            SettingWindow.Add("纵轴位置", setter);
        }

        #endregion

        #region private method

        private void SetAxisXLocation()
        {
            if (this.IsFixAxisXPosition == false)
                return;

            if (this.AxisXHost.Equals(this.AxisX.Parent) == false)
            {
                var panel = this.AxisX.Parent as Panel;
                if (panel != null)
                {
                    panel.Children.Remove(this.AxisX);
                }
                this.AxisXHost.Children.Add(this.AxisX);
            }
            double top = 0;
            switch (this.XAlignment)
            {
                case VerticalAlignment.Bottom:
                    top = this.VisualHeight();
                    break;
                case VerticalAlignment.Center:
                    if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Visible)
                        top = this.TopHeight;
                    else if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Collapsed)
                        top = this.TopHeight / 2;
                    else if (this.TopVisibility == Visibility.Collapsed && this.BottomVisibility == Visibility.Visible)
                        top = this.BottomHeight / 2;
                    break;
            }
            this.AxisX.Margin = new Thickness(0, Math.Round(top), 0, 0);
        }

        private void SetAxisXParent()
        {
            if (this.IsFixAxisXPosition)
            {
                if (this.AxisXHost.Equals(this.AxisX.Parent) == false)
                {
                    var panel = this.AxisX.Parent as Panel;
                    if (panel != null)
                    {
                        panel.Children.Remove(this.AxisX);
                    }
                    this.AxisXHost.Children.Add(this.AxisX);
                }
            }
            else
            {
                if (this.InternalPlotY == PlotY.Y1 || this.InternalPlotY == PlotY.Y3)
                {
                    if (this.AxisY1.Parent.Equals(this.AxisX.Parent) == false)
                    {
                        var panel = this.AxisX.Parent as Panel;
                        if (panel != null)
                        {
                            panel.Children.Remove(this.AxisX);
                        }
                        ((Panel)this.AxisY1.Parent).Children.Add(this.AxisX);
                    }
                }
                else
                {
                    if (this.AxisY2.Parent.Equals(this.AxisX.Parent) == false)
                    {
                        var panel = this.AxisX.Parent as Panel;
                        if (panel != null)
                        {
                            panel.Children.Remove(this.AxisX);
                        }
                        ((Panel)this.AxisY2.Parent).Children.Add(this.AxisX);
                    }
                }
            }
        }

        #endregion

        #region event

        public event EventHandler<ChildRemovedEventArgs> ChildRemoved;

        internal void OnChildRemoved(UIElement child)
        {
            var handle = ChildRemoved;
            if (handle != null)
            {
                handle.Invoke(this, new ChildRemovedEventArgs(child));
            }
        }

        #endregion event

        #region dp

        #region host

        public Panel NotesHost
        {
            get { return (Panel)GetValue(NotesHostProperty); }
            set { SetValue(NotesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NotesHost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotesHostProperty =
            DependencyProperty.Register("NotesHost", typeof(Panel), typeof(PlotArea), null);



        /// <summary>
        /// x轴容器。
        /// </summary>
        internal Canvas AxisXHost
        {
            get { return (Canvas)GetValue(AxisXHostProperty); }
            private set { SetValue(AxisXHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisXHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty AxisXHostProperty =
            DependencyProperty.Register("AxisXHost", typeof(Canvas), typeof(PlotArea), null);

        /// <summary>
        /// 上半区Y轴容器。
        /// </summary>
        internal Panel TopAxisYHost
        {
            get { return (Panel)GetValue(TopAxisYHostProperty); }
            private set { SetValue(TopAxisYHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopAxisYHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TopAxisYHostProperty =
            DependencyProperty.Register("TopAxisYHost", typeof(Panel), typeof(PlotArea), null);

        /// <summary>
        /// 下半区Y轴容器。
        /// </summary>
        internal Panel BottomAxisYHost
        {
            get { return (Panel)GetValue(BottomAxisYHostProperty); }
            private set { SetValue(BottomAxisYHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomAxisYHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BottomAxisYHostProperty =
            DependencyProperty.Register("BottomAxisYHost", typeof(Panel), typeof(PlotArea), null);

        /// <summary>
        /// 上半区网格线和数据线容器。
        /// </summary>
        internal Panel TopLinesHost
        {
            get { return (Panel)GetValue(TopLinesHostProperty); }
            private set { SetValue(TopLinesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopLinesHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TopLinesHostProperty =
            DependencyProperty.Register("TopLinesHost", typeof(Panel), typeof(PlotArea), null);

        /// <summary>
        /// 下半区网格线和数据线容器。
        /// </summary>
        internal Panel BottomLinesHost
        {
            get { return (Panel)GetValue(BottomLinesHostProperty); }
            private set { SetValue(BottomLinesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomLinesHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BottomLinesHostProperty =
            DependencyProperty.Register("BottomLinesHost", typeof(Panel), typeof(PlotArea), null);


        internal Panel TopMarkUpHost
        {
            get { return (Panel)GetValue(TopMarkUpHostProperty); }
            set { SetValue(TopMarkUpHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopMarkUpHost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopMarkUpHostProperty =
            DependencyProperty.Register("TopMarkUpHost", typeof(Panel), typeof(PlotArea), null);



        internal Panel BottomMarkupHost
        {
            get { return (Panel)GetValue(BottomMarkupHostProperty); }
            set { SetValue(BottomMarkupHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomMarkupHost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomMarkupHostProperty =
            DependencyProperty.Register("BottomMarkupHost", typeof(Panel), typeof(PlotArea), null);


        internal Panel TopMarkLinesHost
        {
            get { return (Panel)GetValue(TopMarkLinesHostProperty); }
            set { SetValue(TopMarkLinesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopMarkLinesHost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopMarkLinesHostProperty =
            DependencyProperty.Register("TopMarkLinesHost", typeof(Panel), typeof(PlotArea), null);



        internal Panel BottomMarkLinesHost
        {
            get { return (Panel)GetValue(BottomMarkLinesHostProperty); }
            set { SetValue(BottomMarkLinesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomMarkLinesHost.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomMarkLinesHostProperty =
            DependencyProperty.Register("BottomMarkLinesHost", typeof(Panel), typeof(PlotArea), null);

        #endregion host

        #region length
        /// <summary>
        /// 长度，默认300.
        /// </summary>
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)300, OnLengthChanged));

        private static void OnLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            source.SetSize();
        }

        #endregion length

        #region TopHeight
        /// <summary>
        /// 上半区高度，默认150.
        /// </summary>
        public double TopHeight
        {
            get { return (double)GetValue(TopHeightProperty); }
            set { SetValue(TopHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopHeightProperty =
            DependencyProperty.Register("TopHeight", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)150, OnVisualChanged));

        #endregion TopHeight

        #region BottomHeight
        /// <summary>
        /// 下半区高度，默认150.
        /// </summary>
        public double BottomHeight
        {
            get { return (double)GetValue(BottomHeightProperty); }
            set { SetValue(BottomHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomHeightProperty =
            DependencyProperty.Register("BottomHeight", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)150, OnVisualChanged));

        #endregion BottomHeight

        #region XAlignment
        /// <summary>
        /// X轴的对齐方式，默认Bottom。
        /// </summary>
        public VerticalAlignment XAlignment
        {
            get { return (VerticalAlignment)GetValue(XAlignmentProperty); }
            set { SetValue(XAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XAlignmentProperty =
            DependencyProperty.Register("XAlignment", typeof(VerticalAlignment), typeof(PlotArea),
            new PropertyMetadata(VerticalAlignment.Bottom, OnVisualChanged));

        private static void OnVisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            source.SetAxisXLocation();
            source.SetSize();
        }

        #endregion XAlignment

        #region TopVisibility
        /// <summary>
        /// 上半区是否可见，默认Visible。Visible
        /// </summary>
        public Visibility TopVisibility
        {
            get { return (Visibility)GetValue(TopVisibilityProperty); }
            set { SetValue(TopVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopPanelVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopVisibilityProperty =
            DependencyProperty.Register("TopVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(Visibility.Visible, OnVisualChanged));

        #endregion TopVisibility

        #region BottomVisibility

        /// <summary>
        /// 下半区是否可见，默认Collapsed。
        /// </summary>
        public Visibility BottomVisibility
        {
            get { return (Visibility)GetValue(BottomVisibilityProperty); }
            set { SetValue(BottomVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomVisibilityProperty =
            DependencyProperty.Register("BottomVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(Visibility.Collapsed, OnVisualChanged));

        #endregion BottomVisibility

        #region border

        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(Visibility.Collapsed));


        public Brush BoederBrush
        {
            get { return (Brush)GetValue(BoederBrushProperty); }
            set { SetValue(BoederBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoederBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoederBrushProperty =
            DependencyProperty.Register("BoederBrush", typeof(Brush), typeof(PlotArea), null);


        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(Visibility.Collapsed));


        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(PlotArea), null);


        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(PlotArea),
            new PropertyMetadata(StrokeStyles.Solid));


        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)1));

        #endregion

        #region LinkType

        /// <summary>
        /// 图形类型，默认为线状图。可在文本轴下设置为柱状图，别的样式下无效。
        /// </summary>
        public LinkType LinkType
        {
            get { return (LinkType)GetValue(LinkTypeProperty); }
            set { SetValue(LinkTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkTypeProperty =
            DependencyProperty.Register("LinkType", typeof(LinkType), typeof(PlotArea),
            new PropertyMetadata(LinkType.Line, OnLinkTypeChanged));


        private static void OnLinkTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            if (source._ignoreChanged)
            {
                source._ignoreChanged = false;
                return;
            }

            if (source.AxisX.DataType != DataType.Text)
            {
                source._ignoreChanged = true;
                source.LinkType = (LinkType)e.OldValue;
            }

            source.DataLinks.Load();
        }

        #endregion

        #region InternalPlotY

        internal PlotY InternalPlotY
        {
            get { return (PlotY)GetValue(InternalPlotYProperty); }
            set { SetValue(InternalPlotYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalPlotY.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalPlotYProperty =
            DependencyProperty.Register("InternalPlotY", typeof(PlotY), typeof(PlotArea),
            new PropertyMetadata(PlotY.Y1, OnInternalPlotYChanged));

        private static void OnInternalPlotYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            var newValue = (PlotY)e.NewValue;
            source.InternalPlotYEx = newValue;
            var axisY = source.FindAxisY(newValue);
            source.SetAxisXParent();
            source.AxisX.BindingAxis = axisY;
            source.InternalDataType = axisY.DataType;
        }

        #endregion

        #region InternalPlotYEx

        internal PlotY InternalPlotYEx
        {
            get { return (PlotY)GetValue(InternalPlotYExProperty); }
            set { SetValue(InternalPlotYExProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalPlotYEx.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalPlotYExProperty =
            DependencyProperty.Register("InternalPlotYEx", typeof(PlotY), typeof(PlotArea),
            new PropertyMetadata(PlotY.Y1, OnInternalPlotYExChanged));

        private static void OnInternalPlotYExChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            source.InternalDataType = source.FindAxisY((PlotY)e.NewValue).DataType;
        }

        #endregion

        #region InternalDataType

        internal DataType InternalDataType
        {
            get { return (DataType)GetValue(InternalDataTypeProperty); }
            set { SetValue(InternalDataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalDataType.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty InternalDataTypeProperty =
            DependencyProperty.Register("InternalDataType", typeof(DataType), typeof(PlotArea),
            new PropertyMetadata(DataType.Numberic));

        #endregion

        private bool IsFixAxisXPosition
        {
            get { return (bool)GetValue(IsFixAxisXPositionProperty); }
            set { SetValue(IsFixAxisXPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsFixAxisXPosition.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty IsFixAxisXPositionProperty =
            DependencyProperty.Register("IsFixAxisXPosition", typeof(bool), typeof(PlotArea),
            new PropertyMetadata(true, OnIsFixAxisXPositionChanged));

        private static void OnIsFixAxisXPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as PlotArea;
            if ((bool)e.NewValue)
            {
                source.SetAxisXLocation();
            }
            else
            {
                source.SetAxisXParent();
            }
        }

        #endregion dp
    }
}
