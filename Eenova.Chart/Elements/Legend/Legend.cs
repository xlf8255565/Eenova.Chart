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


using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Behaviors;
using Eenova.Chart.Controls;
using Eenova.Chart.Factories;
using Eenova.Chart.Setter;
using System.Collections.ObjectModel;

namespace Eenova.Chart.Elements
{
    public class Legend : ChartChild
    {
        IList<Panel> _cacheItems;
        Panel _itemsPanel;
        MouseDragElementBehavior _drag;

        protected override string SettingTitle
        {
            get { return "设置图例格式"; }
        }

        public Legend()
        {
            this.DefaultStyleKey = typeof(Legend);
            this.Init();
        }

        #region protected method

        protected override void LoadMenu()
        {
            base.LoadMenu();

#if DEBUG
            var item = new ImproveMenuItem() { Header = "删除" };
            item.Click += (s, e) => this.OnToDelete();
            ContextMenu.Items.Insert(0, item);
#endif
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("位置", new CommonPositionSetter { DataContext = this });
            SettingWindow.Add("图案", new CommonBorderSetter { DataContext = this });
            SettingWindow.Add("字体", new CommonFontSetter { DataContext = this });
            SettingWindow.Add("方向", new OrientationSetter { DataContext = this });
        }

        #endregion protected method

        #region private method

        private void Init()
        {
            this.RenderTransform = new CompositeTransform();
            _drag = new MouseDragElementBehavior() { ConstrainToParentBounds = true };
            _drag.Attach(this);

            this.MinHeight = 20;
            this.MinWidth = 20;
            _cacheItems = new List<Panel>();
            this.DataLinks = new ObservableCollection<DataLink>();
            this.SelectedEffect = new EffectRect();
            this.ItemsHost = new Grid();
            _itemsPanel = new WrapPanel() { Margin = new Thickness(4) };
            _itemsPanel.SetBinding(WrapPanel.OrientationProperty, new Binding("Orientation") { Source = this });
            this.ItemsHost.Children.Add(_itemsPanel);
            this.Load();
        }

        /// <summary>
        /// 加载图例。
        /// </summary>
        private void Load()
        {
            //Debug.WriteLine("load legend");
            this.AddItems(this.DataLinks);
            //Debug.WriteLine("load legend over");
        }

        private void AddItems(IEnumerable items)
        {
            if (items == null)
                return;

            Panel p;
            foreach (DataLink link in items)
            {
                p = this.FindItemFromCache(link);
                if (p == null)
                {
                    p = this.CreateItem(link);
                    _cacheItems.Add(p);
                }
                if (!_itemsPanel.Children.Contains(p))
                    _itemsPanel.Children.Add(p);
            }
        }

        private void ClearItems()
        {
            _itemsPanel.Children.Clear();
        }

        private void RemoveItems(IEnumerable items)
        {
            Panel p;
            foreach (DataLink link in items)
            {
                p = this.FindItemFromCache(link);
                if (p != null && _itemsPanel.Children.Contains(p))
                    _itemsPanel.Children.Remove(p);
            }
        }

        private Panel FindItemFromCache(DataLink link)
        {
            return _cacheItems.FirstOrDefault(p => p.Tag == link);
        }

        /// <summary>
        /// 创建图例项。
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private Panel CreateItem(DataLink link)
        {
            var panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(2),
                Background = new SolidColorBrush(Colors.Transparent)
            };
            var legendGrid = new Grid();
            legendGrid.Children.Add(this.CreateLine(link));
            legendGrid.Children.Add(MarkFactory.Create(link));
            panel.Children.Add(legendGrid);
            panel.Children.Add(this.CreateTitle(link));
            panel.Tag = link;

            panel.MouseLeftButtonDown += (s, e) =>
            {
                e.Handled = true;
                link.Select();
            };
            return panel;
        }
        /// <summary>
        /// 创建图例线。
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private Shape CreateLine(DataLink link)
        {
            var line = new Polyline()
            {
                Stretch = Stretch.Fill,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            line.Points.Add(new Point(0, 0));
            line.Points.Add(new Point(30, 0));

            line.SetBinding(Polyline.StrokeDashArrayProperty, new Binding("StrokeStyle") { Source = link });
            line.SetBinding(Polyline.StrokeThicknessProperty, new Binding("StrokeThickness") { Source = link });
            line.SetBinding(Polyline.StrokeProperty, new Binding("Stroke") { Source = link });
            line.SetBinding(Polyline.VisibilityProperty, new Binding("StrokeVisibility") { Source = link });

            return line;
        }
        /// <summary>
        /// 创建图例文本。
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private Title CreateTitle(DataLink link)
        {
            var title = new Title()
            {
                StrokeVisibility = Visibility.Collapsed,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Thickness(1)
            };

            title.SetBinding(Title.TextProperty, new Binding("Text") { Source = link });
            title.SetBinding(Title.FontFamilyProperty, new Binding("FontFamily") { Source = this });
            title.SetBinding(Title.FontSizeProperty, new Binding("FontSize") { Source = this });
            title.SetBinding(Title.FontStyleProperty, new Binding("FontStyle") { Source = this });
            title.SetBinding(Title.FontWeightProperty, new Binding("FontWeight") { Source = this });
            title.SetBinding(Title.ForegroundProperty, new Binding("Foreground") { Source = this });
            title.SetBinding(Title.BackgroundProperty, new Binding("Background") { Source = this });
            title.SetBinding(Title.TextDecorationsProperty, new Binding("TextDecorations") { Source = this });

            return title;
        }

        #endregion private method

        #region dp

        /// <summary>
        /// 数据源集合。
        /// </summary>
        public ObservableCollection<DataLink> DataLinks
        {
            get { return (ObservableCollection<DataLink>)GetValue(DataLinksProperty); }
            set { SetValue(DataLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataLinks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataLinksProperty =
            DependencyProperty.Register("DataLinks", typeof(ObservableCollection<DataLink>), typeof(Legend),
            new PropertyMetadata(new ObservableCollection<DataLink>(), OnDataLinksChanged));


        /// <summary>
        /// 图例方向。
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Legend), null);


        /// <summary>
        /// 图例文本下划线。
        /// </summary>
        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(Legend), null);


        /// <summary>
        /// 图例框底色是否可见。
        /// </summary>
        public Visibility BorderVisibility
        {
            get { return (Visibility)GetValue(BorderVisibilityProperty); }
            set { SetValue(BorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderVisibilityProperty =
            DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(Legend),
            new PropertyMetadata(Visibility.Collapsed));


        /// <summary>
        /// 图例边框线是否可见,默认可见。
        /// </summary>
        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(Legend),
            new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// 图例边框线颜色。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(Legend),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        /// <summary>
        /// 边框线样式。
        /// </summary>
        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(Legend),
            new PropertyMetadata(StrokeStyles.Solid));


        /// <summary>
        /// 边框线厚度。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(Legend),
            new PropertyMetadata((double)1));

        #endregion

        #region OnChanged static method

        private static void OnDataLinksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Legend;
            control.OnDataLinksChanged((ObservableCollection<DataLink>)e.OldValue, (ObservableCollection<DataLink>)e.NewValue);
        }

        #endregion OnChanged static method

        #region  OnChanged method

        private void OnDataLinksChanged(ObservableCollection<DataLink> oldValue, ObservableCollection<DataLink> newValue)
        {
            if (oldValue != null)
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(newValue_CollectionChanged);

            if (newValue != null)
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(newValue_CollectionChanged);

            this.Load();
        }

        void newValue_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Load();
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.AddItems(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.ClearItems();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.RemoveItems(e.OldItems);
            }

            //if (you.Love(Me) == 1 || you.Love(Me) == 0) { love = love; love++; love--; } //你爱，或者不爱我，爱就在那里，不增不减
        }

        #endregion OnChanged method

        internal Panel ItemsHost
        {
            get { return (Panel)GetValue(ItemsHostProperty); }
            private set { SetValue(ItemsHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ItemsHostProperty =
            DependencyProperty.Register("ItemsHost", typeof(Panel), typeof(Legend), null);
    }
}
