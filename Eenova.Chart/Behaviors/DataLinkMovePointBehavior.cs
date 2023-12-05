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
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Eenova.Chart.Controls;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Behaviors
{
    public class DataLinkMovePointBehavior : Behavior<DataLink>
    {
        FrameworkElement _pointElement = null;
        double _mouseY = 0;
        public event EventHandler<DataLinkMovePointEventArgs> PointMoved;
        public event EventHandler<DataLinkMovePointEventArgs> PointMouseLeftButtonDown;
        public event EventHandler<DataLinkMovePointEventArgs> PointMoving;
        private bool _isCaptureMouse;
        public DataLinkMovePointBehavior()
        {
        }

        public bool IsCanMove { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
            this.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            this.AssociatedObject.MouseMove += new MouseEventHandler(AssociatedObject_MouseMove);
            this.AssociatedObject.MouseEnter += new MouseEventHandler(AssociatedObject_MouseEnter);
            this.AssociatedObject.MouseLeave += new MouseEventHandler(AssociatedObject_MouseLeave);
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            AssociatedObject.Cursor = Cursors.Arrow;
        }

        void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsCanMove)
                AssociatedObject.Cursor = Cursors.SizeNS;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            _pointElement = null;

            this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
            this.AssociatedObject.MouseLeftButtonUp -= new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            this.AssociatedObject.MouseMove -= new MouseEventHandler(AssociatedObject_MouseMove);
            this.AssociatedObject.MouseEnter -= new MouseEventHandler(AssociatedObject_MouseEnter);
            this.AssociatedObject.MouseLeave -= new MouseEventHandler(AssociatedObject_MouseLeave);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.AssociatedObject.AxisY == null ||
                this.AssociatedObject.AxisY.DataType != DataType.Numberic)
                return;

            _pointElement = e.OriginalSource as FrameworkElement;
            while (_pointElement != null && _pointElement != this.AssociatedObject)
            {
                if (_pointElement is EffectCircle || _pointElement is Mark)
                {
                    _mouseY = e.GetPosition(_pointElement).Y;
                    if (PointMouseLeftButtonDown != null)
                        PointMouseLeftButtonDown(AssociatedObject, new DataLinkMovePointEventArgs() { DataPoint = this.AssociatedObject.DataPoints[(int)_pointElement.Tag] });
                    this.AssociatedObject.CaptureMouse();
                    _isCaptureMouse = true;
                    //this.AssociatedObject.MouseMove += new MouseEventHandler(AssociatedObject_MouseMove);
                    //this.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
                    break;
                }
                _pointElement = VisualTreeHelper.GetParent(_pointElement) as FrameworkElement;
            }
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.AssociatedObject.ReleaseMouseCapture();
            _isCaptureMouse = false;
            if (PointMoved != null && IsCanMove)
            {
                try
                {
                    PointMoved(AssociatedObject, new DataLinkMovePointEventArgs() { DataPoint = this.AssociatedObject.DataPoints[(int)_pointElement.Tag] });
                }
                catch (Exception)
                {

                }
            }

            //this.AssociatedObject.MouseMove -= new MouseEventHandler(AssociatedObject_MouseMove);
            //this.AssociatedObject.MouseLeftButtonUp -= new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
        }

        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            //var areaY = e.GetPosition(this.AssociatedObject).Y;
            //if (areaY <= 0 || areaY >= this.AssociatedObject.ActualHeight)
            //    return;
            if (!_isCaptureMouse || !IsCanMove) return;
            var args = new DataLinkMovePointEventArgs() { DataPoint = this.AssociatedObject.DataPoints[(int)_pointElement.Tag] };
            var mouseY = e.GetPosition(_pointElement).Y;
            var offsetY = mouseY - _mouseY;
            var offsetValue = 0.0;
            if (this.AssociatedObject.AxisY.IsLogarithm)
            {
                //Math.Log(,2)
                //offsetValue = offsetY / this.AssociatedObject.ActualHeight * (this.AssociatedObject.AxisY.MaxData - this.AssociatedObject.AxisY.MinData);
                //////do logarithm
                //offsetValue= offsetY/this.AssociatedObject.ActualHeight
            }
            else
            {
                offsetValue = offsetY / this.AssociatedObject.ActualHeight * (this.AssociatedObject.AxisY.MaxData - this.AssociatedObject.AxisY.MinData);
            }
            var point = this.AssociatedObject.DataPoints[(int)_pointElement.Tag];
            args.OldValue = point.YValue.ToString();
            point.YValue = (double)point.YValue + offsetValue;

            if (this.AssociatedObject.AxisY.IsLogarithm)
            {
                var old = Math.Log((double)point.YValue, 2);
                var offset = offsetY / this.AssociatedObject.ActualHeight * (Math.Log(this.AssociatedObject.AxisY.MaxValue, 2) - Math.Log(this.AssociatedObject.AxisY.MinValue, 2));
                point.YValue = Math.Pow(2, old + offset);
            }

            if (PointMoving != null)
                PointMoving(AssociatedObject, args);
        }
    }
}
