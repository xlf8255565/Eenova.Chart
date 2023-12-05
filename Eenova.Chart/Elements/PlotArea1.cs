using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Eenova.Chart.Controls;
using Eenova.Chart.Interfaces;
using Eenova.Chart.Setter;
using System.Collections.Generic;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Elements
{
    public class PlotArea : Control, IPlotArea
    {
        #region field

        GridControl _selectionControl;

        Panel _layoutRoot;
        Panel _layoutArea;
        Panel _layoutTop;
        Panel _layoutBottom;
        Panel _topLines;
        Panel _bottomLines;

        Axis _y1;
        Axis _y2;
        Axis _y3;
        Axis _y4;

        GridLine _glX1;
        GridLine _glX2;
        GridLine _glY1;
        GridLine _glY2;
        GridLine _glY3;
        GridLine _glY4;

        AxisNoteHelper _noteHelper;
        #endregion field


        #region proterty

        internal Axis X0 { get; private set; }
        internal Panel NotesContainer { get; private set; }

        public DataLinkCollection DataLinks { get; private set; }

        public AxisNoteCollection Notes { get; private set; }

        #endregion property


        public PlotArea()
        {
            this.DefaultStyleKey = typeof(PlotArea);
            this.DataLinks = new DataLinkCollection();
            this.Notes = new AxisNoteCollection();
            _noteHelper = new AxisNoteHelper(this);
        }

        #region public method

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LoadControls();
            this.RegisterGridLine();
            this.SetAxisVisibility();
            this.LoadSelectionControl();
            this.SetAxisXLocation();
            this.AddDataLinks(this.DataLinks);

            this.LayoutUpdated += (s, e) => this.SetSize();
            this.DataLinks.CollectionChanged += new NotifyCollectionChangedEventHandler(DataLinks_CollectionChanged);
        }

        #endregion public method

        #region private method

        private double GetLeftWidth()
        {
            var lw1 = this.IsY1Visible && this.TopVisibility == Visibility.Visible ? Canvas.GetLeft(_y1) : 0;
            var lw2 = this.IsY2Visible && this.BottomVisibility == Visibility.Visible ? Canvas.GetLeft(_y2) : 0;
            var lw3 = this.IsY3Visible && this.TopVisibility == Visibility.Visible ? this.Length + Canvas.GetLeft(_y3) : 0;
            var lw4 = this.IsY4Visible && this.BottomVisibility == Visibility.Visible ? this.Length + Canvas.GetLeft(_y4) : 0;
            var left = -Math.Min(Math.Min(lw1, lw2), Math.Min(lw3, lw4)) + 10;

            return left;
        }

        private double GetRightWidth()
        {
            var rw1 = this.IsY1Visible && this.TopVisibility == Visibility.Visible ? _y1.ActualWidth + Canvas.GetLeft(_y1) : 0;
            var rw2 = this.IsY2Visible && this.BottomVisibility == Visibility.Visible ? _y2.ActualWidth + Canvas.GetLeft(_y2) : 0;
            var rw3 = this.IsY3Visible && this.TopVisibility == Visibility.Visible ? this.Length + _y3.ActualWidth + Canvas.GetLeft(_y3) : this.Length;
            var rw4 = this.IsY4Visible && this.BottomVisibility == Visibility.Visible ? this.Length + _y4.ActualWidth + Canvas.GetLeft(_y4) : this.Length;
            var right = Math.Max(Math.Max(rw1, rw2), Math.Max(rw3, rw4)) + 10;

            return right;
        }

        private double GetTopHeight()
        {
            var xTop = this.IsXVisible ? Canvas.GetTop(X0) + X0.Margin.Top : 0;
            var top = xTop < 0 ? -xTop + 10 : 10;

            return top;
        }

        private double GetBottomHeight()
        {
            var xBottom = this.IsXVisible ? X0.ActualHeight + Canvas.GetTop(X0) + X0.Margin.Top : 0;
            var topHeight = this.TopVisibility == Visibility.Visible ? this.TopHeight : 0;
            var bottomHeight = this.BottomVisibility == Visibility.Visible ? this.BottomHeight : 0;
            var height = topHeight + bottomHeight;
            var bottom = xBottom < height ? height + 10 : xBottom + 10;

            return bottom;
        }

        private void LoadControls()
        {
            _selectionControl = GetTemplateChild("SelectionControl") as GridControl;

            _layoutRoot = GetTemplateChild("LayoutRoot") as Panel;
            _layoutArea = GetTemplateChild("LayoutArea") as Panel;
            _layoutTop = GetTemplateChild("LayoutTop") as Panel;
            _layoutBottom = GetTemplateChild("LayoutBottom") as Panel;

            _topLines = GetTemplateChild("TopLines") as Panel;
            _bottomLines = GetTemplateChild("BottomLines") as Panel;
            NotesContainer = GetTemplateChild("NotesContainer") as Panel;

            X0 = GetTemplateChild("AxisX") as Axis;
            _y1 = GetTemplateChild("AxisYLeftTop") as Axis;
            _y2 = GetTemplateChild("AxisYLeftBottom") as Axis;
            _y3 = GetTemplateChild("AxisYRightTop") as Axis;
            _y4 = GetTemplateChild("AxisYRightBottom") as Axis;

            _glX1 = GetTemplateChild("GridLineXTop") as GridLine;
            _glX2 = GetTemplateChild("GridLineXBottom") as GridLine;
            _glY1 = GetTemplateChild("GridLineYLeftTop") as GridLine;
            _glY2 = GetTemplateChild("GridLineYLeftBottom") as GridLine;
            _glY3 = GetTemplateChild("GridLineYRightTop") as GridLine;
            _glY4 = GetTemplateChild("GridLineYRightBottom") as GridLine;
        }

        private void RegisterGridLine()
        {
            X0.RegisterGridLine(_glX1);
            X0.RegisterGridLine(_glX2);
            _y1.RegisterGridLine(_glY1);
            _y2.RegisterGridLine(_glY2);
            _y3.RegisterGridLine(_glY3);
            _y4.RegisterGridLine(_glY4);
        }

        private void SetAxisVisibility()
        {
            this.SetAxisVisibility(X0, this.IsXVisible);
            this.SetAxisVisibility(_y1, this.IsY1Visible);
            this.SetAxisVisibility(_y2, this.IsY2Visible);
            this.SetAxisVisibility(_y3, this.IsY3Visible);
            this.SetAxisVisibility(_y4, this.IsY4Visible);
        }

        private void SetAxisVisibility(Axis axis, bool visible)
        {
            if (axis == null)
                return;

            if (visible)
                axis.Opacity = 1;
            else
                axis.Opacity = 0;

            axis.IsHitEnable = visible;
        }

        private void SetAxisXLocation()
        {
            if (X0 == null)
                return;

            switch (this.XAlignment)
            {
                case VerticalAlignment.Top:
                    X0.Margin = new Thickness(0);
                    break;
                case VerticalAlignment.Bottom:
                    if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Visible)
                    {
                        X0.Margin = new Thickness(0, this.TopHeight + this.BottomHeight, 0, 0);
                    }
                    else if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Collapsed)
                    {
                        X0.Margin = new Thickness(0, this.TopHeight, 0, 0);
                    }
                    else if (this.TopVisibility == Visibility.Collapsed && this.BottomVisibility == Visibility.Visible)
                    {
                        X0.Margin = new Thickness(0, this.BottomHeight, 0, 0);
                    }
                    else
                    {
                    }
                    break;
                case VerticalAlignment.Center:
                    if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Visible)
                    {
                        X0.Margin = new Thickness(0, this.TopHeight, 0, 0);
                    }
                    else if (this.TopVisibility == Visibility.Visible && this.BottomVisibility == Visibility.Collapsed)
                    {
                        X0.Margin = new Thickness(0, this.TopHeight / 2, 0, 0);
                    }
                    else if (this.TopVisibility == Visibility.Collapsed && this.BottomVisibility == Visibility.Visible)
                    {
                        X0.Margin = new Thickness(0, this.BottomHeight / 2, 0, 0);
                    }
                    else
                    {
                    }
                    break;
            }
        }

        private void LoadSelectionControl()
        {
            _selectionControl.Loaded += (s, e) =>
            {
                var effect = new EffectRect() { Visibility = Visibility.Collapsed };
                _selectionControl.Add(effect);
                _selectionControl.SelectedEffectElement = effect;

                var item = new MenuItem() { Header = "添加注释" };
                item.Click += (sender, eventArgs) => _noteHelper.ShowNoteAddWindow();
                _selectionControl.ContextMenu.Items.Insert(0, item);
                _selectionControl.SettingWindow.Title = "设置绘图区格式";
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "显示", Content = new PlotAreaDisplaySetter(this) });
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "位置", Content = new PlotAreaPositionSetter(this) });
                _selectionControl.SettingWindow.Add(new SettingItem() { Header = "图案", Content = new BorderSetter(this) });
            };
        }

        private void SetSize()
        {
            if (_layoutRoot == null)
                return;

            var left = this.GetLeftWidth();
            var right = this.GetRightWidth();
            var top = this.GetTopHeight();
            var bottom = this.GetBottomHeight();
            _layoutRoot.Width = left + right;
            _layoutRoot.Height = top + bottom;
            _layoutArea.Margin = new Thickness(left, top, 0, 0);
        }

        private void DataLinks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.AddDataLinks(e.NewItems);
            }
        }

        private void AddDataLinks(IEnumerable links)
        {
            if (links == null)
                return;

            foreach (DataLink item in links)
            {
                if ((from l in this.DataLinks where l == item select l).Count() > 1)
                {
                    this.BeginInvokeRemove(item);
                    continue;
                }
                this.AddDataLink(item as DataLink);
            }
        }

        private void AddDataLink(DataLink link)
        {
            if (link == null)
                return;

            if (this.SetLinkAxisX(link) == false)
                return;

            if (this.SetLinkAxisY(link) == false)
                return;

            this.LoadDataLine(link);
            link.SetParent(this.DataLinks);
            link._parentArea = this;
        }

        internal void LoadDataLine(DataLink link)
        {
            if (link == null)
                return;

            var parentPanel = link.Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Children.Remove(link);
            }

            if (link.LinkedY == PlotY.Y1 || link.LinkedY == PlotY.Y3)
            {
                _topLines.Children.Add(link);
            }
            else
            {
                _bottomLines.Children.Add(link);
            }

            link.Load();
        }

        private bool SetLinkAxisX(DataLink link)
        {
            if (X0.IsDataTypeMatch(link.XDataType))
            {
                X0.DataType = link.XDataType;
                link.SetLinkAxisX(X0);
                return true;
            }
            else
            {
                MessageBox.Show("添加的数据线X轴数据类型与现有的X轴数据类型不匹配，请重新设置后再添加。");
                this.BeginInvokeRemove(link);
                return false;
            }
        }

        private bool SetLinkAxisY(DataLink link)
        {
            var axisY = this.GetAxisY(link.LinkedY);

            if (axisY.IsDataTypeMatch(link.YDataType))
            {
                axisY.DataType = link.YDataType;
                link.SetLinkAxisY(axisY);
                return true;
            }
            else
            {
                MessageBox.Show("添加的数据线Y轴数据类型与现有的Y轴数据类型不匹配，请重新设置后再添加。");
                this.BeginInvokeRemove(link);
                return false;
            }
        }

        internal Axis GetAxisY(PlotY linkY)
        {
            switch (linkY)
            {
                default:
                case PlotY.Y1:
                    return _y1;
                case PlotY.Y2:
                    return _y2;
                case PlotY.Y3:
                    return _y3;
                case PlotY.Y4:
                    return _y4;
            }
        }

        private void BeginInvokeRemove(DataLink link)
        {
            if (link == null)
                return;

            this.Dispatcher.BeginInvoke(() => this.DataLinks.Remove(link));
        }

        #endregion private method


        #region dp

        #region size

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)300));


        public double TopHeight
        {
            get { return (double)GetValue(TopHeightProperty); }
            set { SetValue(TopHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopHeightProperty =
            DependencyProperty.Register("TopHeight", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)150, OnTopHeightChanged));



        public double BottomHeight
        {
            get { return (double)GetValue(BottomHeightProperty); }
            set { SetValue(BottomHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomHeightProperty =
            DependencyProperty.Register("BottomHeight", typeof(double), typeof(PlotArea),
            new PropertyMetadata((double)150, OnBottomHeightChanged));

        #endregion


        #region visibility



        public VerticalAlignment XAlignment
        {
            get { return (VerticalAlignment)GetValue(XAlignmentProperty); }
            set { SetValue(XAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XAlignmentProperty =
            DependencyProperty.Register("XAlignment", typeof(VerticalAlignment), typeof(PlotArea),
            new PropertyMetadata(VerticalAlignment.Center, OnXAlignmentChanged));


        public Visibility TopVisibility
        {
            get { return (Visibility)GetValue(TopVisibilityProperty); }
            set { SetValue(TopVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopPanelVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopVisibilityProperty =
            DependencyProperty.Register("TopVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(OnTopVisibilityChanged));



        public Visibility BottomVisibility
        {
            get { return (Visibility)GetValue(BottomVisibilityProperty); }
            set { SetValue(BottomVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomVisibilityProperty =
            DependencyProperty.Register("BottomVisibility", typeof(Visibility), typeof(PlotArea),
            new PropertyMetadata(OnBottomVisibilityChanged));




        public bool IsXVisible
        {
            get { return (bool)GetValue(IsXVisibleProperty); }
            set { SetValue(IsXVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsXVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsXVisibleProperty =
            DependencyProperty.Register("IsXVisible", typeof(bool), typeof(PlotArea),
            new PropertyMetadata(OnIsXVisibleChanged));



        public bool IsY1Visible
        {
            get { return (bool)GetValue(IsY1VisibleProperty); }
            set { SetValue(IsY1VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsY1Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsY1VisibleProperty =
            DependencyProperty.Register("IsY1Visible", typeof(bool), typeof(PlotArea),
            new PropertyMetadata(OnIsY1VisibleChanged));



        public bool IsY2Visible
        {
            get { return (bool)GetValue(IsY2VisibleProperty); }
            set { SetValue(IsY2VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsY2Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsY2VisibleProperty =
            DependencyProperty.Register("IsY2Visible", typeof(bool), typeof(PlotArea),
            new PropertyMetadata(OnIsY2VisibleChanged));



        public bool IsY3Visible
        {
            get { return (bool)GetValue(IsY3VisibleProperty); }
            set { SetValue(IsY3VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsY3Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsY3VisibleProperty =
            DependencyProperty.Register("IsY3Visible", typeof(bool), typeof(PlotArea),
           new PropertyMetadata(OnIsY3VisibleChanged));



        public bool IsY4Visible
        {
            get { return (bool)GetValue(IsY4VisibleProperty); }
            set { SetValue(IsY4VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsY4Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsY4VisibleProperty =
            DependencyProperty.Register("IsY4Visible", typeof(bool), typeof(PlotArea),
            new PropertyMetadata(OnIsY4VisibleChanged));



        public Visibility LX1Visibility
        {
            get { return (Visibility)GetValue(LX1VisibilityProperty); }
            set { SetValue(LX1VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LX1Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LX1VisibilityProperty =
            DependencyProperty.Register("LX1Visibility", typeof(Visibility), typeof(PlotArea), null);



        public Visibility LX2Visibility
        {
            get { return (Visibility)GetValue(LX2VisibilityProperty); }
            set { SetValue(LX2VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LX2Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LX2VisibilityProperty =
            DependencyProperty.Register("LX2Visibility", typeof(Visibility), typeof(PlotArea), null);



        public Visibility LY1Visibility
        {
            get { return (Visibility)GetValue(LY1VisibilityProperty); }
            set { SetValue(LY1VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LY1Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LY1VisibilityProperty =
            DependencyProperty.Register("LY1Visibility", typeof(Visibility), typeof(PlotArea), null);



        public Visibility LY2Visibility
        {
            get { return (Visibility)GetValue(LY2VisibilityProperty); }
            set { SetValue(LY2VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LY2Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LY2VisibilityProperty =
            DependencyProperty.Register("LY2Visibility", typeof(Visibility), typeof(PlotArea), null);



        public Visibility LY3Visibility
        {
            get { return (Visibility)GetValue(LY3VisibilityProperty); }
            set { SetValue(LY3VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LY3Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LY3VisibilityProperty =
            DependencyProperty.Register("LY3Visibility", typeof(Visibility), typeof(PlotArea), null);



        public Visibility LY4Visibility
        {
            get { return (Visibility)GetValue(LY4VisibilityProperty); }
            set { SetValue(LY4VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LY4Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LY4VisibilityProperty =
            DependencyProperty.Register("LY4Visibility", typeof(Visibility), typeof(PlotArea), null);



        #endregion


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

        #endregion dp


        #region static callback

        private static void OnTopHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnTopHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnBottomHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnBottomHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnTopVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnTopVisibilityChanged((Visibility)e.OldValue, (Visibility)e.NewValue);
        }

        private static void OnBottomVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnBottomVisibilityChanged((Visibility)e.OldValue, (Visibility)e.NewValue);
        }

        private static void OnXAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnXAlignmentChanged((VerticalAlignment)e.OldValue, (VerticalAlignment)e.NewValue);
        }

        private static void OnIsXVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnIsXVisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsY1VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnIsY1VisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsY3VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnIsY3VisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsY2VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnIsY2VisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnIsY4VisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as PlotArea;
            ctrl.OnIsY4VisibleChanged((bool)e.OldValue, (bool)e.NewValue);
        }


        #endregion


        #region callback

        private void OnTopHeightChanged(double oldValue, double newValue)
        {
            this.SetAxisXLocation();
            //this.SetTopHeight();
        }

        private void OnBottomHeightChanged(double oldValue, double newValue)
        {
            this.SetAxisXLocation();
        }

        private void OnTopVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
            this.SetAxisXLocation();
        }

        private void OnBottomVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
            this.SetAxisXLocation();
        }

        private void OnXAlignmentChanged(VerticalAlignment oldValue, VerticalAlignment newValue)
        {
            this.SetAxisXLocation();
        }

        private void OnIsXVisibleChanged(bool oldValue, bool newValue)
        {
            this.SetAxisVisibility(X0, newValue);
        }

        private void OnIsY1VisibleChanged(bool oldValue, bool newValue)
        {
            this.SetAxisVisibility(_y1, newValue);
        }

        private void OnIsY3VisibleChanged(bool oldValue, bool newValue)
        {
            this.SetAxisVisibility(_y3, newValue);
        }

        private void OnIsY2VisibleChanged(bool oldValue, bool newValue)
        {
            this.SetAxisVisibility(_y2, newValue);
        }

        private void OnIsY4VisibleChanged(bool oldValue, bool newValue)
        {
            this.SetAxisVisibility(_y4, newValue);
        }
        #endregion

    }
}
