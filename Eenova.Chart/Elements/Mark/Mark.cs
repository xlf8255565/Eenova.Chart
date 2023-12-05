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


using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Factories;

namespace Eenova.Chart.Elements
{
    public class Mark : Control
    {
        public Mark()
        {
            this.DefaultStyleKey = typeof(Mark);
            this.Init();
        }

        private void Init()
        {
            this.ItemHost = new Border()
            {
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new CompositeTransform()
            };
            this.LoadShape();
            this.SetScaleX();
            this.SetScaleY();
        }

        private void LoadShape()
        {
            var shape = ShapeFactory.Create(this.MarkType);
            shape.SetBinding(Shape.FillProperty, new Binding("Foreground") { Source = this });
            shape.SetBinding(Shape.WidthProperty, new Binding("Width") { Source = this });
            shape.SetBinding(Shape.HeightProperty, new Binding("Height") { Source = this });
            this.ItemHost.Child = shape;
        }

        private void SetScaleX()
        {
            ((CompositeTransform)this.ItemHost.RenderTransform).ScaleX = this.IsXDesc ? -1 : 1;
        }

        private void SetScaleY()
        {
            ((CompositeTransform)this.ItemHost.RenderTransform).ScaleY = this.IsYDesc ? 1 : -1;
        }

        #region dp

        #region ItemHost

        internal Border ItemHost
        {
            get { return (Border)GetValue(ItemHostProperty); }
            private set { SetValue(ItemHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemHost.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ItemHostProperty =
            DependencyProperty.Register("ItemHost", typeof(Border), typeof(Mark), null);

        #endregion ItemHost

        #region MarkType

        public ShapeType MarkType
        {
            get { return (ShapeType)GetValue(MarkTypeProperty); }
            set { SetValue(MarkTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShapeType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkTypeProperty =
            DependencyProperty.Register("MarkType", typeof(ShapeType), typeof(Mark),
            new PropertyMetadata(ShapeType.Circle, OnMarkTypeChanged));

        private static void OnMarkTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Mark;
            source.LoadShape();
        }

        #endregion MarkType

        #region IsXDesc

        public bool IsXDesc
        {
            get { return (bool)GetValue(IsXDescProperty); }
            set { SetValue(IsXDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsXDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsXDescProperty =
            DependencyProperty.Register("IsXDesc", typeof(bool), typeof(Mark),
            new PropertyMetadata(false, OnIsXDescChanged));

        private static void OnIsXDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Mark;
            source.SetScaleX();
        }

        #endregion IsXDesc

        #region IsYDesc

        public bool IsYDesc
        {
            get { return (bool)GetValue(IsYDescProperty); }
            set { SetValue(IsYDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsYDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsYDescProperty =
            DependencyProperty.Register("IsYDesc", typeof(bool), typeof(Mark),
            new PropertyMetadata(true, OnIsYDescChanged));

        private static void OnIsYDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as Mark;
            source.SetScaleY();
        }

        #endregion IsYDesc

        #endregion dp

    }
}
