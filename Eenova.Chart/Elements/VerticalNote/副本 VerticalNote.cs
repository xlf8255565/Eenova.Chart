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
        internal Axis Axis { get; set; }

        #region constructor

        public VerticalNote()
        {
            this.DefaultStyleKey = typeof(VerticalNote);

            this.InitItemsHost();
            this.Binding();
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
            this.CalculateHostWidth();
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
            item.SizeChanged += (s, e) => this.CalculateChildPosition(item);

            this.ItemsHost.Children.Add(item);

            this.BindingChild(item);
            this.CalculateChildPosition(item);

            this.CalculateHostWidth();
        }

        private void Binding()
        {
            //this.SetBinding(VerticalNote.InternalFontFamilyProperty, new Binding("FontFamily") { Source = this });
            //this.SetBinding(VerticalNote.InternalFontSizeProperty, new Binding("FontSize") { Source = this });
            //this.SetBinding(VerticalNote.InternalFontStyleProperty, new Binding("FontStyle") { Source = this });
            //this.SetBinding(VerticalNote.InternalFontWeightProperty, new Binding("FontWeight") { Source = this });
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

        private void CalculateHostWidth()
        {
            double width = 0;
            foreach (FrameworkElement item in this.ItemsHost.Children)
            {
                width = Math.Max(width, item.ActualWidth);
            }
            this.ItemsHost.Width = width;
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

            var top = position - item.ActualHeight / 2;
            //var top = value - 10;
            //if (!this.Axis.IsDesc)
            //    top = this.ActualHeight - top;

            Canvas.SetTop(item, top);
            item.Visibility = Visibility.Visible;
        }

        private void InitItemsHost()
        {
            this.ItemsHost = new Canvas
            {
                Background = new SolidColorBrush(Colors.LightGray),
                RenderTransform = new CompositeTransform(),
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            var behavior = new MouseDragElementBehavior
            {
                DragDirection = DragDirection.X,
                ConstrainToParentBounds = true,
            };
            behavior.Attach(this.ItemsHost);
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

        #region Font

        #region InternalFontFamily

        internal FontFamily InternalFontFamily
        {
            get { return (FontFamily)GetValue(InternalFontFamilyProperty); }
            set { SetValue(InternalFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalFontFamily.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalFontFamilyProperty =
            DependencyProperty.Register("InternalFontFamily", typeof(FontFamily), typeof(VerticalNote), new PropertyMetadata(OnTextSizeChanged));

        #endregion

        #region InternalFontSize

        internal double InternalFontSize
        {
            get { return (double)GetValue(InternalFontSizeProperty); }
            set { SetValue(InternalFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalFontSize.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalFontSizeProperty =
            DependencyProperty.Register("InternalFontSize", typeof(double), typeof(VerticalNote), new PropertyMetadata(OnTextSizeChanged));

        #endregion

        #region InternalFontStyle

        internal FontStyle InternalFontStyle
        {
            get { return (FontStyle)GetValue(InternalFontStyleProperty); }
            set { SetValue(InternalFontStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalFontStyle.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalFontStyleProperty =
            DependencyProperty.Register("InternalFontStyle", typeof(FontStyle), typeof(VerticalNote), new PropertyMetadata(OnTextSizeChanged));

        #endregion

        #region InternalFontWeight

        internal FontWeight InternalFontWeight
        {
            get { return (FontWeight)GetValue(InternalFontWeightProperty); }
            set { SetValue(InternalFontWeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalFontWeight.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty InternalFontWeightProperty =
            DependencyProperty.Register("InternalFontWeight", typeof(FontWeight), typeof(VerticalNote), new PropertyMetadata(OnTextSizeChanged));

        #endregion

        #region TextDecorations

        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextDecorations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(VerticalNote), new PropertyMetadata(OnTextSizeChanged));

        #endregion

        private static void OnTextSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as VerticalNote;
            source.Load();
        }

        #endregion

        #endregion dp
    }
}
