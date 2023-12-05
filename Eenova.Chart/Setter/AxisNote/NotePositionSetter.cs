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
    public class NotePositionSetter : BaseSetter
    {
        INotePosition _pElement;

        public NotePositionSetter(INotePosition element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.NoteLocation != SNoteLocation)
                _pElement.NoteLocation = SNoteLocation;

            if (_pElement.HorizontalOffset != SHorizontalOffset)
                _pElement.HorizontalOffset = SHorizontalOffset;

            if (_pElement.VerticalOffset != SVerticalOffset)
                _pElement.VerticalOffset = SVerticalOffset;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.NoteLocation != SNoteLocation)
                SNoteLocation = _pElement.NoteLocation;

            if (_pElement.HorizontalOffset != SHorizontalOffset)
                SHorizontalOffset = _pElement.HorizontalOffset;

            if (_pElement.VerticalOffset != SVerticalOffset)
                SVerticalOffset = _pElement.VerticalOffset;

        }



        public NoteLocation SNoteLocation
        {
            get { return (NoteLocation)GetValue(SNoteLocationProperty); }
            set { SetValue(SNoteLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SNoteLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SNoteLocationProperty =
            DependencyProperty.Register("SNoteLocation", typeof(NoteLocation), typeof(NotePositionSetter), null);



        public double SVerticalOffset
        {
            get { return (double)GetValue(SVerticalOffsetProperty); }
            set { SetValue(SVerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SVerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SVerticalOffsetProperty =
            DependencyProperty.Register("SVerticalOffset", typeof(double), typeof(NotePositionSetter), null);



        public double SHorizontalOffset
        {
            get { return (double)GetValue(SHorizontalOffsetProperty); }
            set { SetValue(SHorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SHorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SHorizontalOffsetProperty =
            DependencyProperty.Register("SHorizontalOffset", typeof(double), typeof(NotePositionSetter), null);




    }
}
