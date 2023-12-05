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
    public abstract class ChartChild : UIControl
    {
        public Chart ParentChart { get; internal set; }

        internal event EventHandler ToDelete;

        protected void OnToDelete()
        {
            var handle = ToDelete;
            if (handle != null)
            {
                handle.Invoke(this, new EventArgs());
            }
        }
    }
}
