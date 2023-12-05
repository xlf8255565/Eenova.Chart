using System.Windows;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Interfaces
{
    public interface IPlotArea : IBorder//IPlotAreaDisplay,IPlotAreaPosition
    {
        Axis AxisX { get; }
        Axis AxisY1 { get; }
        Axis AxisY2 { get; }
        Axis AxisY3 { get; }
        Axis AxisY4 { get; }
        GridLine GridLineX1 { get; }
        GridLine GridLineX2 { get; }
        GridLine GridLineY1 { get; }
        GridLine GridLineY2 { get; }
        GridLine GridLineY3 { get; }
        GridLine GridLineY4 { get; }

        Visibility TopVisibility { get; set; }
        Visibility BottomVisibility { get; set; }
        VerticalAlignment XAlignment { get; set; }

        double Length { get; set; }
        double TopHeight { get; set; }
        double BottomHeight { get; set; }
        Thickness Margin { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }

        DataLinkCollection DataLinks { get; }
        AxisNoteCollection Notes { get; }
    }
}
