
using Eenova.Chart.Helpers;
using Eenova.Chart.Elements;
namespace Eenova.Chart.Interfaces
{
    public interface IAxis : INumbericTicks, IAxisLabelsPosition, IAxisTitlePosition, ITicksStroke, ITextTicks, IFormat
    {
        AxisLabels Labels { get; }
        Title Title { get; }
        CoordConverter CoordConverter { get; }
        DataType DataType { get; }
    }
}
