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
using System.Windows.Data;
using System.Windows.Controls.Primitives;

namespace Eenova.Chart.Controls
{
    public class BindingComboBox : ComboBox
    {
        Binding _binding;

        public BindingComboBox()
        {
            this.SelectionChanged += new SelectionChangedEventHandler(BindingComboBox_SelectionChanged);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var be = this.GetBindingExpression(BindingComboBox.SelectedValueProperty);
            if (be != null)
                _binding = be.ParentBinding;
        }

        void BindingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
            {
                //this.SelectedValue = default(int);
                if (_binding != null)
                    this.SetBinding(Selector.SelectedValueProperty, _binding);
            }
        }
    }
}
