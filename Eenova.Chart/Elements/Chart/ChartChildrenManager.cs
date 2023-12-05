using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;

namespace Eenova.Chart.Elements
{
    class ChartChildrenManager<T> where T : ChartChild
    {
        ChartChildCollection<T> _items;

        public ChartChildrenManager(ChartChildCollection<T> itemCollection)
        {
            _items = itemCollection;
            this.AddItems(_items);
            _items.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
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
                    item.ToDelete += new EventHandler(DeleteItem);
                    _items.Container.Children.Add(item);
                    item.ParentChart = _items.ParentChart;
                }
            }
        }

        private void RemoveItems(IEnumerable items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    if (_items.Container.Children.Contains(item))
                    {
                        item.ToDelete -= new EventHandler(DeleteItem);
                        _items.Container.Children.Remove(item);
                        item.ParentChart = null;
                    }
                }
            }
        }

        private void ClearItems()
        {
            var items = new List<T>();
            foreach (var item in _items.Container.Children)
            {
                if (item is T)
                {
                    items.Add((T)item);
                }
            }

            this.RemoveItems(items);
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var item = sender as T;
            if (item != null && _items.Contains(item))
            {
                _items.Remove(item);
                _items.ParentChart.OnChildRemoved(item);
            }
        }
    }
}
