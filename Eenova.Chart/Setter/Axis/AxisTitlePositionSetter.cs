using System.Windows;
using System.Windows.Controls;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class AxisTitlePositionXSetter : AxisTitlePositionSetter
    {
        public AxisTitlePositionXSetter(IAxisTitlePosition element)
            : base(element)
        {
        }
    }

    public class AxisTitlePositionYSetter : AxisTitlePositionSetter
    {
        public AxisTitlePositionYSetter(IAxisTitlePosition element)
            : base(element)
        {
        }
    }

    public abstract class AxisTitlePositionSetter : BaseSetter
    {
        IAxisTitlePosition _pElement;

        public AxisTitlePositionSetter(IAxisTitlePosition element)
            : base(element)
        {
            _pElement = element;
        }


        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.TitleAlignment != STitleAlignment)
                _pElement.TitleAlignment = STitleAlignment;

            if (_pElement.TitleLocation != STitleLocation)
                _pElement.TitleLocation = STitleLocation;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.TitleAlignment != STitleAlignment)
                STitleAlignment = _pElement.TitleAlignment;

            if (_pElement.TitleLocation != STitleLocation)
                STitleLocation = _pElement.TitleLocation;
        }

        #region dp



        public AxisLocation STitleLocation
        {
            get { return (AxisLocation)GetValue(STitleLocationProperty); }
            set { SetValue(STitleLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STitleLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STitleLocationProperty =
            DependencyProperty.Register("STitleLocation", typeof(AxisLocation), typeof(AxisTitlePositionSetter), null);



        public AxisAlignment STitleAlignment
        {
            get { return (AxisAlignment)GetValue(STitleAlignmentProperty); }
            set { SetValue(STitleAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STitleAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STitleAlignmentProperty =
            DependencyProperty.Register("STitleAlignment", typeof(AxisAlignment), typeof(AxisTitlePositionSetter), null);


        #endregion

    }
}
