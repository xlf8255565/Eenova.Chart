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


using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Setter
{
    public class SettingItem : DependencyObject
    {
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(SettingItem), new PropertyMetadata(null));


        public ISetter Content
        {
            get { return (ISetter)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(ISetter), typeof(SettingItem), new PropertyMetadata(null));
    }

    public class SettingGroup : TabControl, ISetter
    {
        public SettingGroup()
        {
            this.Padding = new Thickness(0);
        }

        public void Apply()
        {
            foreach (TabItem item in this.Items)
            {
                if (item.Content != null)
                {
                    ((ISetter)item.Content).Apply();
                }
            }
        }

        public void Load()
        {
            foreach (TabItem item in this.Items)
            {
                if (item.Content != null)
                {
                    ((ISetter)item.Content).Load();
                }
            }
        }
    }
}
