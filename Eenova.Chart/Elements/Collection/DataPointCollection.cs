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


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Eenova.Chart.Elements
{
    public class DataPointCollection : ObservableCollection<DataPoint>
    {
        internal IList<object> XValues
        {
            get { return (from p in this select p.XValue).ToList(); }
        }

        internal IList<object> YValues
        {
            get { return (from p in this select p.YValue).ToList(); }
        }

        internal IList<DataPoint> OrderedPoints { get; private set; }

        internal IList<object> OrderedXValues
        {
            get { return (from p in OrderedPoints select p.XValue).ToList(); }
        }

        internal IList<object> OrderedYValues
        {
            get { return (from p in OrderedPoints select p.YValue).ToList(); }
        }

        internal void OrderPoints(OrderType orderType)
        {
            if (orderType == OrderType.OrderByX)
            {
                OrderedPoints = (from p in this orderby p.XValue select p).ToList();
            }
            else if (orderType == OrderType.OrderByY)
            {
                OrderedPoints = (from p in this orderby p.YValue select p).ToList();
            }
            else
            {
                OrderedPoints = (from p in this select p).ToList();
            }
        }
    }

}
