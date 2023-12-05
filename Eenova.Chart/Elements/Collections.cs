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


using System.Collections.ObjectModel;

namespace Eenova.Chart.Elements
{
    public class UICollection<T> : ObservableCollection<T>
    {
        public new void Add(T item)
        {
            if (this.Contains(item))
                return;

            base.Add(item);
        }
    }

    public class LabelItemCollection : UICollection<LabelItem> { }
}
