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

namespace Eenova.Chart.Elements
{
    public abstract class PlotAreaChild : UIControl
    {
        public PlotArea ParentPlotArea { get; internal set; }

        public abstract PlotY LinkedY { get; set; }
        public abstract DataType XDataType { get; set; }
        public abstract DataType YDataType { get; set; }

        internal abstract void Load();

        internal event EventHandler ToDelete;

        internal void OnToDelete()
        {
            var handle = ToDelete;
            if (handle != null)
            {
                handle.Invoke(this, new EventArgs());
            }
        }
    }
}
