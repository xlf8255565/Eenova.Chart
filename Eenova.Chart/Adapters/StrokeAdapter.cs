using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Adapters
{
    /// <summary>
    /// 把shape转化为IStroke接口的适配器。
    /// </summary>
    public class StrokeAdapter : IStroke
    {
        Shape _shape;

        public StrokeAdapter(Shape shape)
        {
            _shape = shape;
        }

        public Visibility StrokeVisibility
        {
            get { return _shape.Visibility; }
            set { _shape.Visibility = value; }
        }

        public string StrokeStyle
        {
            get { return this.GetStyle(); }
            set { this.SetStyle(value); }
        }

        public double StrokeThickness
        {
            get { return _shape.StrokeThickness; }
            set { _shape.StrokeThickness = value; }
        }

        public Brush Stroke
        {
            get { return _shape.Stroke; }
            set { _shape.Stroke = value; }
        }

        private string GetStyle()
        {
            if (_shape.StrokeDashArray == null || _shape.StrokeDashArray.Count == 0)
            {
                return StrokeStyles.Solid;
            }

            string style = string.Empty;
            foreach (var value in _shape.StrokeDashArray)
            {
                style += value + " ";
            }
            style = style.Trim();

            if (string.IsNullOrEmpty(style))
                return StrokeStyles.Solid;

            return style;
        }

        private void SetStyle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _shape.StrokeDashArray.Clear();
            }
            else
            {
                var splits = value.Split(' ');
                var doubles = new DoubleCollection();
                foreach (var split in splits)
                {
                    doubles.Add(double.Parse(split.Trim()));
                }
                _shape.StrokeDashArray = doubles;
            }
        }
    }
}
