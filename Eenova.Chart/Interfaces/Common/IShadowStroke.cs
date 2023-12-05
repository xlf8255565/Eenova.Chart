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
    /// 有阴影的边线。
    /// </summary>
    public interface IShadowStroke : IStroke
    {
        Visibility ShadowVisibility { get; set; }
    }
}
