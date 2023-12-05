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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Eenova.Chart.Behaviors
{
    public class MouseDragElementBehavior : Behavior<FrameworkElement>
    {
        // Fields
        private Transform cachedRenderTransform;
        public static readonly DependencyProperty ConstrainToParentBoundsProperty =
            DependencyProperty.Register("ConstrainToParentBounds", typeof(bool), typeof(MouseDragElementBehavior),
            new PropertyMetadata(false, new PropertyChangedCallback(MouseDragElementBehavior.OnConstrainToParentBoundsChanged)));
        private MouseEventHandler dragBegun;
        private MouseEventHandler dragFinished;
        private MouseEventHandler dragging;
        private Point relativePosition;
        private bool settingPosition;
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(MouseDragElementBehavior),
            new PropertyMetadata((double)1.0 / (double)0.0, new PropertyChangedCallback(MouseDragElementBehavior.OnXChanged)));
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(MouseDragElementBehavior),
            new PropertyMetadata((double)1.0 / (double)0.0, new PropertyChangedCallback(MouseDragElementBehavior.OnYChanged)));

        // Events
        public event MouseEventHandler DragBegun
        {
            add
            {
                MouseEventHandler handler2;
                MouseEventHandler dragBegun = this.dragBegun;
                do
                {
                    handler2 = dragBegun;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Combine(handler2, value);
                    dragBegun = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragBegun, handler3, handler2);
                }
                while (dragBegun != handler2);
            }
            remove
            {
                MouseEventHandler handler2;
                MouseEventHandler dragBegun = this.dragBegun;
                do
                {
                    handler2 = dragBegun;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Remove(handler2, value);
                    dragBegun = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragBegun, handler3, handler2);
                }
                while (dragBegun != handler2);
            }
        }

        public event MouseEventHandler DragFinished
        {
            add
            {
                MouseEventHandler handler2;
                MouseEventHandler dragFinished = this.dragFinished;
                do
                {
                    handler2 = dragFinished;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Combine(handler2, value);
                    dragFinished = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragFinished, handler3, handler2);
                }
                while (dragFinished != handler2);
            }
            remove
            {
                MouseEventHandler handler2;
                MouseEventHandler dragFinished = this.dragFinished;
                do
                {
                    handler2 = dragFinished;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Remove(handler2, value);
                    dragFinished = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragFinished, handler3, handler2);
                }
                while (dragFinished != handler2);
            }
        }

        public event MouseEventHandler Dragging
        {
            add
            {
                MouseEventHandler handler2;
                MouseEventHandler dragging = this.dragging;
                do
                {
                    handler2 = dragging;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Combine(handler2, value);
                    dragging = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragging, handler3, handler2);
                }
                while (dragging != handler2);
            }
            remove
            {
                MouseEventHandler handler2;
                MouseEventHandler dragging = this.dragging;
                do
                {
                    handler2 = dragging;
                    MouseEventHandler handler3 = (MouseEventHandler)Delegate.Remove(handler2, value);
                    dragging = Interlocked.CompareExchange<MouseEventHandler>(ref this.dragging, handler3, handler2);
                }
                while (dragging != handler2);
            }
        }

        // Methods
        private void ApplyTranslation(double x, double y)
        {
            if (this.ParentElement != null)
            {
                Point point = TransformAsVector(this.RootElement.TransformToVisual(this.ParentElement), x, y);
                x = point.X;
                y = point.Y;
                if (this.ConstrainToParentBounds)
                {
                    FrameworkElement parentElement = this.ParentElement;
                    Rect rect = new Rect(0.0, 0.0, parentElement.ActualWidth, parentElement.ActualHeight);
                    GeneralTransform transform2 = base.AssociatedObject.TransformToVisual(parentElement);
                    Rect elementBounds = this.ElementBounds;
                    Rect rect3 = transform2.TransformBounds(elementBounds);
                    rect3.X += x;
                    rect3.Y += y;
                    if (!RectContainsRect(rect, rect3))
                    {
                        if (rect3.X < rect.Left)
                        {
                            double num = rect3.X - rect.Left;
                            x -= num;
                        }
                        else if (rect3.Right > rect.Right)
                        {
                            double num2 = rect3.Right - rect.Right;
                            x -= num2;
                        }
                        if (rect3.Y < rect.Top)
                        {
                            double num3 = rect3.Y - rect.Top;
                            y -= num3;
                        }
                        else if (rect3.Bottom > rect.Bottom)
                        {
                            double num4 = rect3.Bottom - rect.Bottom;
                            y -= num4;
                        }
                    }
                }
                this.ApplyTranslationTransform(x, y);
            }
        }

        internal void ApplyTranslationTransform(double x, double y)
        {
            Transform renderTransform = this.RenderTransform;
            TranslateTransform transform2 = renderTransform as TranslateTransform;
            if (transform2 == null)
            {
                TransformGroup group = renderTransform as TransformGroup;
                MatrixTransform transform3 = renderTransform as MatrixTransform;
                CompositeTransform transform4 = renderTransform as CompositeTransform;
                if (transform4 != null)
                {
                    transform4.TranslateX += x;
                    transform4.TranslateY += y;
                    return;
                }
                if (group != null)
                {
                    if (group.Children.Count > 0)
                    {
                        transform2 = group.Children[group.Children.Count - 1] as TranslateTransform;
                    }
                    if (transform2 == null)
                    {
                        transform2 = new TranslateTransform();
                        group.Children.Add(transform2);
                    }
                }
                else
                {
                    if (transform3 != null)
                    {
                        Matrix matrix = transform3.Matrix;
                        matrix.OffsetX += x;
                        matrix.OffsetY += y;
                        MatrixTransform transform5 = new MatrixTransform();
                        transform5.Matrix = matrix;
                        this.RenderTransform = transform5;
                        return;
                    }
                    TransformGroup group2 = new TransformGroup();
                    transform2 = new TranslateTransform();
                    if (renderTransform != null)
                    {
                        group2.Children.Add(renderTransform);
                    }
                    group2.Children.Add(transform2);
                    this.RenderTransform = group2;
                }
            }
            transform2.X += x;
            transform2.Y += y;
        }

        internal static Transform CloneTransform(Transform transform)
        {
            ScaleTransform transform2 = null;
            RotateTransform transform3 = null;
            SkewTransform transform4 = null;
            TranslateTransform transform5 = null;
            MatrixTransform transform6 = null;
            TransformGroup group = null;
            if (transform != null)
            {
                transform.GetType();
                transform2 = transform as ScaleTransform;
                if (transform2 != null)
                {
                    ScaleTransform transform7 = new ScaleTransform();
                    transform7.CenterX = transform2.CenterX;
                    transform7.CenterY = transform2.CenterY;
                    transform7.ScaleX = transform2.ScaleX;
                    transform7.ScaleY = transform2.ScaleY;
                    return transform7;
                }
                transform3 = transform as RotateTransform;
                if (transform3 != null)
                {
                    RotateTransform transform8 = new RotateTransform();
                    transform8.Angle = transform3.Angle;
                    transform8.CenterX = transform3.CenterX;
                    transform8.CenterY = transform3.CenterY;
                    return transform8;
                }
                transform4 = transform as SkewTransform;
                if (transform4 != null)
                {
                    SkewTransform transform9 = new SkewTransform();
                    transform9.AngleX = transform4.AngleX;
                    transform9.AngleY = transform4.AngleY;
                    transform9.CenterX = transform4.CenterX;
                    transform9.CenterY = transform4.CenterY;
                    return transform9;
                }
                transform5 = transform as TranslateTransform;
                if (transform5 != null)
                {
                    TranslateTransform transform10 = new TranslateTransform();
                    transform10.X = transform5.X;
                    transform10.Y = transform5.Y;
                    return transform10;
                }
                transform6 = transform as MatrixTransform;
                if (transform6 != null)
                {
                    MatrixTransform transform11 = new MatrixTransform();
                    transform11.Matrix = transform6.Matrix;
                    return transform11;
                }
                group = transform as TransformGroup;
                if (group != null)
                {
                    TransformGroup group2 = new TransformGroup();
                    foreach (Transform transform12 in group.Children)
                    {
                        group2.Children.Add(CloneTransform(transform12));
                    }
                    return group2;
                }
                CompositeTransform transform13 = null;
                transform13 = transform as CompositeTransform;
                if (transform13 != null)
                {
                    CompositeTransform transform15 = new CompositeTransform();
                    transform15.CenterX = transform13.CenterX;
                    transform15.CenterY = transform13.CenterY;
                    transform15.Rotation = transform13.Rotation;
                    transform15.ScaleX = transform13.ScaleX;
                    transform15.ScaleY = transform13.ScaleY;
                    transform15.SkewX = transform13.SkewX;
                    transform15.SkewY = transform13.SkewY;
                    transform15.TranslateX = transform13.TranslateX;
                    transform15.TranslateY = transform13.TranslateY;
                    return transform15;
                }
            }
            return null;
        }

        internal void EndDrag()
        {
            base.AssociatedObject.MouseMove -= new MouseEventHandler(this.OnMouseMove);
            base.AssociatedObject.LostMouseCapture -= new MouseEventHandler(this.OnLostMouseCapture);
            base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonUp));
        }

        private static Point GetTransformOffset(GeneralTransform transform)
        {
            return transform.Transform(new Point(0.0, 0.0));
        }

        internal void HandleDrag(Point newPositionInElementCoordinates)
        {
            double x = DragDirection == DragDirection.Y || DragDirection == DragDirection.None ?
                0 : newPositionInElementCoordinates.X - this.relativePosition.X;
            double y = DragDirection == DragDirection.X || DragDirection == DragDirection.None ?
                0 : newPositionInElementCoordinates.Y - this.relativePosition.Y;
            Point point = TransformAsVector(base.AssociatedObject.TransformToVisual(this.RootElement), x, y);
            this.settingPosition = true;
            this.ApplyTranslation(point.X, point.Y);
            this.UpdatePosition();
            this.settingPosition = false;
        }

        protected override void OnAttached()
        {
            base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
        }

        private static void OnConstrainToParentBoundsChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            MouseDragElementBehavior behavior = (MouseDragElementBehavior)sender;
            behavior.UpdatePosition(new Point(behavior.X, behavior.Y));
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            this.EndDrag();
            if (this.dragFinished != null)
            {
                this.dragFinished(this, e);
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.StartDrag(e.GetPosition(base.AssociatedObject));
            if (this.dragBegun != null)
            {
                this.dragBegun(this, e);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            this.HandleDrag(e.GetPosition(base.AssociatedObject));
            if (this.dragging != null)
            {
                this.dragging(this, e);
            }
        }

        private static void OnXChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            MouseDragElementBehavior behavior = (MouseDragElementBehavior)sender;
            behavior.UpdatePosition(new Point((double)args.NewValue, behavior.Y));
        }

        private static void OnYChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            MouseDragElementBehavior behavior = (MouseDragElementBehavior)sender;
            behavior.UpdatePosition(new Point(behavior.X, (double)args.NewValue));
        }

        private static bool RectContainsRect(Rect rect1, Rect rect2)
        {
            if (rect1.IsEmpty || rect2.IsEmpty)
            {
                return false;
            }
            return ((((rect1.X <= rect2.X) && (rect1.Y <= rect2.Y)) && ((rect1.X + rect1.Width) >= (rect2.X + rect2.Width))) && ((rect1.Y + rect1.Height) >= (rect2.Y + rect2.Height)));
        }

        internal void StartDrag(Point positionInElementCoordinates)
        {
            this.relativePosition = positionInElementCoordinates;
            base.AssociatedObject.CaptureMouse();
            base.AssociatedObject.MouseMove += new MouseEventHandler(this.OnMouseMove);
            base.AssociatedObject.LostMouseCapture += new MouseEventHandler(this.OnLostMouseCapture);
            base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonUp), false);
        }

        private static Point TransformAsVector(GeneralTransform transform, double x, double y)
        {
            Point point = transform.Transform(new Point(0.0, 0.0));
            Point point2 = transform.Transform(new Point(x, y));
            return new Point(point2.X - point.X, point2.Y - point.Y);
        }

        private void UpdatePosition()
        {
            Point transformOffset = GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
            this.X = transformOffset.X;
            this.Y = transformOffset.Y;
        }

        private void UpdatePosition(Point point)
        {
            if (!this.settingPosition && (base.AssociatedObject != null))
            {
                Point transformOffset = GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
                double x = double.IsNaN(point.X) ? 0.0 : (point.X - transformOffset.X);
                double y = double.IsNaN(point.Y) ? 0.0 : (point.Y - transformOffset.Y);
                this.ApplyTranslation(x, y);
            }
        }

        // Properties
        private Point ActualPosition
        {
            get
            {
                Point transformOffset = GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
                return new Point(transformOffset.X, transformOffset.Y);
            }
        }

        public bool ConstrainToParentBounds
        {
            get
            {
                return (bool)base.GetValue(ConstrainToParentBoundsProperty);
            }
            set
            {
                base.SetValue(ConstrainToParentBoundsProperty, value);
            }
        }

        private Rect ElementBounds
        {
            get
            {
                Rect layoutRect = GetLayoutRect(base.AssociatedObject);
                return new Rect(new Point(0.0, 0.0), new Size(layoutRect.Width, layoutRect.Height));
            }
        }

        private FrameworkElement ParentElement
        {
            get
            {
                return (base.AssociatedObject.Parent as FrameworkElement);
            }
        }

        private Transform RenderTransform
        {
            get
            {
                if ((this.cachedRenderTransform == null) || !object.ReferenceEquals(this.cachedRenderTransform, base.AssociatedObject.RenderTransform))
                {
                    Transform transform = CloneTransform(base.AssociatedObject.RenderTransform);
                    this.RenderTransform = transform;
                }
                return this.cachedRenderTransform;
            }
            set
            {
                if (this.cachedRenderTransform != value)
                {
                    this.cachedRenderTransform = value;
                    base.AssociatedObject.RenderTransform = value;
                }
            }
        }

        private UIElement RootElement
        {
            get
            {
                DependencyObject associatedObject = base.AssociatedObject;
                for (DependencyObject obj3 = associatedObject; obj3 != null; obj3 = VisualTreeHelper.GetParent(associatedObject))
                {
                    associatedObject = obj3;
                }
                return (associatedObject as UIElement);
            }
        }

        public double X
        {
            get
            {
                return (double)base.GetValue(XProperty);
            }
            set
            {
                base.SetValue(XProperty, value);
            }
        }

        public double Y
        {
            get
            {
                return (double)base.GetValue(YProperty);
            }
            set
            {
                base.SetValue(YProperty, value);
            }
        }

        private Rect GetLayoutRect(FrameworkElement element)
        {
            double actualWidth = element.ActualWidth;
            double actualHeight = element.ActualHeight;
            if ((element is Image) || (element is MediaElement))
            {
                if (element.Parent.GetType() == typeof(Canvas))
                {
                    actualWidth = double.IsNaN(element.Width) ? actualWidth : element.Width;
                    actualHeight = double.IsNaN(element.Height) ? actualHeight : element.Height;
                }
                else
                {
                    actualWidth = element.RenderSize.Width;
                    actualHeight = element.RenderSize.Height;
                }
            }
            actualWidth = (element.Visibility == Visibility.Collapsed) ? 0.0 : actualWidth;
            actualHeight = (element.Visibility == Visibility.Collapsed) ? 0.0 : actualHeight;
            Thickness margin = element.Margin;
            Rect layoutSlot = LayoutInformation.GetLayoutSlot(element);
            if (element.Parent is Canvas)
            {
                layoutSlot = new Rect(Canvas.GetLeft(element), Canvas.GetTop(element), actualWidth, actualHeight);
            }
            double x = 0.0;
            double y = 0.0;
            switch (element.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = layoutSlot.Left + margin.Left;
                    break;

                case HorizontalAlignment.Center:
                    x = ((((layoutSlot.Left + margin.Left) + layoutSlot.Right) - margin.Right) / 2.0) - (actualWidth / 2.0);
                    break;

                case HorizontalAlignment.Right:
                    x = (layoutSlot.Right - margin.Right) - actualWidth;
                    break;

                case HorizontalAlignment.Stretch:
                    x = Math.Max((double)(layoutSlot.Left + margin.Left), (double)(((((layoutSlot.Left + margin.Left) + layoutSlot.Right) - margin.Right) / 2.0) - (actualWidth / 2.0)));
                    break;
            }
            switch (element.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = layoutSlot.Top + margin.Top;
                    break;

                case VerticalAlignment.Center:
                    y = ((((layoutSlot.Top + margin.Top) + layoutSlot.Bottom) - margin.Bottom) / 2.0) - (actualHeight / 2.0);
                    break;

                case VerticalAlignment.Bottom:
                    y = (layoutSlot.Bottom - margin.Bottom) - actualHeight;
                    break;

                case VerticalAlignment.Stretch:
                    y = Math.Max((double)(layoutSlot.Top + margin.Top), (double)(((((layoutSlot.Top + margin.Top) + layoutSlot.Bottom) - margin.Bottom) / 2.0) - (actualHeight / 2.0)));
                    break;
            }
            return new Rect(x, y, actualWidth, actualHeight);
        }

        //public bool IsOnlyMoveY
        //{
        //    get { return (bool)GetValue(IsOnlyMoveYProperty); }
        //    set { SetValue(IsOnlyMoveYProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsOnlyMoveY.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsOnlyMoveYProperty =
        //    DependencyProperty.Register("IsOnlyMoveY", typeof(bool), typeof(MouseDragElementBehavior),
        //    new PropertyMetadata(false));



        public DragDirection DragDirection
        {
            get { return (DragDirection)GetValue(DragDirectionProperty); }
            set { SetValue(DragDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DragDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragDirectionProperty =
            DependencyProperty.Register("DragDirection", typeof(DragDirection), typeof(MouseDragElementBehavior),
            new PropertyMetadata(DragDirection.Both));


    }
}
