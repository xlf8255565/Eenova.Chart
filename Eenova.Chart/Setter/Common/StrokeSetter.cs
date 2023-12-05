using System.Windows;
using System.Windows.Media;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class StrokeSetter : BaseSetter
    {
        IStroke _pElement;

        public StrokeSetter(IStroke element)
            : base(element)
        {
            _pElement = element;
        }


        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.StrokeVisibility != SStrokeVisibility)
                _pElement.StrokeVisibility = SStrokeVisibility;

            if (_pElement.Stroke != SStroke)
                _pElement.Stroke = SStroke;

            if (_pElement.StrokeStyle != SStrokeStyle)
                _pElement.StrokeStyle = SStrokeStyle;

            if (_pElement.StrokeThickness != SStrokeThickness)
                _pElement.StrokeThickness = SStrokeThickness;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.StrokeVisibility != SStrokeVisibility)
                SStrokeVisibility = _pElement.StrokeVisibility;

            if (_pElement.Stroke != SStroke)
                SStroke = _pElement.Stroke;

            if (_pElement.StrokeStyle != SStrokeStyle)
                SStrokeStyle = _pElement.StrokeStyle;

            if (_pElement.StrokeThickness != SStrokeThickness)
                SStrokeThickness = _pElement.StrokeThickness;
        }

        #region dp


        public Visibility SStrokeVisibility
        {
            get { return (Visibility)GetValue(SStrokeVisibilityProperty); }
            set { SetValue(SStrokeVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeVisibilityProperty =
            DependencyProperty.Register("SStrokeVisibility", typeof(Visibility), typeof(StrokeSetter), null);



        public string SStrokeStyle
        {
            get { return (string)GetValue(SStrokeStyleProperty); }
            set { SetValue(SStrokeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeStyleProperty =
            DependencyProperty.Register("SStrokeStyle", typeof(string), typeof(StrokeSetter), null);



        public Brush SStroke
        {
            get { return (Brush)GetValue(SStrokeProperty); }
            set { SetValue(SStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeProperty =
            DependencyProperty.Register("SStroke", typeof(Brush), typeof(StrokeSetter), null);



        public double SStrokeThickness
        {
            get { return (double)GetValue(SStrokeThicknessProperty); }
            set { SetValue(SStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SStrokeThicknessProperty =
            DependencyProperty.Register("SStrokeThickness", typeof(double), typeof(StrokeSetter), null);



        #endregion

    }
}
