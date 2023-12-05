using System;
using System.Collections.Specialized;
using System.Windows.Controls;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    public class UICollectionHelper<T> where T : UIControl, INotifyToDelete
    {
        UICollection<T> _itemCollection;
        Panel _parent;

        public UICollectionHelper(UICollection<T> itemCollection, Panel parent)
        {
            _parent = parent;
            _itemCollection = itemCollection;
            this.AddItems(_itemCollection);
            _itemCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(Collection_CollectionChanged);
        }

        void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.AddItems(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.RemoveItems(e.OldItems);
            }
        }

        private void RemoveItems(System.Collections.IList items)
        {
            if (items == null || _parent == null)
                return;

            foreach (T item in items)
            {
                if (_parent.Children.Contains(item))
                {
                    item.ToDelete -= new EventHandler(Item_ToDelete);
                    _parent.Children.Remove(item);
                }
            }
        }

        private void AddItems(System.Collections.IList items)
        {
            if (items == null || _parent == null)
                return;

            foreach (T item in items)
            {
                item.ToDelete += new EventHandler(Item_ToDelete);
                _parent.Children.Add(item);
            }
        }

        void Item_ToDelete(object sender, EventArgs e)
        {
            var item = sender as T;
            if (item != null && _itemCollection.Contains(item))
            {
                _itemCollection.Remove(item);
            }
        }
    }
}
