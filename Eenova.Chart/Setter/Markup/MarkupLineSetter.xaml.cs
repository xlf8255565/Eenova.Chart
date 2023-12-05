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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Setter
{
    public partial class MarkupLineSetter : BaseSetter
    {
        public MarkupLineSetter()
        {
            InitializeComponent();
        }

        public override void Load()
        {
            base.Load();

            var item = this.lbItems.SelectedItem;
            this.lbItems.SelectedItem = null;
            this.lbItems.SelectedItem = item;
        }

        public override void Apply()
        {

            var item = this.lbItems.SelectedItem as MarkupLineItem;
            if (item != null)
            {
                var brush = this.cpBrush.SelectedBrush;
                var position = this.nbPosition.Value;
                var style = (string)this.cbStyle.SelectedItem ?? item.Style;
                var thickness = this.cbThickness.SelectedItem != null ? (double)this.cbThickness.SelectedItem : item.Thickness;
                item.Brush = brush;
                item.Position = position;
                item.Style = style;
                item.Thickness = thickness;
                this.lbItems.SelectedItem = item;
            }

            base.Apply();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = this.lbItems.SelectedItem as MarkupLineItem;
            var items = this.lbItems.ItemsSource as MarkupLineItemCollection;
            if (items == null || item == null)
                return;
            items.Remove(item);
            this.lbItems.SelectedItem = items.Count > 0 ? items[0] : null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var items = this.lbItems.ItemsSource as MarkupLineItemCollection;
            if (items == null)
                return;
            var item = new MarkupLineItem
            {
                Brush = this.cpBrush.SelectedBrush,
                Position = this.nbPosition.Value,
            };
            if (this.cbStyle.SelectedItem != null)
                item.Style = (string)this.cbStyle.SelectedItem;

            if (this.cbThickness.SelectedItem != null)
                item.Thickness = (double)this.cbThickness.SelectedItem;

            items.Add(item);
            this.lbItems.SelectedItem = item;
        }


        public DataType DataType
        {
            get { return (DataType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.Register("DataType", typeof(DataType), typeof(MarkupLineSetter),
            new PropertyMetadata(DataType.Numberic, OnDataTypeChanged));

        private static void OnDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as MarkupLineSetter;

            source.LayoutSetter.Visibility = Visibility.Visible;
            source.LayoutNotSupport.Visibility = Visibility.Collapsed;

            switch ((DataType)e.NewValue)
            {
                case DataType.Numberic:
                    source.nbPosition.Visibility = Visibility.Visible;
                    source.tpPosition.Visibility = Visibility.Collapsed;
                    break;
                case DataType.DateTime:
                    source.nbPosition.Visibility = Visibility.Collapsed;
                    source.tpPosition.Visibility = Visibility.Visible;
                    break;
                case DataType.Text:
                    source.LayoutSetter.Visibility = Visibility.Collapsed;
                    source.LayoutNotSupport.Visibility = Visibility.Visible;
                    break;
            }
        }

    }
}
