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

namespace Eenova.Chart.bak
{
    public class bak
    {
        #region LoadBars

        private void LoadBars()
        {
            this.ClearChilren();
            if (this.AxisX == null || this.AxisY == null)
            {
                this.ClearBars();
                return;
            }

            //var xValues = this.AxisX.Convert(DataPoints.XValues);
            //var yValues = this.AxisY.Convert(DataPoints.YValues);

            DataPoints.OrderPoints(this.OrderType);
            var xValues = this.AxisX.Convert(DataPoints.OrderedXValues);
            var yValues = this.AxisY.Convert(DataPoints.OrderedYValues);

            if (xValues == null || yValues == null)
            {
                this.ClearBars();
                return;
            }

            var valuePoint = new Point(); ;
            int pointIndex = 0;
            for (int i = 0; i < xValues.Count; i++)
            {
                valuePoint.X = xValues[i];
                valuePoint.Y = yValues[i];
                if (double.IsNaN(valuePoint.X) || double.IsNaN(valuePoint.Y))
                    continue;

                this.AddBars(valuePoint, pointIndex);
                pointIndex++;
            }
        }

        private void ClearBars()
        {
            _barPanel.Children.Clear();
            _barEffect.Children.Clear();
        }

        private void AddBars(Point point, int count)
        {
            while (count >= _barPanel.Children.Count)
            {
                var rect = new Rectangle();
                rect.SetBinding(Shape.FillProperty, new Binding("Stroke") { Source = this });
                _barPanel.Children.Add(rect);
            }

            var index = ((Panel)this.Parent).Children.IndexOf(this) - 1;
            var width = this.AxisX.MainTicks[1] * 0.75 / (((Panel)this.Parent).Children.Count - 1);

            var bar = _barPanel.Children[count] as FrameworkElement;
            bar.Height = point.Y;
            bar.Width = width;
            Canvas.SetLeft(bar, point.X - width * index);

            //var effect = new EffectRect() { Height = point.Y, Width = width, Visibility = Visibility.Visible };
            //((Panel)this.SelectedEffect).Children.Add(effect);
            //Canvas.SetLeft(effect, point.X - width * index);
        }

        #endregion


        //#region 数据线加载显示移除逻辑

        ///// <summary>
        ///// 集合中添加了项之后。
        ///// </summary>
        ///// <param name="links"></param>
        //private void AddItems(IEnumerable links)
        //{
        //    if (links == null)
        //        return;

        //    foreach (DataLink item in links)
        //    {
        //        var axisY = this.FindAxisY(item.LinkedY);
        //        if (!AxisX.IsDataTypeMatch(item.XDataType) ||
        //            !axisY.IsDataTypeMatch(item.YDataType))
        //        {
        //            MessageBox.Show("添加的数据线数据类型与坐标轴数据类型不匹配，请重新设置后再添加。");
        //            this.Dispatcher.BeginInvoke(() => this.DataLinks.Remove(item));
        //            return;
        //        }

        //        item.AxisX = AxisX;
        //        item.AxisY = axisY;
        //        item.ParentPlotArea = this;

        //        if (item.LinkedY == PlotY.Y1 || item.LinkedY == PlotY.Y3)
        //        {
        //            this.TopLinesHost.Children.Add(item);
        //        }
        //        else
        //        {
        //            this.BottomLinesHost.Children.Add(item);
        //        }

        //        this.DataLinks.Load();
        //    }
        //}

        ///// <summary>
        ///// 集合中的项被清空之后。
        ///// </summary>
        //private void ClearItems()
        //{
        //    var links = new List<DataLink>();
        //    foreach (UIElement element in TopLinesHost.Children)
        //    {
        //        if (element is DataLink)
        //            links.Add(element as DataLink);
        //    }

        //    foreach (UIElement element in BottomLinesHost.Children)
        //    {
        //        if (element is DataLink)
        //            links.Add(element as DataLink);
        //    }

        //    this.RemoveItems(links);
        //}

        ///// <summary>
        ///// 集合中的项被删之后。
        ///// </summary>
        ///// <param name="links"></param>
        //private void RemoveItems(IEnumerable links)
        //{
        //    if (links == null)
        //        return;

        //    foreach (DataLink link in links)
        //    {
        //        link.Clean();
        //        var parent = link.Parent as Panel;
        //        if (parent != null)
        //            parent.Children.Remove(link);
        //    }
        //}

        //void DataLinks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    //if (e.Action == NotifyCollectionChangedAction.Add)
        //    //{
        //    //    this.AddItems(e.NewItems);
        //    //}
        //    //else if (e.Action == NotifyCollectionChangedAction.Remove)
        //    //{
        //    //    this.RemoveItems(e.OldItems);
        //    //}
        //    //else if (e.Action == NotifyCollectionChangedAction.Reset)
        //    //{
        //    //    this.ClearItems();
        //    //}
        //}

        //#endregion 数据线加载显示移除逻辑

    }
}
