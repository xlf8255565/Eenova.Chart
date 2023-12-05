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
using System.Windows;
using System.Windows.Data;
using Eenova.Chart.Setter;
using System;
using Eenova.Chart.Helpers;

namespace Eenova.Chart.Elements
{
    public class AxisY : Axis
    {
        public MarkupArea MarkupArea { get; private set; }
        public MarkupLine MarkupLine { get; private set; }
        public AlarmBoard AlarmBoard { get; private set; }
        public VerticalNote VerticalNote { get; private set; }

        protected override AxisType AxisType
        {
            get { return AxisType.Y; }
        }

        protected override double FixY
        {
            get
            {
                int move = (int)(this.StrokeThickness / 2);
                return FixPoint == AxisFixPoint.StartPoint ? move : -move;
            }
        }

        protected override Point StartPoint
        {
            get { return new Point(PositionPresenter.ActualWidth / 2, PositionPresenter.ActualHeight); }
        }

        protected override Point EndPoint
        {
            get { return new Point(PositionPresenter.ActualWidth / 2, 0); }
        }

        public AxisY()
            : base()
        {
            this.DefaultStyleKey = typeof(AxisY);
        }

        protected override void InitParts()
        {
            MarkupArea = new MarkupArea() { Axis = this };
            MarkupLine = new MarkupLine() { Axis = this };
            AlarmBoard = new AlarmBoard(this);
            this.VerticalNote = new VerticalNote() { Axis = this };

            base.InitParts();
        }

        protected override void Load()
        {
            this.VerticalNote.Load();

            base.Load();
        }

        protected override void LoadSetter()
        {
            base.LoadSetter();

            var areaSetter = new MarkupAreaSetter { DataContext = MarkupArea };
            areaSetter.SetBinding(MarkupAreaSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("标识区", areaSetter);

            var lineSetter = new MarkupLineSetter { DataContext = MarkupLine };
            lineSetter.SetBinding(MarkupLineSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("标识线", lineSetter);

            SettingWindow.Add("报警范围", new AlarmBoardSetter { DataContext = AlarmBoard });
        }

        protected override void SetTitleAlignment()
        {
            switch (this.TitleAlignment)
            {
                case AxisAlignment.TopOrLeft:
                    this.Title.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case AxisAlignment.Center:
                    this.Title.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case AxisAlignment.BottomOrRight:
                    this.Title.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
        }

        protected override DataType GetLinkDataType(DataLink link)
        {
            return link.YDataType;
        }

        internal override IList<object> GetLinkData(DataLink link)
        {
            return link.DataPoints.YValues;
        }

        protected override AxisLabels CreateLabels()
        {
            return new AxisLabelsY();
        }

        protected override GridLine CreateGridLine()
        {
            return new GridLineY();
        }

        internal override double Left
        {
            get { return this.TopTitlePresenter.ActualWidth + this.TopLabelsPresenter.ActualWidth; }
        }

        internal override double Top
        {
            get { return this.Labels.LabelSize.Height / 2; }
        }

        internal override double Right
        {
            get { return this.BottomTitlePresenter.ActualWidth + this.BottomLabelsPresenter.ActualWidth; }
        }

        internal override double Bottom
        {
            get { return this.Labels.LabelSize.Height / 2; }
        }

        protected override void LoadMarkups()
        {
            MarkupArea.Load();
            MarkupLine.Load();
            AlarmBoard.Load();
        }

        internal bool IsRightSide { get; set; }

        internal override void FixPosition()
        {
            if (this.IsFixPosition)
            {
                this.Margin = new Thickness();
                return;
            }

            if (this.BindingAxis == null)
                return;

            var values = new List<object>();
            if (this.BindingAxis.DataType == DataType.Text)
            {
                values.Add(this.TextPosition);
            }
            else if (this.BindingAxis.DataType == DataType.DateTime)
            {
                values.Add(TimeHelper.GetTime(this.NumericPosition));
            }
            else
            {
                values.Add(this.NumericPosition);
            }

            var convertValues = this.BindingAxis.Convert(values);
            var value = convertValues != null && convertValues.Count > 0 ? convertValues[0] : 0;
            if (double.IsNaN(value) || value < 0)
            {
                value = 0;
            }
            else if (value > this.BindingAxis.Length)
            {
                value = this.BindingAxis.Length;
            }

            value = this.IsRightSide ? this.BindingAxis.Length - value : value;
            value = this.BindingAxis.IsDesc ? this.BindingAxis.Length - value : value;
            this.Margin = new Thickness(Math.Round(value), 0, 0, 0);
        }

        protected override void OnAlarmSubAxisChanged(Axis oldValue, Axis newValue)
        {
            base.OnAlarmSubAxisChanged(oldValue, newValue);
            if (oldValue != null)
            {
                if (oldValue.ExtendAlarms.Contains(this.AlarmBoard))
                {
                    oldValue.ExtendAlarms.Remove(this.AlarmBoard);
                }
            }
            if (newValue != null)
            {
                if (newValue.ExtendAlarms.Contains(this.AlarmBoard) == false)
                {
                    newValue.ExtendAlarms.Add(this.AlarmBoard);
                }
            }

            if (AlarmBoard.IsAutoExtend == false)
            {
                AlarmBoard.Load();
            }
        }
    }
}
