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


using System.Windows.Controls;

namespace Eenova.Chart.Controls
{
    public class SizeComboBox : BindingComboBox
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
