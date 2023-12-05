/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Setter
{
    /// <summary>
    /// 格式化样式选择。
    /// </summary>
    public abstract class FormatSelector : Control
    {
        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(FormatSelector), 
            new PropertyMetadata(OnFormatChanged));

        private static void OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as FormatSelector;
            c.OnFormatChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnFormatChanged(string oldValue, string newValue)
        {
        }
    }

    public class TextFormatSelector : FormatSelector
    {
        bool _ignoreChange = false;

        public TextFormatSelector()
        {
            this.DefaultStyleKey = typeof(TextFormatSelector);
            this.Format = "@*|*@";
        }

        protected override void OnFormatChanged(string oldValue, string newValue)
        {
            if (_ignoreChange)
            {
                _ignoreChange = false;
                return;
            }

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

            _ignoreChange = true;
            this.Start = splits[0].Substring(1);
            _ignoreChange = true;
            this.End = splits[1].Substring(1);
        }

        #region Start

        internal string Start
        {
            get { return (string)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Start.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(string), typeof(TextFormatSelector),
            new PropertyMetadata(OnStartChanged));

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as TextFormatSelector;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }
            source._ignoreChange = true;
            source.Format = string.Format("@{0}*|*@{1}", source.Start, source.End);
        }

        #endregion Start

        #region end

        internal string End
        {
            get { return (string)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for End.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(string), typeof(TextFormatSelector),
            new PropertyMetadata(OnEndChanged));

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as TextFormatSelector;
            if (source._ignoreChange)
            {
                source._ignoreChange = false;
                return;
            }
            source._ignoreChange = true;
            source.Format = string.Format("@{0}*|*@{1}", source.Start, source.End);
        }
        #endregion end
    }

    public abstract class DeepFormatSelector : FormatSelector
    {
        public class FormatItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        Button _btn;
        ListBox _list;
        TextBox _addText;
        protected IList<string> _formats;

        public DeepFormatSelector()
        {
            this.DefaultStyleKey = typeof(DeepFormatSelector);
            this.LoadSource();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _list = this.GetTemplateChild("List") as ListBox;
            _addText = this.GetTemplateChild("AddText") as TextBox;
            _btn = this.GetTemplateChild("AddButton") as Button;
            _btn.Click += new RoutedEventHandler(_btn_Click);
        }

        void _btn_Click(object sender, RoutedEventArgs e)
        {
            var text = _addText.Text;
            if (string.IsNullOrWhiteSpace(text) || _formats.Contains(text))
                return;

            var value = this.Formats(text);
            if (!string.IsNullOrWhiteSpace(value))
            {
                _formats.Add(text);
                var item = new FormatItem() { Key = text, Value = value };
                this.FormatSource.Add(item);
                _list.SelectedItem = item;
            }
        }

        private void LoadSource()
        {
            this.FormatSource = new ObservableCollection<FormatItem>();
            var formats = this.LoadFormats();
            _formats = new List<string>(formats);
            string date;
            for (int i = 0; i < formats.Length; i++)
            {
                date = this.Formats(formats[i]);
                if (date == null)
                    continue;

                this.FormatSource.Add(new FormatItem() { Key = formats[i], Value = date });
            }
        }

        protected abstract string[] LoadFormats();
        protected abstract string Formats(string format);

        internal ObservableCollection<FormatItem> FormatSource
        {
            get { return (ObservableCollection<FormatItem>)GetValue(FormatSourceProperty); }
            set { SetValue(FormatSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatSource.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty FormatSourceProperty =
            DependencyProperty.Register("FormatSource", typeof(ObservableCollection<FormatItem>), typeof(DeepFormatSelector), null);
    }

    /// <summary>
    /// 数值格式化样式选择。
    /// </summary>
    public class NumbericFormatSelector : DeepFormatSelector
    {
        double _number = 12345.6789;

        protected override string[] LoadFormats()
        {
            string[] formats = {
                                   "0","0.0",
                                   "0.00","0.000",
                                   "e","e1",
                                   "e2","e3",
                                   "p","p1",
                                   "p2","p3",
                                   "c","c1",
                                   "c2","c3",
                                   "$0","$0.0",
                                   "$0,0.00"
                               };
            return formats;
        }

        protected override string Formats(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return null;

            try
            {
                return _number.ToString(format);
            }
            catch
            {
                return _number.ToString();
            }
        }
    }

    /// <summary>
    /// 时间格式化样式选择。
    /// </summary>
    public class DateTimeFormatSelector : DeepFormatSelector
    {
        DateTime _dt = new DateTime(2010, 1, 15, 12, 24, 36);

        protected override string Formats(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return null;

            try
            {
                return _dt.ToString(format);
            }
            catch
            {
                return _dt.ToString();
            }
        }

        protected override string[] LoadFormats()
        {
            string[] formats = {
                                   "yy-MM-dd" ,"yy年MM月dd日",
                                   "yyy-MM-dd","yyyy年MM月dd日",
                                   "yyy-MM-dd hh:mm","yyy-MM-dd hh:mm:ss",
                                   "yyyy年MM月dd日 hh:mm","yyyy年MM月dd日 hh:mm:ss",
                                   "MM-dd" ,"MM月dd日",
                                   "hh:mm","hh:mm:ss",
                                   "yy-MM" ,"yy年MM月",
                                   "yy-MM-dd ddd" ,"yy年MM月dd日 ddd",
                                   "dddd, MMMM dd yyyy","ddd, MMM d \"'\"yy",
                                   "dd-MM-yy"
                               };
            return formats;
        }
    }
}

