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
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Eenova.Chart.Elements
{
    public class ChartChildCollection<T> : ChildCollection<T> where T : ChartChild
    {
        internal Chart ParentChart { get; private set; }
        internal Panel Container { get; private set; }

        internal ChartChildCollection(Chart chart, Panel container)
        {
            ParentChart = chart;
            Container = container;
        }

        protected override void RegisterItem(T item)
        {
            if (item != null)
            {
                Container.Children.Add(item);
                item.ParentChart = ParentChart;
                item.ToDelete += new EventHandler(Item_ToDelete);
            }
        }

        protected override void UnregisterItem(T item)
        {
            if (item != null)
            {
                item.ToDelete -= new EventHandler(Item_ToDelete);
                Container.Children.Remove(item);
                item.ParentChart = null;
            }
        }

        private void Item_ToDelete(object sender, EventArgs e)
        {
            var item = sender as T;
            if (item != null && this.Contains(item))
            {
                this.Remove(item);
                this.ParentChart.OnChildRemoved(item);
            }
        }
    }
}
