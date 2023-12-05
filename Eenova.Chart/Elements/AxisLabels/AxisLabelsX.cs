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
using System.Windows.Controls;

namespace Eenova.Chart.Elements
{
    public class AxisLabelsX : AxisLabels
    {
        public AxisLabelsX()
            : base()
        {
            _itemsHost.Orientation = Orientation.Horizontal;
        }

        protected override void SetLabelSize(FrameworkElement label, double interval)
        {
            label.Width = interval;
        }

        protected override int TransLabelIndex(int index)
        {
            return this.IsDesc ? this.Source.Count - 1 - index : index;
        }
    }
}
