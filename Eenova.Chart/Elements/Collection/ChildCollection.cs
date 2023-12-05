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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Eenova.Chart.Elements
{
    public abstract class ChildCollection<T> : ObservableCollection<T>
    {
        protected abstract void RegisterItem(T item);

        protected abstract void UnregisterItem(T item);

        protected override void InsertItem(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (this.Contains(item))
                return;

            this.RegisterItem(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            this.UnregisterItem(this[index]);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (this.Contains(item))
                return;

            this.UnregisterItem(this[index]);
            this.RegisterItem(item);
            base.SetItem(index, item);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                this.UnregisterItem(item);
            }

            base.ClearItems();
        }

    }
}
