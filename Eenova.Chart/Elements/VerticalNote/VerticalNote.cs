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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using Eenova.Chart.Helpers;
using Eenova.Chart.Controls;
using Eenova.Chart.Setter;
using Eenova.Chart.Behaviors;

namespace Eenova.Chart.Elements
{
    public class VerticalNote : UIControl
    {
        bool _ignoreChanged;

        internal Axis Axis { get; set; }

        #region constructor

        public VerticalNote()
        {
            this.DefaultStyleKey = typeof(VerticalNote);

            this.InitItemsHost();
            this.SizeChanged += (s, e) => this.Load();
        }

        #endregion

        #region public method

        public void AddItem(double bindingValue, string content)
        {
            this.AddChild(bindingValue, content);
        }

        public void AddItem(DateTime bindingValue, string content)
        {
            this.AddChild(bindingValue, content);
        }

        public void AddItem(string bindingValue, string content)
        {
            this.AddChild(bindingValue, content);
        }

        public void Load()
        {
            foreach (OrientationTextBlock item in this.ItemsHost.Children)
            {
                this.CalculateChildPosition(item);
            }
            this.CaculateHostPosition();
        }

        #endregion

        #region protected method

        protected override string SettingTitle
        {
            get { return "设置标注样式"; }
        }

        protected override void LoadSetter()
        {
            SettingWindow.Add("字体", new CommonFontSetter { DataContext = this });
        }

        #endregion

        #region private method

        private void AddChild(object bindingValue, string content)
        {
            var item = new OrientationTextBlock
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = content,
                Tag = bindingValue
            };

            item.SizeChanged += (s, e) =>
            {
                this.CalculateChildPosition(item);
                this.CaculateHostPosition();
            };

            this.ItemsHost.Children.Add(item);

            this.BindingChild(item);
            this.CalculateChildPosition(item);
            this.CaculateHostPosition();
        }

        private void BindingChild(OrientationTextBlock item)
        {
            item.SetBinding(OrientationTextBlock.TextDecorationsProperty, new Binding("TextDecorations") { Source = this });
            item.SetBinding(OrientationTextBlock.BackgroundProperty, new Binding("Background") { Source = this });

            //item.SetBinding(OrientationTextBlock.ForegroundProperty, new Binding("Foreground") { Source = this });
            //item.SetBinding(OrientationTextBlock.FontFamilyProperty, new Binding("FontFamily") { Source = this });
            //item.SetBinding(OrientationTextBlock.FontSizeProperty, new Binding("FontSize") { Source = this });
            //item.SetBinding(OrientationTextBlock.FontStyleProperty, new Binding("FontStyle") { Source = this });
            //item.SetBinding(OrientationTextBlock.FontWeightProperty, new Binding("FontWeight") { Source = this });
        }

        private void CaculateHostPosition()
        {
            this.CalculateHostWidth();
            this.CalculateHostTransfrom();
        }

        private void CalculateHostWidth()
        {
            double width = 0;
            foreach (FrameworkElement item in this.ItemsHost.Children)
            {
                width = Math.Max(width, item.ActualWidth);
            }
            this.ItemsHost.Width = width;
        }

        private void CalculateHostTransfrom()
        {
            //throw new NotImplementedException();
            //this.X += 10;
            if (this.ActualWidth == 0)
                return;

            var offset = this.X + this.ItemsHost.ActualWidth;
            if (offset > this.ActualWidth)
                this.X = this.ActualWidth - this.ItemsHost.ActualWidth;
        }

        private void CalculateChildPosition(FrameworkElement item)
        {
            item.Visibility = Visibility.Collapsed;

            if (this.Axis == null)
                return;

            double position = double.NaN;
            try
            {
                position = this.Axis.Convert(new object[] { item.Tag })[0];
            }
            catch
            {
                return;
            }

            if (double.IsNaN(position) || position < 0 || position > this.ActualHeight)
                return;

            var top = this.CalculateTop(item, position);

            Canvas.SetTop(item, top);
            item.Visibility = Visibility.Visible;
        }

        private double CalculateTop(FrameworkElement item, double position)
        {
            return this.Axis.IsDesc ?
                position - item.ActualHeight / 2 :
                this.ActualHeight - position - item.ActualHeight / 2;
        }

        private void InitItemsHost()
        {
            this.ItemsHost = new Canvas
            {
                //Background = new SolidColorBrush(Colors.LightGray),
                RenderTransform = new CompositeTransform(),
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            var behavior = new MouseDragElementBehavior
            {
                DragDirection = DragDirection.X,
                ConstrainToParentBounds = true,
            };
            behavior.DragFinished += (s, e) => SyncX();
            behavior.Attach(this.ItemsHost);

            this.SyncTransform();
        }

        private void SyncX()
        {
            _ignoreChanged = true;
            this.X = ((CompositeTransform)this.ItemsHost.RenderTransform).TranslateX;
            _ignoreChanged = false;
        }

        private void SyncTransform()
        {
            ((CompositeTransform)this.ItemsHost.RenderTransform).TranslateX = this.X;
        }

        #endregion

        #region dp

        #region ItemsHost

        internal Panel ItemsHost
        {
            get { return (Panel)GetValue(ItemsHostProperty); }
            set { SetValue(ItemsHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsHost.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ItemsHostProperty =
            DependencyProperty.Register("ItemsHost", typeof(Panel), typeof(VerticalNote), null);

        #endregion

        #region TextDecorations

        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextDecorations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(VerticalNote), null);

        #endregion

        #region X

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(VerticalNote),
            new PropertyMetadata(10.0, OnXChanged));

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as VerticalNote;
            if (source._ignoreChanged)
            {
                source._ignoreChanged = false;
                return;
            }

            source.SyncTransform();
        }

        #endregion

        #endregion dp
    }
}
