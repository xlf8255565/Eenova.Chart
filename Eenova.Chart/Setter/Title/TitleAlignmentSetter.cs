using System.Windows;
using System.Windows.Controls;
using Eenova.Chart.Interfaces;
using System.Windows.Media;

namespace Eenova.Chart.Setter
{
    public class TitleAlignmentSetter : BaseSetter
    {
        Border _panel;
        ITitleAlignment _pElement;

        public TitleAlignmentSetter(ITitleAlignment element)
            : base(element)
        {
            _pElement = element;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _panel = this.GetTemplateChild("PreviewPanel") as Border;
            _panel.SizeChanged += (s, e) => this.SetClip();
        }


        private void SetClip()
        {
            if (_panel == null)
                return;

            _panel.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), new Point(_panel.ActualWidth,_panel.ActualHeight) )};
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.HorizontalContentAlignment != SHorizontalAlignment)
                _pElement.HorizontalContentAlignment = SHorizontalAlignment;

            if (_pElement.VerticalContentAlignment != SVerticalAlignment)
                _pElement.VerticalContentAlignment = SVerticalAlignment;

            if (_pElement.Orientation != SOrientation)
                _pElement.Orientation = SOrientation;

            if (_pElement.TextRotation != STextRotation)
                _pElement.TextRotation = STextRotation;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.HorizontalContentAlignment != SHorizontalAlignment)
                SHorizontalAlignment = _pElement.HorizontalContentAlignment;

            if (_pElement.VerticalContentAlignment != SVerticalAlignment)
                SVerticalAlignment = _pElement.VerticalContentAlignment;

            if (_pElement.Orientation != SOrientation)
                SOrientation = _pElement.Orientation;

            if (_pElement.TextRotation != STextRotation)
                STextRotation = _pElement.TextRotation;
        }

        #region dp



        public HorizontalAlignment SHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(SHorizontalAlignmentProperty); }
            set { SetValue(SHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizontalAlignmentProperty =
            DependencyProperty.Register("SHorizontalAlignment", typeof(HorizontalAlignment), typeof(TitleAlignmentSetter), null);



        public VerticalAlignment SVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(SVerticalAlignmentProperty); }
            set { SetValue(SVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SVerticalAlignmentProperty =
            DependencyProperty.Register("SVerticalAlignment", typeof(VerticalAlignment), typeof(TitleAlignmentSetter), null);



        public Orientation SOrientation
        {
            get { return (Orientation)GetValue(SOrientationProperty); }
            set { SetValue(SOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SOrientationProperty =
            DependencyProperty.Register("SOrientation", typeof(Orientation), typeof(TitleAlignmentSetter), null);



        public double STextRotation
        {
            get { return (double)GetValue(STextRotationProperty); }
            set { SetValue(STextRotationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STextRotation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STextRotationProperty =
            DependencyProperty.Register("STextRotation", typeof(double), typeof(TitleAlignmentSetter), null);


        #endregion

    }
}
