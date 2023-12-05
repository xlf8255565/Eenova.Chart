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
    /// <summary>
    /// 注释的文字位置。
    /// </summary>
    public interface INotePosition : IElement
    {
        NoteLocation NoteLocation { get; set; }
        double HorizontalOffset { get; set; }
        double VerticalOffset { get; set; }
    }
}
