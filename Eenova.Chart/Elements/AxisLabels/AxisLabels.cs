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


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Eenova.Chart.Elements
{
    public abstract class AxisLabels : ContentControl
    {
        IList<Border> _titleCache = new List<Border>();
        protected StackPanel _itemsHost = new StackPanel();

        internal Size LabelSize
        {
            get
            {
                return _itemsHost.Children.Count > 0 ? 
                    (_itemsHost.Children[0] as Border).Child.RenderSize : 
                    new Size();
            }
        }

        public AxisLabels()
        {
            this.Init();
        }

        private void Init()
        {
            this.Content = _itemsHost;
            this.VerticalAlignment = VerticalAlignment.Center;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.Source = new LabelItemCollection();

            this.Load();
        }

        private void Load()
        {
            if (this.Source == null)
            {
                _itemsHost.Children.Clear();
                return;
            }

            for (int i = 0; i < this.Source.Count; i++)
            {
                while (i >= _itemsHost.Children.Count)
                {
                    _itemsHost.Children.Add(this.CreateLabel(_itemsHost.Children.Count));
                }
                this.SetLabel((Border)_itemsHost.Children[i], this.Source[this.TransLabelIndex(i)]);
            }

            while (this.Source.Count < _itemsHost.Children.Count)
            {
                _itemsHost.Children.RemoveAt(_itemsHost.Children.Count - 1);
            }
        }

        private Border CreateLabel(int index)
        {
            if (index >= _titleCache.Count)
            {
                var title = new Title()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(0),
                };
                this.BindingLabel(title);
                _titleCache.Add(new Border { Child = title });
            }
            return _titleCache[index];
        }

        private void BindingLabel(Title item)
        {
            item.SetBinding(Title.ForegroundProperty, new Binding("Foreground") { Source = this });
            item.SetBinding(Title.BackgroundProperty, new Binding("Background") { Source = this });
            item.SetBinding(Title.OrientationProperty, new Binding("Orientation") { Source = this });
            item.SetBinding(Title.TextDecorationsProperty, new Binding("TextDecorations") { Source = this });
            item.SetBinding(Title.TextRotationProperty, new Binding("TextRotation") { Source = this });
            item.SetBinding(Title.FontFamilyProperty, new Binding("FontFamily") { Source = this });
            item.SetBinding(Title.FontSizeProperty, new Binding("FontSize") { Source = this });
            item.SetBinding(Title.FontStyleProperty, new Binding("FontStyle") { Source = this });
            item.SetBinding(Title.FontWeightProperty, new Binding("FontWeight") { Source = this });
            item.SetBinding(Title.HorizontalContentAlignmentProperty, new Binding("HorizontalContentAlignment") { Source = this });
            item.SetBinding(Title.VerticalContentAlignmentProperty, new Binding("VerticalContentAlignment") { Source = this });
            item.SetBinding(Title.TextRenderTransformOriginProperty, new Binding("TextRenderTransformOrigin") { Source = this });
        }

        private void SetLabel(Border item, LabelItem label)
        {
            (item.Child as Title).Text = label.Label;
            this.SetLabelSize(item, label.Interval);
        }

        protected abstract int TransLabelIndex(int index);
        protected abstract void SetLabelSize(FrameworkElement label, double interval);

        #region Labels

        /// <summary>
        /// 包含的标签。
        /// </summary>
        internal LabelItemCollection Source
        {
            get { return (LabelItemCollection)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(LabelItemCollection), typeof(AxisLabels),
            new PropertyMetadata(new LabelItemCollection(), OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisLabels;
            source.Load();
        }

        #endregion Labels

        #region IsDesc

        /// <summary>
        /// 是否逆序。
        /// </summary>
        internal bool IsDesc
        {
            get { return (bool)GetValue(IsDescProperty); }
            set { SetValue(IsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDesc.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty IsDescProperty =
            DependencyProperty.Register("IsDesc", typeof(bool), typeof(AxisLabels),
            new PropertyMetadata(OnIsDescChanged));

        private static void OnIsDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as AxisLabels;
            source.Load();
        }

        #endregion IsDesc

        #region title

        /// <summary>
        /// 旋转角。
        /// </summary>
        public double TextRotation
        {
            get { return (double)GetValue(TextRotationProperty); }
            set { SetValue(TextRotationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rotation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextRotationProperty =
            DependencyProperty.Register("TextRotation", typeof(double), typeof(AxisLabels),
            new PropertyMetadata((double)0));

        /// <summary>
        /// 文本竖直或水平。
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(AxisLabels),
            new PropertyMetadata(Orientation.Horizontal));


        /// <summary>
        /// 下划线。
        /// </summary>
        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextDecorations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(AxisLabels), null);





        public Point TextRenderTransformOrigin
        {
            get { return (Point)GetValue(TextRenderTransformOriginProperty); }
            set { SetValue(TextRenderTransformOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextRenderTransformOrigin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextRenderTransformOriginProperty =
            DependencyProperty.Register("TextRenderTransformOrigin", typeof(Point), typeof(AxisLabels),
            new PropertyMetadata(new Point(0.5, 0.5)));




        #endregion

    }
}
