using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Converters
{
    /// <summary>
    /// 解析XAttribute转化成各种格式。
    /// </summary>
    class XAttributeConverter
    {
        /// <summary>
        /// 转化为Bool.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static bool? Convert2Bool(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            bool value;
            if (bool.TryParse(attribute.Value, out value))
                return value;

            return null;
        }

        /// <summary>
        /// 转化为Brush.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static Brush Convert2Brush(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            try
            {
                var color = ColorOperator.ConvertFromString(attribute.Value.Substring(1));
                return new SolidColorBrush(color);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转化为double.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static double? Convert2Double(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            double value;
            if (double.TryParse(attribute.Value, out value))
                return value;

            return null;
        }

        /// <summary>
        /// 转化为通用枚举.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static TEnum? Convert2Enum<TEnum>(XAttribute attribute) where TEnum : struct
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            TEnum value;
            if (Enum.TryParse<TEnum>(attribute.Value, true, out value))
                return value;

            return null;
        }

        /// <summary>
        /// 转化为FontFamily.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static FontFamily Convert2FontFamily(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            return new FontFamily(attribute.Value);
        }

        /// <summary>
        /// 转化为FontStyle.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static FontStyle? Convert2FontStyle(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            if (attribute.Value == FontStyles.Italic.ToString())
                return FontStyles.Italic;

            return FontStyles.Normal;
        }

        /// <summary>
        /// 转化为FontWeight.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static FontWeight? Convert2FontWeight(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            if (attribute.Value == FontWeights.Bold.ToString())
                return FontWeights.Bold;

            return FontWeights.Normal;
        }

        /// <summary>
        /// 转化为string.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static string Convert2String(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            return attribute.Value;
        }

        /// <summary>
        /// 转化为Thickness.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static Thickness? Convert2Thickness(XAttribute attribute)
        {
            if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                return null;

            var strArray = attribute.Value.Split(',');

            double value;
            var values = new List<double>();
            foreach (var str in strArray)
            {
                if (!double.TryParse(str, out value))
                    return null;

                values.Add(value);
            }

            if (values.Count == 1)
            {
                return new Thickness(values[0]);
            }
            else if (values.Count == 2)
            {
                return new Thickness(values[0], values[1], values[0], values[1]);
            }
            else if (values.Count == 4)
            {
                return new Thickness(values[0], values[1], values[2], values[3]);
            }
            else
            {
                return null;
            }
        }
    }
}
