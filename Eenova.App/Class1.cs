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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Eenova.App
{
    public class MyWrapPanle : WrapPanel
    {
        ObservableCollection<Class1> lists;

        public MyWrapPanle()
        {
            lists = new ObservableCollection<Class1>();
            lists.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(lists_CollectionChanged);
        }

        void lists_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Class1 c in e.NewItems)
                {
                    c.LoadVisual(this.IcoType);
                    this.Children.Add(c.Visual);
                    c.Selected += new EventHandler(c_Selected);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Class1 c in e.OldItems)
                {
                    this.Children.Remove(c.Visual);
                    c.Selected -= new EventHandler(c_Selected);
                }
            }
        }

        void c_Selected(object sender, EventArgs e)
        {
            Class1 c = sender as Class1;

            //c,Name;
            //c.source;

        }

        public void Add(Class1 c)
        {
            lists.Add(c);
        }

        public IcoType IcoType
        {
            get { return (IcoType)GetValue(IcoTypeProperty); }
            set { SetValue(IcoTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IcoType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IcoTypeProperty =
            DependencyProperty.Register("IcoType", typeof(IcoType), typeof(MyWrapPanle), new PropertyMetadata(OnIcoTypeChanged));

        private static void OnIcoTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var axis = d as MyWrapPanle;
            axis.OnIcoTypeChanged((IcoType)e.OldValue, (IcoType)e.NewValue);
        }

        private void OnIcoTypeChanged(Eenova.App.IcoType oldValue, Eenova.App.IcoType newValue)
        {
            this.Children.Clear();

            foreach (Class1 c in lists)
            {
                c.LoadVisual(newValue);
                this.Children.Add(c.Visual);
            }
        }
    }

    public class Class1 : DependencyObject
    {
        public event EventHandler Selected;


        public int name { get; set; }
        public int source { get; set; }
        public int filepath { get; set; }

        public Visual Visual;

        public void LoadVisual(IcoType type)
        {
            // Visual...
            Visual.Selected += new EventHandler(Visual_Selected);
        }

        void Visual_Selected(object sender, EventArgs e)
        {
            OnSelected();
        }

        private void OnSelected()
        {
            if (Selected != null)
            {
                Selected(this, null);
            }
        }
    }

    public abstract class Visual : Control
    {
        public event EventHandler Selected;

        private void OnSelected()
        {
            if (Selected != null)
            {
                Selected(this, null);
            }
        }



        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ownerclass), new UIPropertyMetadata(0));




        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ownerclass), new UIPropertyMetadata(0));

        
    }

    public enum IcoType
    {
        a,
        b,
        c,
    }
}
