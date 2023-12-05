using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Setter
{
    public class TextSetter : BaseSetter
    {
        IText _pElement;

        public TextSetter(IText element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.Text != SText)
                _pElement.Text = this.SText;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.Text != SText)
                SText = _pElement.Text;
        }


        #region dp


        public string SText
        {
            get { return (string)GetValue(STextProperty); }
            set { SetValue(STextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STextProperty =
            DependencyProperty.Register("SText", typeof(string), typeof(TextSetter), null);

        #endregion

    }
}
