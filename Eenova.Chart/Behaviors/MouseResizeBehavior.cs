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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Eenova.Chart.Behaviors
{
    public class MouseResizeBehavior : Behavior<FrameworkElement>
    {
        private enum ControlParts
        {
            None,
            Left,
            Right,
            Top,
            Bottom,
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom,
        }

        public event EventHandler BeginResize;
        public event EventHandler EndResize;

        const double DRAG_SAPCE = 5;
        ControlParts _resizePart;
        bool _isResizeing;
        double _totalLeft;
        double _totalTop;

        public MouseResizeBehavior()
        {
            // 在此点下面插入创建对象所需的代码。

            //
            // 下面的代码行用于在命令
            // 与要调用的函数之间建立关系。如果您选择
            // 使用 MyFunction 和 MyCommand 的已注释掉的版本，而不是创建自己的实现，
            // 请取消注释以下行并添加对 Microsoft.Expression.Interactions 的引用。
            //
            // 文档将向您提供简单命令实现的示例，
            // 您可以使用该示例，而不是使用 ActionCommand 并引用 Interactions 程序集。
            //
            //this.MyCommand = new ActionCommand(this.MyFunction);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            // 插入要在将 Behavior 附加到对象时运行的代码。
            base.AssociatedObject.MouseMove += new MouseEventHandler(OnMouseMove);
            //base.AssociatedObject.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
            //base.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(OnMouseLeftButtonUp);
            base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
            base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), true);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // 插入要在从对象中删除 Behavior 时运行的代码。
            base.AssociatedObject.MouseMove -= new MouseEventHandler(OnMouseMove);
            //base.AssociatedObject.MouseLeftButtonDown -= new MouseButtonEventHandler(OnMouseLeftButtonDown);
            //base.AssociatedObject.MouseLeftButtonUp -= new MouseButtonEventHandler(OnMouseLeftButtonUp);
            base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
            base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp));

        }

        int i = 0;

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizeing)
            {
                if (i < 3)
                {
                    i++;
                    System.Diagnostics.Debug.WriteLine(i);
                }
                else
                {
                    i = 0;
                    this.Resize(e.GetPosition(base.AssociatedObject));
                }
            }
            else
            {
                _resizePart = this.GetResizePart(e.GetPosition(base.AssociatedObject));
                this.SetCursor(_resizePart);
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_resizePart != ControlParts.None)
            {
                base.AssociatedObject.CaptureMouse(); 
                _totalLeft = Canvas.GetLeft(base.AssociatedObject) + base.AssociatedObject.ActualWidth;
                _totalTop = Canvas.GetTop(base.AssociatedObject) + base.AssociatedObject.ActualHeight;
                _isResizeing = true;
                this.OnBeginResize();
                e.Handled = true;
            }
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizeing)
            {
                i = 0;
                //this.Resize(e.GetPosition(base.AssociatedObject));
                base.AssociatedObject.ReleaseMouseCapture();
                _isResizeing = false;
                this.OnEndResize();
            }
        }

        private void Resize(Point insideMousePosition)
        {
            ResizeCanvas(insideMousePosition);
        }

        private void ResizeMargin(Point insideMousePosition)
        {

        }

        private void ResizeCanvas(Point insideMousePosition)
        {
            base.AssociatedObject.MinWidth = 20;
            base.AssociatedObject.MinHeight = 20;
            var width = base.AssociatedObject.ActualWidth;
            var height = base.AssociatedObject.ActualHeight;
            bool toSetTop = false;
            bool toSetLeft = false;
            switch (_resizePart)
            {
                case ControlParts.Left:
                case ControlParts.LeftTop:
                case ControlParts.LeftBottom:
                    width -= width == 0 && insideMousePosition.X > 0 ? 0 : insideMousePosition.X;
                    toSetLeft = true;
                    break;
                case ControlParts.Right:
                case ControlParts.RightTop:
                case ControlParts.RightBottom:
                    width = insideMousePosition.X;
                    break;
            }
            base.AssociatedObject.Width = width = Math.Max(base.AssociatedObject.MinWidth, width);
            if (toSetLeft) Canvas.SetLeft(base.AssociatedObject, _totalLeft - width);

            switch (_resizePart)
            {
                case ControlParts.Top:
                case ControlParts.LeftTop:
                case ControlParts.RightTop:
                    height -= height == 0 && insideMousePosition.Y > 0 ? 0 : insideMousePosition.Y;
                    toSetTop = true;
                    break;
                case ControlParts.Bottom:
                case ControlParts.LeftBottom:
                case ControlParts.RightBottom:
                    height = insideMousePosition.Y;
                    break;
            }
            base.AssociatedObject.Height = height = Math.Max(base.AssociatedObject.MinHeight, height);
            if (toSetTop) Canvas.SetTop(base.AssociatedObject, _totalTop - height);
        }

        private ControlParts GetResizePart(Point mousePosition)
        {
            var size = new Size(base.AssociatedObject.ActualWidth, base.AssociatedObject.ActualHeight);
            var center = new Point(size.Width / 2, size.Height / 2);

            if (IsRightBottomOnly == false)
            {
                if (mousePosition.X < center.X && mousePosition.X < DRAG_SAPCE &&
                    mousePosition.Y < center.Y && mousePosition.Y < DRAG_SAPCE)
                    return ControlParts.LeftTop;

                if (mousePosition.X > center.X && mousePosition.X > size.Width - DRAG_SAPCE &&
                    mousePosition.Y < center.Y && mousePosition.Y < DRAG_SAPCE)
                    return ControlParts.RightTop;
            }

            if (mousePosition.X > center.X && mousePosition.X > size.Width - DRAG_SAPCE &&
                mousePosition.Y > center.Y && mousePosition.Y > size.Height - DRAG_SAPCE)
                return ControlParts.RightBottom;

            if (IsRightBottomOnly == false)
            {
                if (mousePosition.X < center.X && mousePosition.X < DRAG_SAPCE &&
                    mousePosition.Y > center.Y && mousePosition.Y > size.Height - DRAG_SAPCE)
                    return ControlParts.LeftBottom;
            }

            if (IsRightBottomOnly == false)
            {
                if (mousePosition.X < center.X && mousePosition.X < DRAG_SAPCE)
                    return ControlParts.Left;

                if (mousePosition.Y < center.Y && mousePosition.Y < DRAG_SAPCE)
                    return ControlParts.Top;
            }

            if (mousePosition.X > center.X && mousePosition.X > size.Width - DRAG_SAPCE)
                return ControlParts.Right;

            if (mousePosition.Y > center.Y && mousePosition.Y > size.Height - DRAG_SAPCE)
                return ControlParts.Bottom;

            return ControlParts.None;
        }

        private void SetCursor(ControlParts part)
        {
            switch (part)
            {
                case ControlParts.None:
                    this.AssociatedObject.Cursor = Cursors.Arrow;
                    break;
                case ControlParts.Top:
                case ControlParts.Bottom:
                    this.AssociatedObject.Cursor = Cursors.SizeNS;
                    break;
                case ControlParts.LeftTop:
                case ControlParts.RightBottom:
                    this.AssociatedObject.Cursor = Cursors.SizeNWSE;
                    break;
                case ControlParts.RightTop:
                case ControlParts.LeftBottom:
                    this.AssociatedObject.Cursor = Cursors.SizeNESW;
                    break;
                case ControlParts.Right:
                case ControlParts.Left:
                    this.AssociatedObject.Cursor = Cursors.SizeWE;
                    break;
            }
        }

        private void OnBeginResize()
        {
            var handle = this.BeginResize;
            if (handle != null)
                handle(this.AssociatedObject, null);
        }

        private void OnEndResize()
        {
            var handle = this.EndResize;
            if (handle != null)
                handle(this.AssociatedObject, null);
        }

        public bool IsRightBottomOnly
        {
            get { return (bool)GetValue(IsRightBottomOnlyProperty); }
            set { SetValue(IsRightBottomOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRightBottomOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRightBottomOnlyProperty =
            DependencyProperty.Register("IsRightBottomOnly", typeof(bool), typeof(MouseResizeBehavior),
            new PropertyMetadata(false));
    }
}
