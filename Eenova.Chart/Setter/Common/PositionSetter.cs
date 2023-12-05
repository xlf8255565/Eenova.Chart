using System.Windows;
using System.Windows.Controls;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class PositionSetter : BaseSetter
    {
        IPosition _pElement;

        public PositionSetter(IPosition element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (SHorizontalAlignment != _pElement.HorizontalAlignment)
                SHorizontalAlignment = _pElement.HorizontalAlignment;

            if (SVerticalAlignment != _pElement.VerticalAlignment)
                SVerticalAlignment = _pElement.VerticalAlignment;

            if (_pElement.Margin != SMargin)
                SMargin = _pElement.Margin;


            if (!double.IsNaN(_pElement.Width))
            {
                SIsWidthAuto = false;
                SWidth = _pElement.Width;
            }
            else
            {
                SIsWidthAuto = true;
                SWidth = _pElement.ActualWidth;
            }

            if (!double.IsNaN(_pElement.Height))
            {
                SIsHeightAuto = false;
                SHeight = _pElement.Height;
            }
            else
            {
                SIsHeightAuto = true;
                SHeight = _pElement.ActualHeight;
            }

        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.HorizontalAlignment != SHorizontalAlignment)
                _pElement.HorizontalAlignment = SHorizontalAlignment;

            if (_pElement.VerticalAlignment != SVerticalAlignment)
                _pElement.VerticalAlignment = SVerticalAlignment;

            if (_pElement.Margin != SMargin)
                _pElement.Margin = SMargin;


            if (SIsWidthAuto)
            {
                if (!double.IsNaN(_pElement.Width))
                {
                    _pElement.Width = double.NaN;
                }
            }
            else if (SWidth != _pElement.Width)
            {
                _pElement.Width = SWidth;
            }

            if (SIsHeightAuto)
            {
                if (!double.IsNaN(_pElement.Height))
                {
                    _pElement.Height = double.NaN;
                }
            }
            else if (SHeight != _pElement.Height)
            {
                _pElement.Height = SHeight;
            }
        }

        #region dp



        public Thickness SMargin
        {
            get { return (Thickness)GetValue(SMarginProperty); }
            set { SetValue(SMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarginProperty =
            DependencyProperty.Register("SMargin", typeof(Thickness), typeof(PositionSetter), null);



        public HorizontalAlignment SHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(SHorizontalAlignmentProperty); }
            set { SetValue(SHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizontalAlignmentProperty =
            DependencyProperty.Register("SHorizontalAlignment", typeof(HorizontalAlignment), typeof(PositionSetter), null);



        public VerticalAlignment SVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(SVerticalAlignmentProperty); }
            set { SetValue(SVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SVerticalAlignmentProperty =
            DependencyProperty.Register("SVerticalAlignment", typeof(VerticalAlignment), typeof(PositionSetter), null);



        public double SWidth
        {
            get { return (double)GetValue(SWidthProperty); }
            set { SetValue(SWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SWidthProperty =
            DependencyProperty.Register("S", typeof(double), typeof(PositionSetter), null);



        public double SHeight
        {
            get { return (double)GetValue(SHeightProperty); }
            set { SetValue(SHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHeightProperty =
            DependencyProperty.Register("SHeight", typeof(double), typeof(PositionSetter), null);



        public bool SIsWidthAuto
        {
            get { return (bool)GetValue(SIsWidthAutoProperty); }
            set { SetValue(SIsWidthAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsWidthAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsWidthAutoProperty =
            DependencyProperty.Register("SIsWidthAuto", typeof(bool), typeof(PositionSetter), null);



        public bool SIsHeightAuto
        {
            get { return (bool)GetValue(SIsHeightAutoProperty); }
            set { SetValue(SIsHeightAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SIsHeightAuto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SIsHeightAutoProperty =
            DependencyProperty.Register("SIsHeightAuto", typeof(bool), typeof(PositionSetter), null);


        #endregion
    }
}
