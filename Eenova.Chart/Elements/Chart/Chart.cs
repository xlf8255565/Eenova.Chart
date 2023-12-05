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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Eenova.Chart.Behaviors;
using Eenova.Chart.Controls;
using Eenova.Chart.Helpers.Image;
using Eenova.Chart.Setter;
using Eenova.Chart.Styler;

namespace Eenova.Chart.Elements
{
    public class Chart : UIControl
    {
        #region field

        MouseDragElementBehavior _drag;
        ChartResizer _resizer;
        XmlStyler _styler;
        Point _mousePosition;

        ImproveMenuItem _zoomOutItem;
        ImproveMenuItem _zoomInItem;
        ImproveMenuItem _recoveryItem;

        #endregion

        #region property

        public object Mark { get; set; }
        public DateTime CreateTime { get; set; }
        public ChartChildCollection<TitleNote> TitleNotes { get; private set; }
        public ChartChildCollection<PlotArea> PlotAreas { get; private set; }
        public ChartChildCollection<Legend> Legends { get; private set; }

        #endregion

        #region constructor

        public Chart()
        {
            this.DefaultStyleKey = typeof(Chart);

            //this.CacheMode = new BitmapCache();
            this.Init();
        }

        //~Chart()
        //{
        //    //GC.Collect();
        //    //GC.WaitForPendingFinalizers();
        //    //GC.Collect();
        //    System.Diagnostics.Debug.WriteLine("Release");
        //}

        #endregion

        #region public method

        public static Chart Create(string xml)
        {
            return Create(XElement.Parse(xml));
        }

        public static Chart Create(XElement xml)
        {
            var chart = new Chart();
            chart._styler.ReadXml(xml);
            return chart;
        }

        public void Load(string xml)
        {
            _styler.ReadXml(XElement.Parse(xml));
        }

        public void Load(XElement xml)
        {
            _styler.ReadXml(xml);
        }

        public string GetStyle()
        {
            return _styler.CreateXml().ToString();
        }

        #endregion

        #region protected method

        protected override void LoadMenu()
        {
            base.LoadMenu();

            var item = new ImproveMenuItem() { Header = "添加标题备注" };
            item.Click += (s, e) => AddTitleNote();
            ContextMenu.Items.Insert(0, item);

            item = new ImproveMenuItem() { Header = "图片另存为" };
            item.Click += (s, e) => SaveImage();
            ContextMenu.Items.Insert(0, item);

            _recoveryItem = new ImproveMenuItem() { Header = "还原" };
            _recoveryItem.Click += (s, e) => this.Recovery();
            ContextMenu.Items.Insert(0, _recoveryItem);

            _zoomInItem = new ImproveMenuItem() { Header = "缩小" };
            _zoomInItem.Click += (s, e) => this.ZoomIn();
            ContextMenu.Items.Insert(0, _zoomInItem);

            _zoomOutItem = new ImproveMenuItem() { Header = "放大" };
            _zoomOutItem.Click += (s, e) => this.ZoomOut();
            ContextMenu.Items.Insert(0, _zoomOutItem);

            this.SetZoomItemVisibility();

#if DEBUG
            item = new ImproveMenuItem() { Header = "添加绘图区" };
            item.Click += (s, e) => AddPlotArea();
            ContextMenu.Items.Insert(0, item);

            item = new ImproveMenuItem() { Header = "添加图例" };
            item.Click += (s, e) => AddLegend();
            ContextMenu.Items.Insert(0, item);

            item = new ImproveMenuItem() { Header = "保存样式" };
            item.Click += (s, e) => _styler.SaveStyle();
            ContextMenu.Items.Add(item);

            item = new ImproveMenuItem() { Header = "应用样式" };
            item.Click += (s, e) => _styler.ApplyStyle();
            ContextMenu.Items.Add(item);
#endif
        }

        private void ZoomIn()
        {
            this.Height = this.ActualHeight;
            this.Width = this.ActualWidth;

            Multiple--;

            foreach (var area in this.PlotAreas)
            {
                area.Length /= 2;
                area.SetSize();
            }
            this.SetZoomItemVisibility();
        }

        private void ZoomOut()
        {
            this.Height = this.ActualHeight;
            this.Width = this.ActualWidth;

            Multiple++;

            foreach (var area in this.PlotAreas)
            {
                area.Length *= 2;
                area.SetSize();
            }
            this.SetZoomItemVisibility();
        }

        private void Recovery()
        {
            while (Multiple > 0)
            {
                foreach (var area in this.PlotAreas)
                {
                    area.Length /= 2;
                    area.SetSize();
                }
                Multiple--;
            }
            this.SetZoomItemVisibility();
        }

        private void SetZoomItemVisibility()
        {
            _zoomInItem.Visibility = Multiple > 0 ? Visibility.Visible : Visibility.Collapsed;
            _recoveryItem.Visibility = Multiple > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override string SettingTitle
        {
            get { return "设置图框格式"; }
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("位置", new CommonPositionSetter() { DataContext = this });
            SettingWindow.Add("图案", new CommonBorderSetter() { DataContext = this });
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Enter", true);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.IsSelected == false)
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            _mousePosition = e.GetPosition(this);
            base.OnMouseRightButtonDown(e);
        }

        #endregion

        #region private method

        private void Init()
        {
            this.SelectedEffect = new EffectRect();
            this.RenderTransform = new CompositeTransform();

            _styler = new ChartStyler(this);

            _resizer = new ChartResizer();
            _resizer.Attach(this);

            _drag = new MouseDragElementBehavior() { ConstrainToParentBounds = true };
            _drag.Attach(this);

            this.PlotsHost = new Grid();
            this.PlotAreas = new ChartChildCollection<PlotArea>(this, this.PlotsHost);

            this.LegendsHost = new Grid();
            this.Legends = new ChartChildCollection<Legend>(this, this.LegendsHost);

            this.TitlesHost = new Grid();
            this.TitleNotes = new ChartChildCollection<TitleNote>(this, this.TitlesHost);

            this.SizeChanged += (s, e) => this.SetClip(e.NewSize);
        }

        private void SetClip(Size size)
        {
            //this.PlotsHost.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), size) };
            this.LegendsHost.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), size) };
            this.TitlesHost.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), size) };
        }

        private void SaveImage()
        {
            var sfd = new SaveFileDialog()
            {
                DefaultExt = "png",
                Filter = "Png files (*.png)|*.png|All files (*.*)|*.*",
                FilterIndex = 1
            };

            if (sfd.ShowDialog() == true)
            {
                this.SelectorVisibility = Visibility.Collapsed;
                if (this.Background == null)
                    this.Background = new SolidColorBrush(Colors.White);
                var bitmap = new WriteableBitmap(this, null);
                this.SelectorVisibility = Visibility.Visible;
                var imageData = new EditableImage(bitmap.PixelWidth, bitmap.PixelHeight);
                try
                {
                    for (int y = 0; y < bitmap.PixelHeight; ++y)
                    {
                        for (int x = 0; x < bitmap.PixelWidth; ++x)
                        {
                            int pixel = bitmap.Pixels[bitmap.PixelWidth * y + x];
                            imageData.SetPixel(x, y,
                            (byte)((pixel >> 16) & 0xFF),
                            (byte)((pixel >> 8) & 0xFF),
                            (byte)(pixel & 0xFF), (byte)((pixel >> 24) & 0xFF));
                        }
                    }
                }
                catch (System.Security.SecurityException)
                {
                    throw new Exception("保存失败，请重试。");
                }

                Stream pngStream = imageData.GetStream();

                StreamReader sr = new StreamReader(pngStream);
                byte[] binaryData = new Byte[pngStream.Length];

                long bytesRead = pngStream.Read(binaryData, 0, (int)pngStream.Length);
                using (Stream stream = sfd.OpenFile())
                {
                    stream.Write(binaryData, 0, binaryData.Length);
                    stream.Close();
                }
            }
        }

        private void AddLegend()
        {
            var legend = new Legend()
            {
                //Margin = new Thickness(_mousePosition.X, _mousePosition.Y, 0, 0),
                RenderTransform = new CompositeTransform { TranslateX = _mousePosition.X, TranslateY = _mousePosition.Y },
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                MinHeight = 20,
                MinWidth = 20
            };
            this.Legends.Add(legend);
        }

        private void AddPlotArea()
        {
            var area = new PlotArea()
            {
                //Margin = new Thickness(_mousePosition.X, _mousePosition.Y, 0, 0),
                RenderTransform = new CompositeTransform { TranslateX = _mousePosition.X, TranslateY = _mousePosition.Y },
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            this.PlotAreas.Add(area);
        }

        private void AddTitleNote()
        {
            var titleNote = new TitleNote()
            {
                //Margin = new Thickness(_mousePosition.X, _mousePosition.Y, 0, 0),
                RenderTransform = new CompositeTransform { TranslateX = _mousePosition.X, TranslateY = _mousePosition.Y },
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "备注"
            };
            this.TitleNotes.Add(titleNote);
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

        #endregion

        #region dp

        #region SelectorVisibility

        public Visibility SelectorVisibility
        {
            get { return (Visibility)GetValue(SelectorVisibilityProperty); }
            set { SetValue(SelectorVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectorVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectorVisibilityProperty =
            DependencyProperty.Register("SelectorVisibility", typeof(Visibility), typeof(Chart),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region IsSelected

        /// <summary>
        /// 当前控件是否选中，默认false。
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Chart), new PropertyMetadata(false));

        #endregion

        #region StrokeVisibility
        /// <summary>
        /// 边框线是否可见，默认Visible.
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(Chart),
            new PropertyMetadata(Visibility.Visible));

        #endregion StrokeVisibility

        #region StrokeStyle
        /// <summary>
        /// 线条样式,默认StrokeStyles.Solid。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(Chart),
            new PropertyMetadata(StrokeStyles.Solid));

        #endregion StrokeStyle

        #region StrokeThickness
        /// <summary>
        /// 边框线厚度，默认1。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Chart),
            new PropertyMetadata((double)1));

        #endregion StrokeThickness

        #region Stroke
        /// <summary>
        /// 边框线颜色，默认Colors.Black。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Chart),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion Stroke

        #region BorderVisibility
        /// <summary>
        /// 底框是否可见，默认Collapsed。
        /// </summary>
        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(Chart),
            new PropertyMetadata(Visibility.Collapsed));

        #endregion BorderVisibility

        #region PlotsHost
        /// <summary>
        /// 绘图区容器。
        /// </summary>
        internal Panel PlotsHost
        {
            get { return (Panel)GetValue(PlotsHostProperty); }
            private set { SetValue(PlotsHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlotsHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty PlotsHostProperty =
            DependencyProperty.Register("PlotsHost", typeof(Panel), typeof(Chart), null);

        #endregion PlotsHost

        #region LegendsHost
        /// <summary>
        /// 图例容器。
        /// </summary>
        internal Panel LegendsHost
        {
            get { return (Panel)GetValue(LegendsHostProperty); }
            private set { SetValue(LegendsHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LegendsHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty LegendsHostProperty =
            DependencyProperty.Register("LegendsHost", typeof(Panel), typeof(Chart), null);

        #endregion LegendsHost

        #region TitlesHost
        /// <summary>
        /// 标题容器。
        /// </summary>
        internal Panel TitlesHost
        {
            get { return (Panel)GetValue(TitlesHostProperty); }
            private set { SetValue(TitlesHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitlesHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TitlesHostProperty =
            DependencyProperty.Register("TitlesHost", typeof(Panel), typeof(Chart), null);

        #endregion TitlesHost




        public int Multiple
        {
            get { return (int)GetValue(MultipleProperty); }
            set { SetValue(MultipleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiple.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultipleProperty =
            DependencyProperty.Register("Multiple", typeof(int), typeof(Chart), new PropertyMetadata(0));




        public Brush MarkBrush
        {
            get { return (Brush)GetValue(MarkBrushProperty); }
            set { SetValue(MarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushProperty =
            DependencyProperty.Register("MarkBrush", typeof(Brush), typeof(Chart), null);



        public Visibility MarkVisibility
        {
            get { return (Visibility)GetValue(MarkVisibilityProperty); }
            set { SetValue(MarkVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarkVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkVisibilityProperty =
            DependencyProperty.Register("MarkVisibility", typeof(Visibility), typeof(Chart),
            new PropertyMetadata(Visibility.Collapsed));


        #endregion
    }
}
