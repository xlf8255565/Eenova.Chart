using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class PlotAreaPositionSetter : BaseSetter
    {
        IPlotAreaPosition _pElement;

        public PlotAreaPositionSetter(IPlotAreaPosition element)
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

            if (STopHeight != _pElement.TopHeight)
                STopHeight = _pElement.TopHeight;

            if (SBottomHeight != _pElement.BottomHeight)
                SBottomHeight = _pElement.BottomHeight;

            if (SLength != _pElement.Length)
                SLength = _pElement.Length;
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

            if (STopHeight != _pElement.TopHeight)
                _pElement.TopHeight = STopHeight;

            if (SBottomHeight != _pElement.BottomHeight)
                _pElement.BottomHeight = SBottomHeight;

            if (SLength != _pElement.Length)
                _pElement.Length = SLength;

        }

        #region dp

        public Thickness SMargin
        {
            get { return (Thickness)GetValue(SMarginProperty); }
            set { SetValue(SMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarginProperty =
            DependencyProperty.Register("SMargin", typeof(Thickness), typeof(PlotAreaPositionSetter), null);

        public HorizontalAlignment SHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(SHorizontalAlignmentProperty); }
            set { SetValue(SHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizontalAlignmentProperty =
            DependencyProperty.Register("SHorizontalAlignment", typeof(HorizontalAlignment), typeof(PlotAreaPositionSetter), null);



        public VerticalAlignment SVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(SVerticalAlignmentProperty); }
            set { SetValue(SVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SVerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SVerticalAlignmentProperty =
            DependencyProperty.Register("SVerticalAlignment", typeof(VerticalAlignment), typeof(PlotAreaPositionSetter), null);

        public double STopHeight
        {
            get { return (double)GetValue(STopHeightProperty); }
            set { SetValue(STopHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STopHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STopHeightProperty =
            DependencyProperty.Register("STopHeight", typeof(double), typeof(PlotAreaPositionSetter), null);



        public double SBottomHeight
        {
            get { return (double)GetValue(SBottomHeightProperty); }
            set { SetValue(SBottomHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBottomHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBottomHeightProperty =
            DependencyProperty.Register("SBottomHeight", typeof(double), typeof(PlotAreaPositionSetter), null);



        public double SLength
        {
            get { return (double)GetValue(SLengthProperty); }
            set { SetValue(SLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLengthProperty =
            DependencyProperty.Register("SLength", typeof(double), typeof(PlotAreaPositionSetter), null);



        #endregion
    }
}
