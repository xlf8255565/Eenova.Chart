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
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Styler
{
    class XmlValueReader
    {
        private static Dictionary<Type, Dictionary<string, Enum>> _enumCache = new Dictionary<Type, Dictionary<string, Enum>>();
        private static Dictionary<Type, Func<string, object>> _methodCache = new Dictionary<Type, Func<string, object>>();

        public static Func<string, object> GetConvert(Type type)
        {
            if (!_methodCache.ContainsKey(type))
            {
                if (type.IsEnum)
                    _methodCache.Add(type, s => { var enums = GetEnums(type); return enums[s]; });
                else if (type.Equals(typeof(double)))
                    _methodCache.Add(type, Convert2Double);
                else if (type.Equals(typeof(Brush)))
                    _methodCache.Add(type, Convert2Brush);
                else if (type.Equals(typeof(Point)))
                    _methodCache.Add(type, Convert2Point);
                else if (type.Equals(typeof(Thickness)))
                    _methodCache.Add(type, Convert2Thickness);
                else if (type.Equals(typeof(Transform)))
                    _methodCache.Add(type, Convert2Transform);
                else if (type.Equals(typeof(bool)))
                    _methodCache.Add(type, Convert2Bool);
                else if (type.Equals(typeof(FontFamily)))
                    _methodCache.Add(type, Convert2FontFamily);
                else if (type.Equals(typeof(FontStyle)))
                    _methodCache.Add(type, Convert2FontStyle);
                else if (type.Equals(typeof(FontWeight)))
                    _methodCache.Add(type, Convert2FontWeight);
                else if (type.Equals(typeof(TextDecorationCollection)))
                    _methodCache.Add(type, Convert2TextDecorations);
                else
                    _methodCache.Add(type, null);
            }

            return _methodCache[type];
        }

        private static Dictionary<string, Enum> GetEnums(Type type)
        {
            if (!_enumCache.ContainsKey(type))
            {
                var fields = type.GetFields().Where(field => field.IsLiteral);
                var enums = new Dictionary<string, Enum>();
                foreach (var field in fields)
                {
                    enums.Add(field.Name, (Enum)field.GetValue(null));
                }
                _enumCache.Add(type, enums);
            }
            return _enumCache[type];
        }

        public static object Convert2Bool(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return false;

            bool value;
            if (bool.TryParse(attr, out value))
                return value;

            return false;
        }

        public static Brush Convert2Brush(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return null;

            try
            {
                var color = Utility.ConvertFromString(attr.Substring(1));
                return new SolidColorBrush(color);
            }
            catch
            {
                return null;
            }
        }

        public static object Convert2Double(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return double.NaN;

            double value;
            if (double.TryParse(attr, out value))
                return value;

            return double.NaN;
        }

        public static FontFamily Convert2FontFamily(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return new FontFamily(FontFamilies.Default);

            return new FontFamily(attr);
        }

        public static object Convert2FontStyle(string attr)
        {
            if (attr == FontStyles.Italic.ToString())
                return FontStyles.Italic;
            else
                return FontStyles.Normal;
        }

        public static object Convert2FontWeight(string attr)
        {
            if (attr == FontWeights.Bold.ToString())
                return FontWeights.Bold;
            else
                return FontWeights.Normal;
        }

        public static object Convert2Thickness(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return new Thickness();

            var attrs = attr.Split(',');

            double value;
            var values = new List<double>();
            foreach (var str in attrs)
            {
                if (!double.TryParse(str, out value))
                    return new Thickness();
                values.Add(value);
            }

            if (values.Count == 1)
                return new Thickness(values[0]);
            else if (values.Count == 2)
                return new Thickness(values[0], values[1], values[0], values[1]);
            else if (values.Count == 4)
                return new Thickness(values[0], values[1], values[2], values[3]);
            else
                return new Thickness();
        }

        public static object Convert2Point(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return new Point();

            var attrs = attr.Split(',');

            double value;
            var values = new List<double>();
            foreach (var str in attrs)
            {
                if (!double.TryParse(str, out value))
                    return new Point();
                values.Add(value);
            }

            if (values.Count == 1)
                return new Point(values[0], values[0]);
            else if (values.Count == 2)
                return new Point(values[0], values[1]);
            else
                return new Point();
        }

        public static Transform Convert2Transform(string attr)
        {
            if (string.IsNullOrWhiteSpace(attr))
                return new CompositeTransform();

            var attrs = attr.Split(',');

            double value;
            var values = new List<double>();
            foreach (var str in attrs)
            {
                if (!double.TryParse(str, out value))
                    return new CompositeTransform();
                values.Add(value);
            }

            if (values.Count == 1)
                return new CompositeTransform() { TranslateX = values[0], TranslateY = values[0] };
            else if (values.Count == 2)
                return new CompositeTransform() { TranslateX = values[0], TranslateY = values[1] };
            else
                return new CompositeTransform();

        }

        public static TextDecorationCollection Convert2TextDecorations(string attr)
        {
            if ((bool)Convert2Bool(attr))
                return TextDecorations.Underline;
            return null;
        }
    }
}
