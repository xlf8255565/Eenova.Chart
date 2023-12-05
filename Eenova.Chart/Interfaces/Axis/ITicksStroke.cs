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
    /// 刻度线。
    /// </summary>
    public interface ITicksStroke : IStroke
    {
        TicksShow MainTicksShow { get; set; }
        TicksShow SubTicksShow { get; set; }
    }
}
