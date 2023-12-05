using System.Windows;

namespace Eenova.Chart.Interfaces
{
    public interface IPlotAreaPosition : IElement
    {
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        Thickness Margin { get; set; }
        double Length { get; set; }
        double TopHeight { get; set; }
        double BottomHeight { get; set; }
    }
}
