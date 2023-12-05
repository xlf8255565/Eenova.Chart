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
    public class TicksStrokeXSetter : TicksStrokeSetter
    {
        public TicksStrokeXSetter(ITicksStroke element)
            : base(element)
        {
        }
    }


    public class TicksStrokeYSetter : TicksStrokeSetter
    {
        public TicksStrokeYSetter(ITicksStroke element)
            : base(element)
        {
        }
    }


    public abstract class TicksStrokeSetter : StrokeSetter
    {
        ITicksStroke _pElement;

        public TicksStrokeSetter(ITicksStroke element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.MainTicksShow != SMainTicksShow)
                _pElement.MainTicksShow = SMainTicksShow;

            if (_pElement.SubTicksShow != SSubTicksShow)
                _pElement.SubTicksShow = SSubTicksShow;

            base.Apply();
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.MainTicksShow != SMainTicksShow)
                SMainTicksShow = _pElement.MainTicksShow;

            if (_pElement.SubTicksShow != SSubTicksShow)
                SSubTicksShow = _pElement.SubTicksShow;

            base.Load();
        }

        #region dp

        public TicksShow SMainTicksShow
        {
            get { return (TicksShow)GetValue(SMainTicksShowProperty); }
            set { SetValue(SMainTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SMainTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SMainTicksShowProperty =
            DependencyProperty.Register("SMainTicksShow", typeof(TicksShow), typeof(TicksStrokeSetter), null);



        public TicksShow SSubTicksShow
        {
            get { return (TicksShow)GetValue(SSubTicksShowProperty); }
            set { SetValue(SSubTicksShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SSubTicksShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SSubTicksShowProperty =
            DependencyProperty.Register("SSubTicksShow", typeof(TicksShow), typeof(TicksStrokeSetter), null);

        #endregion
    }
}
