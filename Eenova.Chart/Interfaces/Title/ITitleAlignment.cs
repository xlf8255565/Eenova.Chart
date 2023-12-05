using System.Windows;
using System.Windows.Controls;

namespace Eenova.Chart.Interfaces
{
    public interface ITitleAlignment : IElement
    {
        HorizontalAlignment HorizontalContentAlignment { get; set; }
        VerticalAlignment VerticalContentAlignment { get; set; }
        double TextRotation { get; set; }
        Orientation Orientation { get; set; }
    }
}
