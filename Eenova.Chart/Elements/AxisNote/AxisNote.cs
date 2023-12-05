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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Controls;
using Eenova.Chart.Helpers;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public class AxisNote : UIControl
    {
        #region field

        AxisX _axisX;
        Title _note;
        Polyline _pointingLine;

        #endregion

        #region property

        internal AxisX AxisX
        {
            get { return _axisX; }
            set
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

        protected override string SettingTitle
        {
            get { return "设置注释格式"; }
        }

        #endregion

        #region constructor

        public AxisNote()
        {
            this.DefaultStyleKey = typeof(AxisNote);
            this.SelectedEffect = new EffectRect();
        }

        public AxisNote(DateTime start, DateTime end)
            : this()
        {
            if (start > end)
                throw new ArgumentException("start must less than end");

            this.StartValue = TimeHelper.GetSpanTime(start);
            this.EndValue = TimeHelper.GetSpanTime(end);
        }

        #endregion

        #region public method

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _note = this.GetTemplateChild("Note") as Title;
            _pointingLine = this.GetTemplateChild("Pointing") as Polyline;

            _note.LayoutUpdated += new EventHandler(NoteLayoutUpdated);
            _note.SizeChanged += new SizeChangedEventHandler(NoteSizeChanged);
            this.Load();
        }

        #endregion

        #region internal method

        internal void Load()
        {
            if (this.AxisX == null ||
                this.AxisX.DataType == DataType.Text)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            var startValue = Math.Max(Math.Min(this.StartValue, this.EndValue), this.AxisX.MinValue);
            var endValue = Math.Min(Math.Max(this.StartValue, this.EndValue), this.AxisX.MaxValue);
            if (startValue >= endValue)
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            var values = new List<object>();
            if (this.AxisX.DataType == DataType.DateTime)
            {
                values.Add(TimeHelper.GetTime(startValue));
                values.Add(TimeHelper.GetTime(endValue));
            }
            else
            {
                values.Add(startValue);
                values.Add(endValue);
            }

            var results = this.AxisX.Convert(values);
            if (double.IsNaN(results[0]) ||
                double.IsNaN(results[1]))
            {
                this.Visibility = Visibility.Collapsed;
                return;
            }

            var max = Math.Max(results[0], results[1]);
            var min = Math.Min(results[0], results[1]);
            this.InternalWidth = max - min;

            var left = this.AxisX.IsDesc ? Math.Max(0, this.AxisX.Length - max) : Math.Max(0, min);
            this.InternalMargin = new Thickness(left, 0, 0, 0);
            this.Visibility = Visibility.Visible;
        }

        #endregion

        #region protected method

        protected override void LoadMenu()
        {
            base.LoadMenu();

            var item = new ImproveMenuItem() { Header = "删除" };
            item.Click += (s, e) => this.OnToDelete();
            ContextMenu.Items.Add(item);
        }

        protected override void LoadSetter()
        {
            var items = new List<TabItem>();
            items.Add(new TabItem { Header = "名称", Content = new CommonTextSetter { DataContext = _note } });
            items.Add(new TabItem { Header = "位置", Content = new AxisNotePositionSetter { DataContext = this } });
            items.Add(new TabItem { Header = "字体", Content = new CommonFontSetter { DataContext = _note } });
            SettingWindow.Add("注释", new SettingGroup { ItemsSource = items });

            var setter = new AxisNoteLineStyleSetter { DataContext = this };
            setter.SetBinding(AxisNoteLineStyleSetter.DataTypeProperty, new Binding("DataType") { Source = this.AxisX });
            SettingWindow.Add("位置", setter);

            SettingWindow.Add("标注线", new CommonStrokeSetter { DataContext = this });
            SettingWindow.Add("左边线", new AxisNoteLeftSideSetter { DataContext = this });
            SettingWindow.Add("右边线", new AxisNoteRightSideSetter { DataContext = this });
            SettingWindow.Add("指示线", new AxisNotePointingLineSetter { DataContext = this });
        }

        #endregion

        #region private method

        private void SetNoteLocation()
        {
            if (_note == null ||
                _pointingLine == null)
                return;

            var vertical = this.VerticalOffset;
            var point = new Point() { Y = vertical };

            if (this.NoteLocation == NoteLocation.Top)
            {
                vertical = -(_note.ActualHeight + vertical);
                point.Y = -point.Y;
            }

            var horizontal = -_note.ActualWidth / 2 + this.HorizontalOffset;
            point.X = this.HorizontalOffset;

            Canvas.SetTop(_note, vertical);
            Canvas.SetLeft(_note, horizontal);
            _pointingLine.Points[1] = point;
        }

        private void NoteLayoutUpdated(object sender, EventArgs e)
        {
            _note.LayoutUpdated -= new EventHandler(NoteLayoutUpdated);
            this.SetNoteLocation();
        }

        private void NoteSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SetNoteLocation();
        }

        #endregion

        #region event

        internal event EventHandler ToDelete;

        private void OnToDelete()
        {
            if (ToDelete != null)
            {
                ToDelete(this, null);
            }
        }

        #endregion

        #region dp

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AxisNote),
            new PropertyMetadata("注释"));

        #endregion

        #region InternalWidth

        public double InternalWidth
        {
            get { return (double)GetValue(InternalWidthProperty); }
            set { SetValue(InternalWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalWidthProperty =
            DependencyProperty.Register("InternalWidth", typeof(double), typeof(AxisNote),
            new PropertyMetadata(100.0));

        #endregion

        #region InternalMargin

        public Thickness InternalMargin
        {
            get { return (Thickness)GetValue(InternalMarginProperty); }
            set { SetValue(InternalMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalMarginProperty =
            DependencyProperty.Register("InternalMargin", typeof(Thickness), typeof(AxisNote),
            new PropertyMetadata(new Thickness()));


        #endregion

        #region Value

        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(double), typeof(AxisNote),
            new PropertyMetadata(0.0, OnValueChanged));


        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(AxisNote), new
                PropertyMetadata(100.0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisNote;
            source.Load();
        }

        #endregion

        #region SideHeight

        public double SideHeight
        {
            get { return (double)GetValue(SideHeightProperty); }
            set { SetValue(SideHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SideHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SideHeightProperty =
            DependencyProperty.Register("SideHeight", typeof(double), typeof(AxisNote),
            new PropertyMetadata(20.0));

        #endregion

        #region Top

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(AxisNote),
            new PropertyMetadata(0.0));

        #endregion

        #region HorizontalOffset

        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(AxisNote),
            new PropertyMetadata(0.0, OnNoteLocationChanged));

        #endregion

        #region VerticalOffset

        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(AxisNote),
            new PropertyMetadata(5.0, OnNoteLocationChanged));

        #endregion

        #region NoteLocation

        public NoteLocation NoteLocation
        {
            get { return (NoteLocation)GetValue(NoteLocationProperty); }
            set { SetValue(NoteLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoteLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteLocationProperty =
            DependencyProperty.Register("NoteLocation", typeof(NoteLocation), typeof(AxisNote),
            new PropertyMetadata(NoteLocation.Top, OnNoteLocationChanged));

        private static void OnNoteLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisNote;
            source.SetNoteLocation();
        }

        #endregion

        #region ArrowSize

        public double ArrowSize
        {
            get { return (double)GetValue(ArrowSizeProperty); }
            set { SetValue(ArrowSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArrowSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.Register("ArrowSize", typeof(double), typeof(AxisNote),
            new PropertyMetadata(6.0));

        #endregion

        #region StrokeLine

        #region StrokeVisibility

        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(AxisNote),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region StrokeThickness

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(AxisNote),
            new PropertyMetadata(2.0));

        #endregion

        #region StrokeStyle

        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(AxisNote),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion

        #region Stroke

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(AxisNote),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #endregion

        #region LeftSide

        #region LeftSideVisibility

        public Visibility LeftSideVisibility
        {
            get { return (Visibility)GetValue(LeftSideVisibilityProperty); }
            set { SetValue(LeftSideVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftSideVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftSideVisibilityProperty =
            DependencyProperty.Register("LeftSideVisibility", typeof(Visibility), typeof(AxisNote),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region LeftSideThickness

        public double LeftSideThickness
        {
            get { return (double)GetValue(LeftSideThicknessProperty); }
            set { SetValue(LeftSideThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftSideThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftSideThicknessProperty =
            DependencyProperty.Register("LeftSideThickness", typeof(double), typeof(AxisNote),
            new PropertyMetadata(2.0));

        #endregion

        #region LeftSideStyle

        public string LeftSideStyle
        {
            get { return (string)GetValue(LeftSideStyleProperty); }
            set { SetValue(LeftSideStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftSideStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftSideStyleProperty =
            DependencyProperty.Register("LeftSideStyle", typeof(string), typeof(AxisNote),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion

        #region LeftSideStroke

        public Brush LeftSideStroke
        {
            get { return (Brush)GetValue(LeftSideStrokeProperty); }
            set { SetValue(LeftSideStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftSideStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftSideStrokeProperty =
            DependencyProperty.Register("LeftSideStroke", typeof(Brush), typeof(AxisNote),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #endregion

        #region RightSide

        #region RightSideVisibility

        public Visibility RightSideVisibility
        {
            get { return (Visibility)GetValue(RightSideVisibilityProperty); }
            set { SetValue(RightSideVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSideVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSideVisibilityProperty =
            DependencyProperty.Register("RightSideVisibility", typeof(Visibility), typeof(AxisNote),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region RightSideThickness

        public double RightSideThickness
        {
            get { return (double)GetValue(RightSideThicknessProperty); }
            set { SetValue(RightSideThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSideThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSideThicknessProperty =
            DependencyProperty.Register("RightSideThickness", typeof(double), typeof(AxisNote),
            new PropertyMetadata(2.0));

        #endregion

        #region RightSideStyle

        public string RightSideStyle
        {
            get { return (string)GetValue(RightSideStyleProperty); }
            set { SetValue(RightSideStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSideStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSideStyleProperty =
            DependencyProperty.Register("RightSideStyle", typeof(string), typeof(AxisNote),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion

        #region RightSideStroke

        public Brush RightSideStroke
        {
            get { return (Brush)GetValue(RightSideStrokeProperty); }
            set { SetValue(RightSideStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSideStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSideStrokeProperty =
            DependencyProperty.Register("RightSideStroke", typeof(Brush), typeof(AxisNote),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #endregion

        #region PointingLine

        #region PointingLineVisibility

        public Visibility PointingLineVisibility
        {
            get { return (Visibility)GetValue(PointingLineVisibilityProperty); }
            set { SetValue(PointingLineVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointingLineVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointingLineVisibilityProperty =
            DependencyProperty.Register("PointingLineVisibility", typeof(Visibility), typeof(AxisNote),
            new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #region PointingLineThickness

        public double PointingLineThickness
        {
            get { return (double)GetValue(PointingLineThicknessProperty); }
            set { SetValue(PointingLineThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointingLineThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointingLineThicknessProperty =
            DependencyProperty.Register("PointingLineThickness", typeof(double), typeof(AxisNote),
            new PropertyMetadata(1.0));

        #endregion

        #region PointingLineStyle

        public string PointingLineStyle
        {
            get { return (string)GetValue(PointingLineStyleProperty); }
            set { SetValue(PointingLineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointingLineStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointingLineStyleProperty =
            DependencyProperty.Register("PointingLineStyle", typeof(string), typeof(AxisNote),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion

        #region PointingLineStroke

        public Brush PointingLineStroke
        {
            get { return (Brush)GetValue(PointingLineStrokeProperty); }
            set { SetValue(PointingLineStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointingLineStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointingLineStrokeProperty =
            DependencyProperty.Register("PointingLineStroke", typeof(Brush), typeof(AxisNote),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #endregion

        #endregion
    }
}
