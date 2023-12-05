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


using System.ComponentModel;

namespace Eenova.Chart.Elements
{
    public class DataPoint : INotifyPropertyChanged
    {
        object _xValue;
        object _yValue;

        public object XValue
        {
            get { return _xValue; }
            set
            {
                if (_xValue != value)
                {
                    _xValue = value;
                    this.OnPropertyChanged("XValue");
                }
            }
        }

        public object YValue
        {
            get { return _yValue; }
            set
            {
                if (_yValue != value)
                {
                    _yValue = value;
                    this.OnPropertyChanged("YValue");
                }
            }
        }

        public DataPoint()
        {
        }

        public DataPoint(object x, object y)
        {
            XValue = x;
            YValue = y;
        }

        public override string ToString()
        {
            return _xValue + "," + _yValue;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }
}
