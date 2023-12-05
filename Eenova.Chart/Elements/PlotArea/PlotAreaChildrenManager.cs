using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Eenova.Chart.Elements
{
    public class PlotAreaChildrenManager<T> where T : PlotAreaChild
    {
        PlotAreaChildCollection<T> _items;

        public PlotAreaChildrenManager(PlotAreaChildCollection<T> items)
        {
            _items = items;
            this.AddItems(_items);
            //_items.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.AddItems(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.RemoveItems(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.ClearItems();
            }
        }

        private void AddItems(IEnumerable items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    //this.AddItem(item);
                }
                _items.Load();
            }
        }

        private void RemoveItems(IEnumerable items)
        {
            if (items != null)
            {
                foreach (T link in items)
                {
                    //link.Clean();
                    //this.Remove(link);
                }
            }
        }

        private void ClearItems()
        {
            var items = new List<T>();
            foreach (var item in _items.TopContainer.Children)
            {
                if (item is T)
                {
                    items.Add((T)item);
                }
            }

            foreach (var item in _items.BottomContainer.Children)
            {
                if (item is T)
                {
                    items.Add((T)item);
                }
            }

            this.RemoveItems(items);
        }
    }
}
