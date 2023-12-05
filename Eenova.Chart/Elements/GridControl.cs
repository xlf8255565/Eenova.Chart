using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public class GridControl : UIControl
    {
        Panel _root;

        IList<UIElement> _elements;

        public GridControl()
        {
            this.DefaultStyleKey = typeof(GridControl);
            _elements = new List<UIElement>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.AddElements();
        }

        private void AddElements()
        {
            foreach (var element in _elements)
            {
                _root.Children.Add(element);
            }
            _elements.Clear();
        }

        protected override void LoadControls()
        {
            base.LoadControls();

            _root = this.GetTemplateChild("LayoutRoot") as Panel;
        }


        protected override void LoadSettingItems()
        {
        }

        internal void Clear()
        {
            if (_root == null)
                return;

            _root.Children.Clear();
        }

        internal void Add(UIElement element)
        {
            if (_root == null)
                _elements.Add(element);
            else
                _root.Children.Add(element);
        }
    }
}
