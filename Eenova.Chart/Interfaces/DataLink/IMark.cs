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
    public interface IMark : IElement
    {
        Visibility MarkVisibility { get; set; }
        ShapeType MarkType { get; set; }
        double MarkSize { get; set; }
        Brush MarkBrush { get; set; }
    }
}
