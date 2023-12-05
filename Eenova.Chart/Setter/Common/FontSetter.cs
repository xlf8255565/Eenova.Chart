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
    public class FontSetter : BaseSetter
    {
        IFont _pElement;

        public FontSetter(IFont element)
            : base(element)
        {
            _pElement = element;
        }

        public override void Apply()
        {
            if (_pElement == null)
                return;

            if (_pElement.FontFamily.Source != SFontFamily.Source)
                _pElement.FontFamily = SFontFamily;

            if (_pElement.FontSize != SFontSize)
                _pElement.FontSize = SFontSize;

            if (_pElement.FontStyle != SFontStyle)
                _pElement.FontStyle = SFontStyle;

            if (_pElement.FontWeight != SFontWeight)
                _pElement.FontWeight = SFontWeight;

            if (_pElement.TextDecorations != STextDecorations)
                _pElement.TextDecorations = STextDecorations;

            if (_pElement.Foreground != SForeground)
                _pElement.Foreground = SForeground;

            if (_pElement.Background != SBackground)
                _pElement.Background = SBackground;
        }

        public override void Load()
        {
            if (_pElement == null)
                return;

            if (_pElement.FontFamily.Source != SFontFamily.Source)
                SFontFamily = _pElement.FontFamily;

            if (_pElement.FontSize != SFontSize)
                SFontSize = _pElement.FontSize;

            if (_pElement.FontStyle != SFontStyle)
                SFontStyle = _pElement.FontStyle;

            if (_pElement.FontWeight != SFontWeight)
                SFontWeight = _pElement.FontWeight;

            if (_pElement.TextDecorations != STextDecorations)
                STextDecorations = _pElement.TextDecorations;

            if (_pElement.Foreground != SForeground)
                SForeground = _pElement.Foreground;

            if (_pElement.Background != SBackground)
                SBackground = _pElement.Background;
        }

        #region dp



        public FontFamily SFontFamily
        {
            get { return (FontFamily)GetValue(SFontFamilyProperty); }
            set { SetValue(SFontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SFontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SFontFamilyProperty =
            DependencyProperty.Register("SFontFamily", typeof(FontFamily), typeof(FontSetter),
            new PropertyMetadata(new FontFamily("Arial")));



        public double SFontSize
        {
            get { return (double)GetValue(SFontSizeProperty); }
            set { SetValue(SFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SFontSizeProperty =
            DependencyProperty.Register("SFontSize", typeof(double), typeof(FontSetter), null);



        public FontStyle SFontStyle
        {
            get { return (FontStyle)GetValue(SFontStyleProperty); }
            set { SetValue(SFontStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SFontStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SFontStyleProperty =
            DependencyProperty.Register("SFontStyle", typeof(FontStyle), typeof(FontSetter), null);



        public FontWeight SFontWeight
        {
            get { return (FontWeight)GetValue(SFontWeightProperty); }
            set { SetValue(SFontWeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SFontWeightProperty =
            DependencyProperty.Register("SFontWeight", typeof(FontWeight), typeof(FontSetter), null);



        public TextDecorationCollection STextDecorations
        {
            get { return (TextDecorationCollection)GetValue(STextDecorationsProperty); }
            set { SetValue(STextDecorationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for STextDecorations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty STextDecorationsProperty =
            DependencyProperty.Register("STextDecorations", typeof(TextDecorationCollection), typeof(FontSetter), null);



        public Brush SForeground
        {
            get { return (Brush)GetValue(SForegroundProperty); }
            set { SetValue(SForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SForegroundProperty =
            DependencyProperty.Register("SForeground", typeof(Brush), typeof(FontSetter), null);



        public Brush SBackground
        {
            get { return (Brush)GetValue(SBackgroundProperty); }
            set { SetValue(SBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SBackgroundProperty =
            DependencyProperty.Register("SBackground", typeof(Brush), typeof(FontSetter), null);


        #endregion
    }
}
