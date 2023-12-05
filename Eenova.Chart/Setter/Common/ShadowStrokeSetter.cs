using System.Windows;
using System.Windows.Media;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class ShadowStrokeSetter : StrokeSetter
    {
        IShadowStroke _pElement;

        public ShadowStrokeSetter(IShadowStroke element)
            : base(element)
        {
            _pElement = element;
        }


        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.ShadowVisibility != SShadowVisibility)
                _pElement.ShadowVisibility = SShadowVisibility;

            base.Apply();
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.ShadowVisibility != SShadowVisibility)
                SShadowVisibility = _pElement.ShadowVisibility;

            base.Load();
        }


        #region dp


        public Visibility SShadowVisibility
        {
            get { return (Visibility)GetValue(SShadowVisibilityProperty); }
            set { SetValue(SShadowVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SShadowVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SShadowVisibilityProperty =
            DependencyProperty.Register("SShadowVisibility", typeof(Visibility), typeof(ShadowStrokeSetter), null);


        #endregion
    }
}
