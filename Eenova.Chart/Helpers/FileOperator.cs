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
using System.IO;
using System.Windows.Controls;

namespace Eenova.Chart.Helpers
{
    static class FileOperator
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <param name="value">要保存的内容</param>
        public static void SaveFile(string ext, string value)
        {
            var fileContent = System.Text.UTF8Encoding.UTF8.GetBytes(value);
            FileOperator.SaveFile(ext, fileContent);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <param name="value">要保存的内容</param>
        public static void SaveFile(string ext, byte[] value)
        {
            FileOperator.SaveFile(ext, s => s.Write(value, 0, value.Length));
        }

        public static void SaveFile(string ext, Action<Stream> write)
        {
            var dialog = new SaveFileDialog()
            {
                DefaultExt = ext,
                Filter = string.IsNullOrWhiteSpace(ext) ? "All Files(*.*)|*.*" : string.Format("{0}文件(*.{0})|*.{0}", ext),
                FilterIndex = 2
            };

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                using (var stream = dialog.OpenFile())
                {
                    if (write != null)
                        write(stream);
                }
            }
        }

        public static string OpenFile(string ext)
        {
            string value = string.Empty;
            Action<Stream> read = s => { value = new StreamReader(s).ReadToEnd(); };
            FileOperator.OpenFile(ext, read);
            return value;
        }

        public static void OpenFile(string ext, Action<Stream> read)
        {
            var dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = string.IsNullOrWhiteSpace(ext) ? "All Files(*.*)|*.*" : string.Format("{0}文件(*.{0})|*.{0}", ext),
                FilterIndex = 2
            };

            var result = dialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            if (dialog.File == null)
                return;

            using (var stream = dialog.File.OpenRead())
            {
                if (read != null)
                    read(stream);
            }
        }
    }
}
