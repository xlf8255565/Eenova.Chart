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
using System.Collections.Generic;

namespace Eenova.Chart.Controls
{
    public class NoteLocationComboBox : ComboBox
    {
        public NoteLocationComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            var dict = new Dictionary<string, NoteLocation>();
            dict.Add("上方", NoteLocation.Top);
            dict.Add("下方", NoteLocation.Bottom);
            this.ItemsSource = dict;
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }
    }
}
