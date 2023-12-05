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
    public class LinkSetter : BaseSetter
    {
        ILink _pElement;

        public LinkSetter(ILink element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.LinkedY != SLinkedY)
                _pElement.LinkedY = SLinkedY;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.LinkedY != SLinkedY)
                SLinkedY = _pElement.LinkedY;
        }



        public PlotY SLinkedY
        {
            get { return (PlotY)GetValue(SLinkedYProperty); }
            set { SetValue(SLinkedYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SLinkedY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SLinkedYProperty =
            DependencyProperty.Register("SLinkedY", typeof(PlotY), typeof(LinkSetter), null);


    }
}
