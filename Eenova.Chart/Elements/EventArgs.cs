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
using System.Windows;

namespace Eenova.Chart.Elements
{
    public class ChildRemovedEventArgs : EventArgs
    {
        public UIElement RemovedChild { get; private set; }

        internal ChildRemovedEventArgs(UIElement removedChild)
        {
            RemovedChild = removedChild;
        }
    }
}
