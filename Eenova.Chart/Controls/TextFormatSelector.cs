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

namespace Eenova.Chart.Controls
{
    public class TextFormatSelector : Control
    {
        public TextFormatSelector()
        {
            this.DefaultStyleKey = typeof(TextFormatSelector);
        }


        internal string Start
        {
            get { return (string)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Start.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(string), typeof(TextFormatSelector),
            new PropertyMetadata(OnStartChanged));



        internal string End
        {
            get { return (string)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for End.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(string), typeof(TextFormatSelector),
            new PropertyMetadata(OnEndChanged));



        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(TextFormatSelector),
            new PropertyMetadata("@*|*@", OnFormatChanged));


        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as TextFormatSelector;
            c.OnStartChanged((string)e.OldValue, (string)e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as TextFormatSelector;
            c.OnEndChanged((string)e.OldValue, (string)e.NewValue);
        }

        private static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as TextFormatSelector;
            c.OnFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void OnFormatChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
            {
                this.Format = "@*|*@";
                return;
            }

            var splits = newValue.Split(new string[] { "*|*" }, StringSplitOptions.None);
            if (splits == null || splits.Length != 2)
            {
                this.Format = "@*|*@";
                return;
            }

            this.Start = splits[0].Substring(1);
            this.End = splits[1].Substring(1);
        }

        private void OnStartChanged(string oldValue, string newValue)
        {
            var splits = this.Format.Split(new string[] { "*|*" }, StringSplitOptions.None);
            if (splits == null || splits.Length != 2)
            {
                this.Format = "@" + this.Start + "*|*@";
                return;
            }

            this.Format = "@" + this.Start + "*|*" + splits[1];
        }

        private void OnEndChanged(string oldValue, string newValue)
        {
            var splits = this.Format.Split(new string[] { "*|*" }, StringSplitOptions.None);
            if (splits == null || splits.Length != 2)
            {
                this.Format = "@*|*@" + this.End;
                return;
            }

            this.Format = splits[0] + "*|*@" + this.End;
        }
    }
}
