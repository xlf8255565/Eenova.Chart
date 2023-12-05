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
    public interface INumbericTicks : ITextTicks
    {
        bool IsLogarithm { get; set; }
        bool IsMinValueAuto { get; set; }
        bool IsMaxValueAuto { get; set; }
        bool IsMainUnitAuto { get; set; }
        bool IsSubUnitAuto { get; set; }

        double MinValue { get; set; }
        double MaxValue { get; set; }
        double MainUnit { get; set; }
        double SubUnit { get; set; }

        //double MaxData { get; }
        //double MinData { get; }
    }
}
