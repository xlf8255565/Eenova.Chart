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


using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eenova.Chart.Controls
{
    /// <summary>
    /// 字体选择框。
    /// </summary>
    public class FontFamilyComboBox : BindingComboBox
    {
        public FontFamilyComboBox()
        {
            AddItems();
            ApplyConfig();
        }

        private void AddItems()
        {
            this.ItemsSource = this.CreateSource1();
        }

        private void ApplyConfig()
        {
            this.DisplayMemberPath = "Key";
            this.SelectedValuePath = "Value";
        }

        private Dictionary<string, string> CreateSource1()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("默认", FontFamilies.Default);
            dict.Add("宋体", FontFamilies.Simsun);
            dict.Add("新宋体", FontFamilies.NSimsun);
            dict.Add("楷体", FontFamilies.KaiTi);
            dict.Add("黑体", FontFamilies.SimHei);
            dict.Add("仿宋", FontFamilies.FangSong);
            dict.Add("微软正黑体", FontFamilies.MicrosoftJhengHei);
            dict.Add("细明体", FontFamilies.MingLiu);
            dict.Add("微软雅黑", FontFamilies.MicrosoftYaHei);
            dict.Add("隶书", FontFamilies.LiSu);
            dict.Add("华文彩云", FontFamilies.STCaiyun);
            dict.Add("华文琥珀", FontFamilies.STHupo);
            dict.Add("华文隶书", FontFamilies.STLiti);
            dict.Add("华文新魏", FontFamilies.STXinwei);
            dict.Add("华文行楷", FontFamilies.STXingkai);
            dict.Add("幼园", FontFamilies.YouYuan);
            dict.Add("Arial", FontFamilies.Arial);
            dict.Add("Arial Black", FontFamilies.ArialBlack);
            dict.Add("Courier New", FontFamilies.CourierNew);
            dict.Add("Georgia", FontFamilies.Georgia);
            dict.Add("Lucida Sans Unicode", FontFamilies.LucidaSansUnicode);
            dict.Add("Times New Roman", FontFamilies.TimesNewRoman);
            dict.Add("Trebuchet MS", FontFamilies.TrebuchetMS);
            dict.Add("Verdana", FontFamilies.Verdana);
            return dict;
        }

        private Dictionary<string, FontFamily> CreateSource2()
        {
            var dict = new Dictionary<string, FontFamily>();
            dict.Add("默认", new FontFamily(FontFamilies.Default));
            dict.Add("宋体", new FontFamily(FontFamilies.Simsun));
            dict.Add("新宋体", new FontFamily(FontFamilies.NSimsun));
            dict.Add("楷体", new FontFamily(FontFamilies.KaiTi));
            dict.Add("黑体", new FontFamily(FontFamilies.SimHei));
            dict.Add("仿宋", new FontFamily(FontFamilies.FangSong));
            dict.Add("微软正黑体", new FontFamily(FontFamilies.MicrosoftJhengHei));
            dict.Add("细明体", new FontFamily(FontFamilies.MingLiu));
            dict.Add("微软雅黑", new FontFamily(FontFamilies.MicrosoftYaHei));
            dict.Add("隶书", new FontFamily(FontFamilies.LiSu));
            dict.Add("华文彩云", new FontFamily(FontFamilies.STCaiyun));
            dict.Add("华文琥珀", new FontFamily(FontFamilies.STHupo));
            dict.Add("华文隶书", new FontFamily(FontFamilies.STLiti));
            dict.Add("华文新魏", new FontFamily(FontFamilies.STXinwei));
            dict.Add("华文行楷", new FontFamily(FontFamilies.STXingkai));
            dict.Add("幼园", new FontFamily(FontFamilies.YouYuan));
            dict.Add("Arial", new FontFamily(FontFamilies.Arial));
            dict.Add("Arial Black", new FontFamily(FontFamilies.ArialBlack));
            dict.Add("Courier New", new FontFamily(FontFamilies.CourierNew));
            dict.Add("Georgia", new FontFamily(FontFamilies.Georgia));
            dict.Add("Lucida Sans Unicode", new FontFamily(FontFamilies.LucidaSansUnicode));
            dict.Add("Times New Roman", new FontFamily(FontFamilies.TimesNewRoman));
            dict.Add("Trebuchet MS", new FontFamily(FontFamilies.TrebuchetMS));
            dict.Add("Verdana", new FontFamily(FontFamilies.Verdana));

            return dict;
        }
    }
}
