/*****************************************************************************
*   Project:        城市轨道交通
*
*   Developed by:   Jphotonics Technology.
*                   Hangzhou, China
*
*   Developers:     Jphotonics   2010-10-20
*
*
*   Copyright:      (c) 2010 Jphotonics Technology. All rights reserved.
*****************************************************************************/


using System;
using System.Windows.Controls;

namespace Eenova.Chart.Elements
{
    public class DataLinkCollection : ChildCollection<DataLink>
    {
        internal PlotArea ParentPlotArea { get; private set; }

        internal DataLinkCollection(PlotArea plotArea)
        {
            ParentPlotArea = plotArea;
        }

        internal void Load()
        {
            foreach (var item in this)
            {
                item.Load();
            }
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            this.Load();
        }

        protected override void RegisterItem(DataLink item)
        {
            if (ParentPlotArea.AxisX.IsDataTypeMatch(item.XDataType) == false)
                throw new ArgumentException("横轴数据格式不匹配。");

            var axisY = ParentPlotArea.FindAxisY(item.LinkedY);
            if (axisY.IsDataTypeMatch(item.YDataType) == false)
                throw new ArgumentException("纵轴数据格式不匹配。");

            if (item.LinkedY == PlotY.Y1 ||
                item.LinkedY == PlotY.Y3)
            {
                ParentPlotArea.TopLinesHost.Children.Add(item);
            }
            else
            {
                ParentPlotArea.BottomLinesHost.Children.Add(item);
            }
            item.ParentPlotArea = ParentPlotArea;

            item.ToDelete += new EventHandler(Item_ToDelete);
            item.LinkedYChanged += new EventHandler(Item_LinkedYChanged);
        }

        protected override void UnregisterItem(DataLink item)
        {
            item.ToDelete -= new EventHandler(Item_ToDelete);
            item.LinkedYChanged -= new EventHandler(Item_LinkedYChanged);

            item.ParentPlotArea = null;
            var parent = item.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(item);
            }
        }

        private void Item_ToDelete(object sender, EventArgs e)
        {
            var item = sender as DataLink;
            if (item != null && this.Contains(item))
            {
                this.Remove(item);
                this.ParentPlotArea.OnChildRemoved(item);
            }
        }

        private void Item_LinkedYChanged(object sender, EventArgs e)
        {
            var item = sender as DataLink;
            if (item != null && this.Contains(item))
            {
                this.Remove(item);
                this.Add(item);
            }
        }
    }
}
