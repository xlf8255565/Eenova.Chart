using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 数值格式化样式选择。
    /// </summary>
    public class NumbericFormatSelector : FormatSelector
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
                return null;
            }
        }
    }

    /// <summary>
    /// 时间格式化样式选择。
    /// </summary>
    public class DateTimeFormatSelector : FormatSelector
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
                return null;
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

    /// <summary>
    /// 格式化样式选择。
    /// </summary>
    public abstract class FormatSelector : Control
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


        public FormatSelector()
        {
            this.DefaultStyleKey = typeof(FormatSelector);
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


        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(FormatSelector), null);




        internal ObservableCollection<FormatItem> FormatSource
        {
            get { return (ObservableCollection<FormatItem>)GetValue(FormatSourceProperty); }
            set { SetValue(FormatSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatSourceProperty =
            DependencyProperty.Register("FormatSource", typeof(ObservableCollection<FormatItem>), typeof(FormatSelector), null);


    }
}
