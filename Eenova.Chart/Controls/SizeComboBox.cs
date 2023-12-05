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

namespace Eenova.Chart.Controls
{
    public class SizeComboBox : ComboBox
    {
        public SizeComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            for (var i = 8; i < 20; i++)
            {
                this.Items.Add((double)i);
            }
            for (var i = 20; i <= 64; i++)
            {
                this.Items.Add((double)i);
                i = i + 3;
            }
            this.Items.Add((double)78);
            this.Items.Add((double)96);
            this.Items.Add((double)128);
        }

        private void ApplyConfig()
        {
        }
    }
}
