using Eenova.Chart.Elements;

namespace Eenova.Chart.Interfaces
{
    public interface ILegend : IBorder, IPosition, IFont, ILegendAlignment
    {
        DataLinkCollection DataLinks { get; set; }
    }
}
