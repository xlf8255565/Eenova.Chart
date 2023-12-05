using System.Windows;

namespace Eenova.Chart.Interfaces
{
    public interface IPlotAreaDisplay : IElement
    {
        Visibility TopVisibility { get; set; }
        Visibility BottomVisibility { get; set; }

        Visibility LX1Visibility { get; set; }
        Visibility LX2Visibility { get; set; }
        Visibility LY1Visibility { get; set; }
        Visibility LY2Visibility { get; set; }
        Visibility LY3Visibility { get; set; }
        Visibility LY4Visibility { get; set; }

        bool IsXVisible { get; set; }
        bool IsY1Visible { get; set; }
        bool IsY2Visible { get; set; }
        bool IsY3Visible { get; set; }
        bool IsY4Visible { get; set; }

        VerticalAlignment XAlignment { get; set; }
    }
}
