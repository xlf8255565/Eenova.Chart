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
using System.Windows.Data;

namespace Eenova.Chart.Setter
{
    public class SetterModel
    {
        public FrameworkElement Element { get; set; }
        public DependencyProperty Property { get; set; }
        public BindingExpression Binding { get; set; }
    }
}