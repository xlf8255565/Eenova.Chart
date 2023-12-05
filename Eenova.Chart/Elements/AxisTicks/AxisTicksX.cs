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
    public class AxisTicksX : AxisTicks
    {
        protected override Panel LoadRootHost()
        {
            return new StackPanel() { Orientation = Orientation.Vertical };
        }

        protected override void FixSize()
        {
            _topFixer.Height = this.TickHeight;
            _centerFixer.Height = this.Thickness;
            _bottomFixer.Height = this.TickHeight;
        }

        protected override void SetTransform()
        {
            ((CompositeTransform)this.RenderTransform).ScaleX = this.IsDesc ? -1 : 1;
        }

        protected override void SetTickCoord(Line line, double offset, double height)
        {
            line.X1 = offset;
            line.X2 = offset;
            line.Y1 = 0;
            line.Y2 = height;
        }
    }
}
