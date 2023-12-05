using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Controls;
using Eenova.Chart.Interfaces;
using Eenova.Chart.Settings;

namespace Eenova.Chart.Elements
{
    public class GridLine : Canvas, IStroke
    {
        #region field

        GridControl _root;
        Canvas _selectedUI;
        CompositeTransform _transform;
        List<double> _intervals;

        #endregion field

        public GridLine()
        {
            _transform = new CompositeTransform();
            this.RenderTransform = _transform;

            _selectedUI = new Canvas() { Visibility = Visibility.Collapsed };

            this.LoadLineRoot();
            this.LoadTemlateIntervals();
            this.Load();

            this.SizeChanged += (s, e) => this.Load();
        }

        #region public method

        public void Load(IEnumerable<double> intervals)
        {
            _intervals = intervals == null ? null : (from i in intervals select i).ToList();

            this.Load();
        }

        #endregion public method

        #region private method

        private Line CreateBaseLine(double move, double thickness)
        {
            var line = new Line()
            {
                StrokeThickness = thickness,
            };

            move = move + this.StrokeThickness / 2;

            if (AxisType == AxisType.X)
            {
                line.Y1 = 0;
                line.X1 = line.X2 = move;
                line.Y2 = this.ActualHeight;
            }
            else
            {
                line.X1 = 0;
                line.Y1 = line.Y2 = move;
                line.X2 = this.ActualWidth;
            }

            return line;
        }

        private Line CreateCoverLine(double move)
        {
            var line = this.CreateBaseLine(move, this.StrokeThickness + 5);
            line.Stroke = new SolidColorBrush(Colors.Transparent);
            return line;
        }

        private Line CreateDisplayLine(double move)
        {
            var line = this.CreateBaseLine(move, this.StrokeThickness);

            var b = new Binding("Stroke");
            b.Source = this;
            line.SetBinding(Line.StrokeProperty, b);

            b = new Binding("StrokeStyle");
            b.Source = this;
            line.SetBinding(Line.StrokeDashArrayProperty, b);

            b = new Binding("StrokeVisibility");
            b.Source = this;
            line.SetBinding(Line.VisibilityProperty, b);

            return line;
        }

        private void CreateEffect(double move, out EffectCircle circle1, out EffectCircle circle2)
        {
            var size = 8;

            circle1 = new EffectCircle() { Width = size, Height = size };
            circle2 = new EffectCircle() { Width = size, Height = size };

            move = move - (size - this.StrokeThickness) / 2;

            if (AxisType == AxisType.X)
            {
                Canvas.SetLeft(circle1, move);
                Canvas.SetTop(circle1, -size / 2);

                Canvas.SetLeft(circle2, move);
                Canvas.SetTop(circle2, this.ActualHeight - size / 2);
            }
            else
            {
                Canvas.SetLeft(circle1, -size / 2);
                Canvas.SetTop(circle1, move);

                Canvas.SetLeft(circle2, this.ActualWidth - size / 2);
                Canvas.SetTop(circle2, move);
            }
        }

        private void Load()
        {
            this.LoadLines();
            this.SetSize();
            this.SetDesc();
        }

        private void LoadTemlateIntervals()
        {
            _intervals = new List<double>();
            for (int i = 0; i < 6; i++)
            {
                _intervals.Add(i * 20);
            }
        }

        private void LoadLineRoot()
        {
            _root = new GridControl();
            _root.SelectedEffectElement = _selectedUI;
            _root.SettingWindow.Title = "设置网格线格式";
            _root.SettingWindow.Add(new SettingItem() { Header = "图案", Content = new StrokeSetting(this) });
        }

        private void LoadLines()
        {
            this.Children.Clear();
            _root.Clear();
            _selectedUI.Children.Clear();

            if (_intervals == null || _intervals.Count < 2)
                return;

            Line line;
            EffectCircle circle1, circle2;
            for (var i = 1; i < _intervals.Count; i++)
            {
                line = this.CreateDisplayLine(_intervals[i]);
                _root.Add(line);

                line = this.CreateCoverLine(_intervals[i]);
                _root.Add(line);

                this.CreateEffect(_intervals[i], out circle1, out circle2);
                _selectedUI.Children.Add(circle1);
                _selectedUI.Children.Add(circle2);
            }

            this.Children.Add(_root);
            this.Children.Add(_selectedUI);
        }

        private void SetSize()
        {

            if (AxisType == AxisType.X)
            {
                _root.Height = this.ActualHeight;
                _root.Width = this.ActualWidth + this.StrokeThickness;

                Canvas.SetLeft(_root, -this.StrokeThickness / 2);
                Canvas.SetTop(_root, 0);
                Canvas.SetLeft(_selectedUI, -this.StrokeThickness / 2);
                Canvas.SetTop(_selectedUI, 0);
            }
            else
            {
                _root.Height = this.ActualHeight + this.StrokeThickness;
                _root.Width = this.ActualWidth;

                Canvas.SetLeft(_root, 0);
                Canvas.SetTop(_root, -this.StrokeThickness / 2);
                Canvas.SetLeft(_selectedUI, 0);
                Canvas.SetTop(_selectedUI, -this.StrokeThickness / 2);
            }
        }

        private void SetDesc()
        {
            if (_transform == null)
                return;

            if (this.AxisType == AxisType.X)
            {
                _transform.ScaleY = 1;
                if (this.IsDesc)
                    _transform.ScaleX = -1;
                else
                    _transform.ScaleX = 1;
            }
            else
            {
                _transform.ScaleX = 1;
                if (this.IsDesc)
                    _transform.ScaleY = 1;
                else
                    _transform.ScaleY = -1;
            }
        }

        #endregion private method


        #region dp

        /// <summary>
        /// 是否次序反转。
        /// </summary>
        public bool IsDesc
        {
            get { return (bool)GetValue(IsDescProperty); }
            set { SetValue(IsDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDescProperty =
            DependencyProperty.Register("IsDesc", typeof(bool), typeof(GridLine),
            new PropertyMetadata(false, OnIsDescChanged));


        public AxisType AxisType
        {
            get { return (AxisType)GetValue(AxisTypeProperty); }
            set { SetValue(AxisTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisTypeProperty =
            DependencyProperty.Register("AxisType", typeof(AxisType), typeof(GridLine),
            new PropertyMetadata(OnAxisTypeChanged));

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(GridLine),
            new PropertyMetadata((double)1, OnStrokeThicknessChanged));


        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(GridLine),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 23, 55, 95))));



        public Visibility StrokeVisibility
        {
            get { return (Visibility)GetValue(StrokeVisibilityProperty); }
            set { SetValue(StrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeVisibilityProperty =
            DependencyProperty.Register("StrokeVisibility", typeof(Visibility), typeof(GridLine), null);



        public string StrokeStyle
        {
            get { return (string)GetValue(StrokeStyleProperty); }
            set { SetValue(StrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeStyleProperty =
            DependencyProperty.Register("StrokeStyle", typeof(string), typeof(GridLine),
            new PropertyMetadata(StrokeStyles.Dashed));



        #endregion

        #region static callback

        private static void OnIsDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var line = d as GridLine;
            line.OnIsDescChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private static void OnAxisTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var line = d as GridLine;
            line.OnAxisTypeChanged((AxisType)e.OldValue, (AxisType)e.NewValue);
        }

        private static void OnStrokeThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var line = d as GridLine;
            line.OnStrokeThicknessChanged((double)e.OldValue, (double)e.NewValue);
        }

        #endregion

        #region callback

        private void OnIsDescChanged(bool oldValue, bool newValue)
        {
            this.SetDesc();
        }

        private void OnAxisTypeChanged(AxisType oldValue, AxisType newValue)
        {
            this.SetDesc();
        }

        private void OnStrokeThicknessChanged(double oldValue, double newValue)
        {
            this.Load();
        }
        #endregion callback
    }

}
