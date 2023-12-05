using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class PlotAreaDisplaySetter : BaseSetter
    {
        IPlotAreaDisplay _pElement;

        public PlotAreaDisplaySetter(IPlotAreaDisplay element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.TopVisibility != STopVisibility)
                _pElement.TopVisibility = STopVisibility;

            if (_pElement.BottomVisibility != SBottomVisibility)
                _pElement.BottomVisibility = SBottomVisibility;

            if (_pElement.LX2Visibility != SLX2Visibility)
                _pElement.LX2Visibility = SLX2Visibility;

            if (_pElement.LX1Visibility != SLX1Visibility)
                _pElement.LX1Visibility = SLX1Visibility;

            if (_pElement.LY2Visibility != SLY2Visibility)
                _pElement.LY2Visibility = SLY2Visibility;

            if (_pElement.LY1Visibility != SLY1Visibility)
                _pElement.LY1Visibility = SLY1Visibility;

            if (_pElement.LY4Visibility != SLY4Visibility)
                _pElement.LY4Visibility = SLY4Visibility;

            if (_pElement.LY3Visibility != SLY3Visibility)
                _pElement.LY3Visibility = SLY3Visibility;

            if (_pElement.IsXVisible != SIsXVisible)
                _pElement.IsXVisible = SIsXVisible;

            if (_pElement.IsY2Visible != SIsY2Visible)
                _pElement.IsY2Visible = SIsY2Visible;

            if (_pElement.IsY1Visible != SIsY1Visible)
                _pElement.IsY1Visible = SIsY1Visible;

            if (_pElement.IsY4Visible != SIsY4Visible)
                _pElement.IsY4Visible = SIsY4Visible;

            if (_pElement.IsY3Visible != SIsY3Visible)
                _pElement.IsY3Visible = SIsY3Visible;

            if (_pElement.XAlignment != SXAlignment)
                _pElement.XAlignment = SXAlignment;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.TopVisibility != STopVisibility)
                STopVisibility = _pElement.TopVisibility;

            if (_pElement.BottomVisibility != SBottomVisibility)
                SBottomVisibility = _pElement.BottomVisibility;

            if (_pElement.LX2Visibility != SLX2Visibility)
                SLX2Visibility = _pElement.LX2Visibility;

            if (_pElement.LX1Visibility != SLX1Visibility)
                SLX1Visibility = _pElement.LX1Visibility;

            if (_pElement.LY2Visibility != SLY2Visibility)
                SLY2Visibility = _pElement.LY2Visibility;

            if (_pElement.LY1Visibility != SLY1Visibility)
                SLY1Visibility = _pElement.LY1Visibility;

            if (_pElement.LY4Visibility != SLY4Visibility)
                SLY4Visibility = _pElement.LY4Visibility;

            if (_pElement.LY3Visibility != SLY3Visibility)
                SLY3Visibility = _pElement.LY3Visibility;

            if (_pElement.IsXVisible != SIsXVisible)
                SIsXVisible = _pElement.IsXVisible;

            if (_pElement.IsY2Visible != SIsY2Visible)
                SIsY2Visible = _pElement.IsY2Visible;

            if (_pElement.IsY1Visible != SIsY1Visible)
                SIsY1Visible = _pElement.IsY1Visible;

            if (_pElement.IsY4Visible != SIsY4Visible)
                SIsY4Visible = _pElement.IsY4Visible;

            if (_pElement.IsY3Visible != SIsY3Visible)
                SIsY3Visible = _pElement.IsY3Visible;

            if (_pElement.XAlignment != SXAlignment)
                SXAlignment = _pElement.XAlignment;

        }

        #region visibility



        public VerticalAlignment SXAlignment
        {
            get { return (VerticalAlignment)GetValue(SXAlignmentProperty); }
            set { SetValue(SXAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SXAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SXAlignmentProperty =
            DependencyProperty.Register("SXAlignment", typeof(VerticalAlignment), typeof(PlotAreaDisplaySetter), null);



        public Visibility STopVisibility
        {
            get { return (Visibility)GetValue(STopVisibilityProperty); }
            set { SetValue(STopVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STopVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STopVisibilityProperty =
            DependencyProperty.Register("STopVisibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SBottomVisibility
        {
            get { return (Visibility)GetValue(SBottomVisibilityProperty); }
            set { SetValue(SBottomVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBottomVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBottomVisibilityProperty =
            DependencyProperty.Register("SBottomVisibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);




        public bool SIsXVisible
        {
            get { return (bool)GetValue(SIsXVisibleProperty); }
            set { SetValue(SIsXVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsXVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsXVisibleProperty =
            DependencyProperty.Register("SIsXVisible", typeof(bool), typeof(PlotAreaDisplaySetter), null);



        public bool SIsY1Visible
        {
            get { return (bool)GetValue(SIsY1VisibleProperty); }
            set { SetValue(SIsY1VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsY1Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsY1VisibleProperty =
            DependencyProperty.Register("SIsY1Visible", typeof(bool), typeof(PlotAreaDisplaySetter), null);



        public bool SIsY2Visible
        {
            get { return (bool)GetValue(SIsY2VisibleProperty); }
            set { SetValue(SIsY2VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsY2Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsY2VisibleProperty =
            DependencyProperty.Register("SIsY2Visible", typeof(bool), typeof(PlotAreaDisplaySetter), null);



        public bool SIsY3Visible
        {
            get { return (bool)GetValue(SIsY3VisibleProperty); }
            set { SetValue(SIsY3VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsY3Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsY3VisibleProperty =
            DependencyProperty.Register("SIsY3Visible", typeof(bool), typeof(PlotAreaDisplaySetter), null);



        public bool SIsY4Visible
        {
            get { return (bool)GetValue(SIsY4VisibleProperty); }
            set { SetValue(SIsY4VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsY4Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsY4VisibleProperty =
            DependencyProperty.Register("SIsY4Visible", typeof(bool), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLX1Visibility
        {
            get { return (Visibility)GetValue(SLX1VisibilityProperty); }
            set { SetValue(SLX1VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLX1Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLX1VisibilityProperty =
            DependencyProperty.Register("SLX1Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLX2Visibility
        {
            get { return (Visibility)GetValue(SLX2VisibilityProperty); }
            set { SetValue(SLX2VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLX2Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLX2VisibilityProperty =
            DependencyProperty.Register("SLX2Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLY1Visibility
        {
            get { return (Visibility)GetValue(SLY1VisibilityProperty); }
            set { SetValue(SLY1VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLY1Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLY1VisibilityProperty =
            DependencyProperty.Register("SLY1Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLY2Visibility
        {
            get { return (Visibility)GetValue(SLY2VisibilityProperty); }
            set { SetValue(SLY2VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLY2Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLY2VisibilityProperty =
            DependencyProperty.Register("SLY2Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLY3Visibility
        {
            get { return (Visibility)GetValue(SLY3VisibilityProperty); }
            set { SetValue(SLY3VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLY3Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLY3VisibilityProperty =
            DependencyProperty.Register("SLY3Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        public Visibility SLY4Visibility
        {
            get { return (Visibility)GetValue(SLY4VisibilityProperty); }
            set { SetValue(SLY4VisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLY4Visibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLY4VisibilityProperty =
            DependencyProperty.Register("SLY4Visibility", typeof(Visibility), typeof(PlotAreaDisplaySetter), null);



        #endregion

    }
}
