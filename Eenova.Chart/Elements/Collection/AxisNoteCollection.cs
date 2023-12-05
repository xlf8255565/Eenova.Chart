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

namespace Eenova.Chart.Elements
{
    public class AxisNoteCollection : ChildCollection<AxisNote>
    {
        internal PlotArea ParentPlotArea { get; private set; }

        internal AxisNoteCollection(PlotArea plotArea)
        {
            ParentPlotArea = plotArea;
        }

        protected override void RegisterItem(AxisNote item)
        {
            if (item != null)
            {
                ParentPlotArea.NotesHost.Children.Add(item);
                item.AxisX = ParentPlotArea.AxisX;
                item.ToDelete += new EventHandler(Item_ToDelete);
                item.Load();
                //((IPart) item).Load();
            }
        }

        protected override void UnregisterItem(AxisNote item)
        {
            if (item != null)
            {
                item.ToDelete -= new EventHandler(Item_ToDelete);
                ParentPlotArea.NotesHost.Children.Remove(item);
                item.AxisX = null;
            }
        }

        private void Item_ToDelete(object sender, EventArgs e)
        {
            var item = sender as AxisNote;
            if (item != null && this.Contains(item))
            {
                this.Remove(item);
            }
        }
    }
}
