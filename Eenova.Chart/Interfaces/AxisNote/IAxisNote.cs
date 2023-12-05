using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Eenova.Chart.Interfaces
{
    public interface IAxisNote : INotePosition
    {
        Polyline PointingLine { get; }
        Polyline LeftSideLine { get; }
        Polyline RightSideLine { get; }
        Line StrokeLine { get; }
        ITitle Note { get; }

        double ArrowSize { get; set; }
        double Top { get; set; }
        double SideHeight { get; set; }
    }
}
