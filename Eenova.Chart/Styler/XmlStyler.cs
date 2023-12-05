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
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Styler
{
    abstract class XmlStyler
    {
        Control _element;
        protected XElement _xml;
        internal string Header { get; private set; }

        public XmlStyler(string header)
            : this(null, header)
        {
        }

        public XmlStyler(Control element, string header)
        {
            _element = element;
            this.Header = header;
        }

        public void SaveStyle()
        {
            string xml = null;
#if DEBUG
            xml = this.CreateXml().ToString();
#else
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Xml.XmlWriterSettings setting = new System.Xml.XmlWriterSettings() { Indent = false };
            using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sb, setting))
            {
                this.CreateXml().WriteTo(xw);
            }
            xml = sb.ToString();
#endif
            FileOperator.SaveFile("xml", xml);
        }

        public void ApplyStyle()
        {
            var result = FileOperator.OpenFile("xml");
            if (string.IsNullOrWhiteSpace(result))
                return;

            XElement xml;
            try
            {
                xml = XElement.Parse(result);
            }
            catch
            {
                return;
            }
            if (xml == null || xml.Name != this.Header)
                return;

            this.ReadXml(xml);
        }

        public virtual XElement CreateXml()
        {
            _xml = new XElement(this.Header);
            return _xml;
        }

        /// <summary>
        /// 从Xml中读取信息。
        /// </summary>
        /// <param name="xml"></param>
        public virtual void ReadXml(XElement xml)
        {
            _xml = xml;
            foreach (var attr in _xml.Attributes())
            {
                var name = attr.Name.LocalName;
                //通过反射获取属性。
                var pi = _element.GetType().GetProperty(name);
                if (pi == null)
                    continue;
                //根据数据类型获取转换器把string的数据转化为各种类型的。
                var convert = XmlValueReader.GetConvert(pi.PropertyType);
                var value = convert == null ? attr.Value : convert(attr.Value);
                pi.SetValue(_element, value, null);
            }
        }

        protected bool AddAttribute(Expression<Func<DependencyProperty>> expression)
        {
            return this.AddAttribute(expression, false);
        }

        protected bool AddAttribute(Expression<Func<DependencyProperty>> expression, bool foceSave)
        {
            return this.AddAttribute(expression, null, foceSave);
        }

        protected bool AddAttribute(Expression<Func<DependencyProperty>> expression, IValueConverter converter)
        {
            return this.AddAttribute(expression, converter, false);
        }

        /// <summary>
        /// 扩展添加Xml属性。
        /// </summary>
        /// <param name="expression">动态属性表达式，动态属性需要以"Property"结尾。（如：() => TextBlock.TextProperty）</param>
        protected bool AddAttribute(Expression<Func<DependencyProperty>> expression, IValueConverter converter, bool foceSave)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return this.AddAttribute(expression.Compile().Invoke(), Regex.Replace(memberExpression.Member.Name, "Property$", ""), converter, foceSave);
            else
                throw new NotImplementedException();
        }

        protected bool AddAttribute(DependencyProperty dp, string name)
        {
            return this.AddAttribute(dp, name, false);
        }

        protected bool AddAttribute(DependencyProperty dp, string name, bool foceSave)
        {
            return this.AddAttribute(dp, name, null, foceSave);
        }

        protected bool AddAttribute(DependencyProperty dp, string name, IValueConverter convert)
        {
            return this.AddAttribute(dp, name, convert, false);
        }

        /// <summary>
        /// 添加Xml属性。
        /// </summary>
        /// <param name="dp">需要添加的动态属性</param>
        /// <param name="name">写入Xml的属性名称</param>
        /// <param name="converter">数据转化（如Brush需要转化为Color保存）</param>
        /// <param name="foceSave">是否强制保存（如果不强制保存，当DP的现有值与元数据的值相同是不写入XML）</param>
        protected bool AddAttribute(DependencyProperty dp, string name, IValueConverter converter, bool foceSave)
        {
            var value = _element.GetValue(dp);//动态属性的现有值。
            var metadata = dp.GetMetadata(typeof(Control)).DefaultValue;//动态属性的元数据值。
            if (!foceSave)
            {
                if (metadata.Equals(value) ||
                    (metadata.GetType().BaseType == null && value == null))
                {
                    //如果不是强制保存的话，当值相同时不写入Xml。
                    return false;
                }
            }
            if (converter != null)
            {
                //通过convert转化数据。
                value = converter.Convert(value, null, null, null);
            }
            _xml.SetAttributeValue(name, value);

            return true;
        }
    }
}
