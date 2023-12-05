using System.Windows;
using System.Windows.Media;

namespace Eenova.Chart.Interfaces
{
    /// <summary>
    /// 普通线条。
    /// </summary>
    public interface IStroke : IElement
    {
        Visibility StrokeVisibility { get; set; }
        string StrokeStyle { get; set; }
        double StrokeThickness { get; set; }
        Brush Stroke { get; set; }
    }
}
