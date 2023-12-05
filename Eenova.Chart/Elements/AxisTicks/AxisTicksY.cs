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
using System.Windows.Media;
using System.Windows.Shapes;

namespace Eenova.Chart.Elements
{
    public class AxisTicksY : AxisTicks
    {
        protected override Panel LoadRootHost()
        {
            return new StackPanel() { Orientation = Orientation.Horizontal };
        }

        protected override void FixSize()
        {
            _topFixer.Width = this.TickHeight;
            _centerFixer.Width = this.Thickness;
            _bottomFixer.Width = this.TickHeight;
        }

        protected override void SetTransform()
        {
            ((CompositeTransform)this.RenderTransform).ScaleY = this.IsDesc ? 1 : -1;
        }

        protected override void SetTickCoord(Line line, double offset, double height)
        {
            line.X1 = 0;
            line.X2 = height;
            line.Y1 = offset;
            line.Y2 = offset;
        }
    }
}
