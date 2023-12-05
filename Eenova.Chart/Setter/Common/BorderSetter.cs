using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class BorderSetter : StrokeSetter
    {
        IBorder _pElement;

        public BorderSetter(IBorder element)
            : base(element)
        {
            _pElement = element;
        }


        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.BorderVisibility != SBorderVisibility)
                _pElement.BorderVisibility = SBorderVisibility;

            if (_pElement.BorderBrush != SBorderBrush)
                _pElement.BorderBrush = SBorderBrush;

            base.Apply();
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.BorderVisibility != SBorderVisibility)
                SBorderVisibility = _pElement.BorderVisibility;

            if (_pElement.BorderBrush != SBorderBrush)
                SBorderBrush = _pElement.BorderBrush;

            base.Load();
        }

        #region dp


        public Visibility SBorderVisibility
        {
            get { return (Visibility)GetValue(SBorderVisibilityProperty); }
            set { SetValue(SBorderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderVisibilityProperty =
            DependencyProperty.Register("SBorderVisibility", typeof(Visibility), typeof(BorderSetter), null);



        public Brush SBorderBrush
        {
            get { return (Brush)GetValue(SBorderBrushProperty); }
            set { SetValue(SBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBorderBrushProperty =
            DependencyProperty.Register("SBorderBrush", typeof(Brush), typeof(BorderSetter), null);


        #endregion

    }
}
