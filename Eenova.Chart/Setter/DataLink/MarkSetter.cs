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
    public class MarkSetter : BaseSetter
    {
        IMark _pElement;

        public MarkSetter(IMark element)
            : base(element)
        {
            _pElement = element;
        }


        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.MarkVisibility != SMarkVisibility)
                _pElement.MarkVisibility = SMarkVisibility;

            if (_pElement.MarkType != SMarkType)
                _pElement.MarkType = SMarkType;

            if (_pElement.MarkSize != SMarkSize)
                _pElement.MarkSize = SMarkSize;

            if (_pElement.MarkBrush != SMarkBrush)
                _pElement.MarkBrush = SMarkBrush;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.MarkVisibility != SMarkVisibility)
                SMarkVisibility = _pElement.MarkVisibility;

            if (_pElement.MarkType != SMarkType)
                SMarkType = _pElement.MarkType;

            if (_pElement.MarkSize != SMarkSize)
                SMarkSize = _pElement.MarkSize;

            if (_pElement.MarkBrush != SMarkBrush)
                SMarkBrush = _pElement.MarkBrush;
        }


        public Visibility SMarkVisibility
        {
            get { return (Visibility)GetValue(SMarkVisibilityProperty); }
            set { SetValue(SMarkVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMarkVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarkVisibilityProperty =
            DependencyProperty.Register("SMarkVisibility", typeof(Visibility), typeof(MarkSetter), null);



        public Brush SMarkBrush
        {
            get { return (Brush)GetValue(SMarkBrushProperty); }
            set { SetValue(SMarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarkBrushProperty =
            DependencyProperty.Register("SMarkBrush", typeof(Brush), typeof(MarkSetter), null);



        public double SMarkSize
        {
            get { return (double)GetValue(SMarkSizeProperty); }
            set { SetValue(SMarkSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMarkSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarkSizeProperty =
            DependencyProperty.Register("SMarkSize", typeof(double), typeof(MarkSetter), null);



        public ShapeType SMarkType
        {
            get { return (ShapeType)GetValue(SMarkTypeProperty); }
            set { SetValue(SMarkTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMarkType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMarkTypeProperty =
            DependencyProperty.Register("SMarkType", typeof(ShapeType), typeof(MarkSetter), null);


    }
}
