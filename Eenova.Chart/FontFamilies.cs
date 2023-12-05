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



using System.Collections;
using System.Collections.Generic;
namespace Eenova.Chart
{
    public static class FontFamilies
    {
        /// <summary>
        /// 默认。
        /// </summary>
        public static string Default = "Portable User Interface";

        /// <summary>
        /// 宋体。
        /// </summary>
        public static string Simsun = "SimSun";

        /// <summary>
        /// 新宋体。
        /// </summary>
        public static string NSimsun = "NSimSun";

        /// <summary>
        /// 楷体。
        /// </summary>
        public static string KaiTi = "KaiTi";

        /// <summary>
        /// 黑体。
        /// </summary>
        public static string SimHei = "SimHei";

        /// <summary>
        /// 仿宋。
        /// </summary>
        public static string FangSong = "FangSong";

        /// <summary>
        /// 微软正黑体。
        /// </summary>
        public static string MicrosoftJhengHei = "Microsoft JhengHei";

        /// <summary>
        /// 细明体。
        /// </summary>
        public static string MingLiu = "MingLiu";

        /// <summary>
        /// 微软雅黑。
        /// </summary>
        public static string MicrosoftYaHei = "Microsoft YaHei";

        /// <summary>
        /// 隶书。
        /// </summary>
        public static string LiSu = "LiSu";

        /// <summary>
        /// 华文彩云。
        /// </summary>
        public static string STCaiyun = "STCaiyun";

        /// <summary>
        /// 华文琥珀。
        /// </summary>
        public static string STHupo = "STHupo";

        /// <summary>
        /// 华文隶书。
        /// </summary>
        public static string STLiti = "STLiti";

        /// <summary>
        /// 华文新魏。
        /// </summary>
        public static string STXinwei = "STXinwei";

        /// <summary>
        /// 华文行楷。
        /// </summary>
        public static string STXingkai = "STXingkai";

        /// <summary>
        /// 幼园。
        /// </summary>
        public static string YouYuan = "YouYuan";

        /// <summary>
        /// Arial。
        /// </summary>
        public static string Arial = "Arial";

        /// <summary>
        /// Arial Black。
        /// </summary>
        public static string ArialBlack = "Arial Black";

        /// <summary>
        /// Comic Sans MS。
        /// </summary>
        public static string ComicSansMS = "Comic Sans MS";

        /// <summary>
        /// Courier New。
        /// </summary>
        public static string CourierNew = "Courier New";

        /// <summary>
        /// Georgia。
        /// </summary>
        public static string Georgia = "Georgia";

        /// <summary>
        /// Lucida Sans Unicode。
        /// </summary>
        public static string LucidaSansUnicode = "Lucida Sans Unicode";

        /// <summary>
        /// Times New Roman。
        /// </summary>
        public static string TimesNewRoman = "Times New Roman";

        /// <summary>
        /// Trebuchet MS。
        /// </summary>
        public static string TrebuchetMS = "Trebuchet MS";

        /// <summary>
        /// Verdana。
        /// </summary>
        public static string Verdana = "Verdana";

        public static IList<string> _source;
        public static IEnumerable<string> Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new List<string>();
                    _source.Add(FontFamilies.Arial);
                    _source.Add(FontFamilies.ArialBlack);
                    _source.Add(FontFamilies.ComicSansMS);
                    _source.Add(FontFamilies.CourierNew);
                    _source.Add(FontFamilies.Default);
                    _source.Add(FontFamilies.FangSong);
                    _source.Add(FontFamilies.Georgia);
                    _source.Add(FontFamilies.KaiTi);
                    _source.Add(FontFamilies.LiSu);
                    _source.Add(FontFamilies.LucidaSansUnicode);
                    _source.Add(FontFamilies.MicrosoftJhengHei);
                    _source.Add(FontFamilies.MicrosoftYaHei);
                    _source.Add(FontFamilies.MingLiu);
                    _source.Add(FontFamilies.NSimsun);
                    _source.Add(FontFamilies.SimHei);
                    _source.Add(FontFamilies.Simsun);
                    _source.Add(FontFamilies.STCaiyun);
                    _source.Add(FontFamilies.STHupo);
                    _source.Add(FontFamilies.STLiti);
                    _source.Add(FontFamilies.STXingkai);
                    _source.Add(FontFamilies.STXinwei);
                    _source.Add(FontFamilies.TimesNewRoman);
                    _source.Add(FontFamilies.TrebuchetMS);
                    _source.Add(FontFamilies.Verdana);
                    _source.Add(FontFamilies.YouYuan);
                }
                return _source;
            }
        }
    }
}
