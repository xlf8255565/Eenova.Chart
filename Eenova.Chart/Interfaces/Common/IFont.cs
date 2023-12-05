using System.Windows;
using System.Windows.Media;

namespace Eenova.Chart.Interfaces
{
    /// <summary>
    /// 字体。
    /// </summary>
    public interface IFont : IElement
    {
        FontFamily FontFamily { get; set; }
        double FontSize { get; set; }
        FontStyle FontStyle { get; set; }
        FontWeight FontWeight { get; set; }
        TextDecorationCollection TextDecorations { get; set; }
        Brush Foreground { get; set; }
        Brush Background { get; set; }
    }
}
