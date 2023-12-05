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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Eenova.Chart.Setter
{
    public class BaseSetter : UserControl, ISetter
    {
        protected IList<Tuple<FrameworkElement, DependencyProperty>> _bindingProperties;

        //public BaseSetter()
        //{
        //    this.Loaded += (s, e) => this.Load();
        //}

        public virtual void Load()
        {
            if (_bindingProperties == null)
            {
                _bindingProperties = new List<Tuple<FrameworkElement, DependencyProperty>>();
                this.AddBindingProperties();
            }

            BindingExpression b = null;
            foreach (var prop in _bindingProperties)
            {
                b = prop.Item1.GetBindingExpression(prop.Item2);
                if (b != null && b.ParentBinding != null)
                    prop.Item1.SetBinding(prop.Item2, b.ParentBinding);
            }
        }

        public virtual void Apply()
        {
            BindingExpression b = null;
            foreach (var prop in _bindingProperties)
            {
                b = prop.Item1.GetBindingExpression(prop.Item2);
                if (b != null)
                    b.UpdateSource();
            }
        }

        protected void AddBindingProperty(FrameworkElement element, DependencyProperty dp)
        {
            _bindingProperties.Add(new Tuple<FrameworkElement, DependencyProperty>(element, dp));
        }

        protected virtual void AddBindingProperties() { }
    }
}
