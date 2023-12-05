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
using Eenova.Chart.Elements;

namespace Eenova.Chart.Behaviors
{
    public class DataLinkMovePointEventArgs:EventArgs
    {
        public DataPoint DataPoint { get; set; }
        public string OldValue { get; set; }
    }
}
