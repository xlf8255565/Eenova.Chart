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
    /// 坐标轴标题的位置。
    /// </summary>
    public interface IAxisTitlePosition : IElement
    {
        AxisAlignment TitleAlignment { get; set; }
        AxisLocation TitleLocation { get; set; }
    }
}
