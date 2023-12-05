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
using System.Windows;
using System.Windows.Data;
using Eenova.Chart.Helpers;
using Eenova.Chart.Setter;
using System.Windows.Controls;

namespace Eenova.Chart.Elements
{
    public class AxisX : Axis
    {
        IList<AxisNote> _axisNotes = new List<AxisNote>();

        public MarkupArea TopMarkupArea { get; private set; }
        public MarkupArea BottomMarkupArea { get; private set; }
        public MarkupLine TopMarkupLine { get; private set; }
        public MarkupLine BottomMarkupLine { get; private set; }
        public AlarmBoard TopAlarmBoard { get; private set; }
        public AlarmBoard BottomAlarmBoard { get; private set; }

        protected override AxisType AxisType
        {
            get { return AxisType.X; }
        }

        protected override double FixX
        {
            get
            {
                int move = (int)(this.StrokeThickness / 2);
                return FixPoint == AxisFixPoint.StartPoint ? -move : move;
            }
        }

        protected override Point StartPoint
        {
            get { return new Point(0, PositionPresenter.ActualHeight / 2); }
        }

        protected override Point EndPoint
        {
            get { return new Point(PositionPresenter.ActualWidth, PositionPresenter.ActualHeight / 2); }
        }

        public AxisX()
        {
            this.DefaultStyleKey = typeof(AxisX);
        }

        protected override void InitParts()
        {
            TopMarkupArea = new MarkupArea() { Axis = this };
            BottomMarkupArea = new MarkupArea() { Axis = this };
            TopMarkupLine = new MarkupLine() { Axis = this };
            BottomMarkupLine = new MarkupLine() { Axis = this };
            TopAlarmBoard = new AlarmBoard(this);
            BottomAlarmBoard = new AlarmBoard(this);

            base.InitParts();
        }

        protected override void LoadSetter()
        {
            base.LoadSetter();

            var areaSetter = new MarkupAreaSetter { DataContext = TopMarkupArea };
            areaSetter.SetBinding(MarkupAreaSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("上标识区", areaSetter);

            areaSetter = new MarkupAreaSetter { DataContext = BottomMarkupArea };
            areaSetter.SetBinding(MarkupAreaSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("下标识区", areaSetter);

            var lineSetter = new MarkupLineSetter { DataContext = TopMarkupLine };
            lineSetter.SetBinding(MarkupLineSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("上标识线", lineSetter);

            lineSetter = new MarkupLineSetter { DataContext = BottomMarkupLine };
            lineSetter.SetBinding(MarkupLineSetter.DataTypeProperty, new Binding("DataType") { Source = this });
            SettingWindow.Add("下标识线", lineSetter);
        }

        protected override void SetTitleAlignment()
        {
            switch (this.TitleAlignment)
            {
                case AxisAlignment.TopOrLeft:
                    this.Title.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case AxisAlignment.Center:
                    this.Title.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case AxisAlignment.BottomOrRight:
                    this.Title.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
        }

        protected override DataType GetLinkDataType(DataLink link)
        {
            return link.XDataType;
        }

        internal override IList<object> GetLinkData(DataLink link)
        {
            return link.DataPoints.XValues;
        }

        protected override AxisLabels CreateLabels()
        {
            return new AxisLabelsX();
        }

        protected override GridLine CreateGridLine()
        {
            return new GridLineX();
        }

        internal override double Left
        {
            get { return this.Labels.LabelSize.Width / 2; }
        }

        internal override double Top
        {
            get { return this.TopTitlePresenter.ActualHeight + this.TopLabelsPresenter.ActualHeight; }
        }

        internal override double Right
        {
            get { return this.Labels.LabelSize.Width / 2; }
        }

        internal override double Bottom
        {
            get { return this.BottomTitlePresenter.ActualHeight + this.BottomLabelsPresenter.ActualHeight; }
        }

        protected override void LoadMarkups()
        {
            TopMarkupArea.Load();
            BottomMarkupArea.Load();
            TopMarkupLine.Load();
            BottomMarkupLine.Load();
            TopAlarmBoard.Load();
            BottomAlarmBoard.Load();
        }

        internal void Register(AxisNote note)
        {
            if (_axisNotes.Contains(note) == false)
            {
                _axisNotes.Add(note);
            }
        }

        internal void Unregister(AxisNote note)
        {
            if (_axisNotes.Contains(note) == true)
            {
                _axisNotes.Remove(note);
            }
        }

        protected override void Load()
        {
            foreach (var note in _axisNotes)
            {
                note.Load();
            }
            base.Load();
        }

        internal override void FixPosition()
        {
            if (this.IsFixPosition)
                return;

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

            var value = this.BindingAxis.Convert(values)[0];
            if (double.IsNaN(value) || value < 0)
            {
                value = 0;
            }
            else if (value > this.BindingAxis.Length)
            {
                value = this.BindingAxis.Length;
            }

            value = this.BindingAxis.IsDesc ? value : this.BindingAxis.Length - value;
            this.Margin = new Thickness(0, Math.Round(value), 0, 0);
        }

        protected override void OnAlarmSubAxisChanged(Axis oldValue, Axis newValue)
        {
            base.OnAlarmSubAxisChanged(oldValue, newValue);

            if (oldValue != null)
            {
                if (oldValue.ExtendAlarms.Contains(this.TopAlarmBoard))
                {
                    oldValue.ExtendAlarms.Remove(this.TopAlarmBoard);
                }
                if (oldValue.ExtendAlarms.Contains(this.BottomAlarmBoard))
                {
                    oldValue.ExtendAlarms.Remove(this.BottomAlarmBoard);
                }
            }
            if (newValue != null)
            {
                if (newValue.ExtendAlarms.Contains(this.TopAlarmBoard) == false)
                {
                    newValue.ExtendAlarms.Add(this.TopAlarmBoard);
                }
                if (newValue.ExtendAlarms.Contains(this.BottomAlarmBoard) == false)
                {
                    newValue.ExtendAlarms.Add(this.BottomAlarmBoard);
                }
            }

            if (TopAlarmBoard.IsAutoExtend == false)
            {
                TopAlarmBoard.Load();
            }

            if (BottomAlarmBoard.IsAutoExtend == false)
            {
                BottomAlarmBoard.Load();
            }
        }
    }
}
