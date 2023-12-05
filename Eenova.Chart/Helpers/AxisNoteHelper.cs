using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Controls;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Helpers
{
    class AxisNoteHelper
    {
        PlotArea _area;
        AxisNoteAddWindow _window;

        public AxisNoteHelper(PlotArea area)
        {
            _area = area;
            area.Notes.CollectionChanged += new NotifyCollectionChangedEventHandler(Notes_CollectionChanged);
            area.SizeChanged += (s, e) => this.LoadNotes();
        }

        internal void ShowNoteAddWindow()
        {
            if (_window == null)
            {
                _window = new AxisNoteAddWindow(_area.AxisX.MinValue, _area.AxisX.MaxValue);
                _window.Closed += new EventHandler(NoteAddWindow_Closed);
            }

            _window.Show(_area.AxisX.DataType);
        }

        internal void LoadNotes()
        {
            if (_area.NotesContainer == null)
                return;

            _area.NotesContainer.Children.Clear();

            foreach (var note in _area.Notes)
            {
                this.LoadNote(note);
            }
        }

        private void LoadNote(AxisNote note)
        {
            var values = new List<object>();
            if (_area.AxisX.DataType == DataType.DateTime)
            {
                values.Add(TimeHelper.GetTime(note.StartValue));
                values.Add(TimeHelper.GetTime(note.EndValue));
            }
            else
            {
                values.Add(note.StartValue);
                values.Add(note.EndValue);
            }

            var results = _area.AxisX.Convert(values);
            double left = this.SetNoteLeft(note, results);
            var right = Math.Min(results[1], _area.Length);
            note.Width = right - left;
            _area.NotesContainer.Children.Add(note);
            this.SetNoteTop(note);
        }

        private double SetNoteLeft(AxisNote note, IList<double> results)
        {
            double left;
            if (_area.AxisX.IsDesc)
            {
                left = Math.Max(_area.Length - results[1], 0);
                //Canvas.SetLeft(note, left);
            }
            else
            {
                left = Math.Max(results[0], 0);
                //Canvas.SetLeft(note, left);
            }
            return left;
        }

        private void SetNoteTop(AxisNote note)
        {
            var height = _area.VisualHeight();
            if (height < note.Top)
                note.Top = height;
        }

        void NoteAddWindow_Closed(object sender, EventArgs e)
        {
            if (!_window.DialogResult.HasValue)
                return;

            if (!_window.DialogResult.Value)
                return;

            var note = new AxisNote(_window.StartValue, _window.EndValue) { Top = _window.Offset };
            _area.Notes.Add(note);
        }

        void Notes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    foreach (AxisNote note in e.NewItems)
                    {
                        note.ToDelete += new EventHandler(Note_ToDelete);
                        note.TopChanged += new EventHandler(Note_TopChanged);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems != null)
                {
                    foreach (AxisNote note in e.OldItems)
                    {
                        note.ToDelete -= new EventHandler(Note_ToDelete);
                        note.TopChanged -= new EventHandler(Note_TopChanged);
                    }
                }
            }

            this.LoadNotes();
        }

        void Note_TopChanged(object sender, EventArgs e)
        {
            this.SetNoteTop(sender as AxisNote);
        }

        void Note_ToDelete(object sender, EventArgs e)
        {
            _area.Notes.Remove(sender as AxisNote);
            //this.Read(1);
            //this.Read(1, 4);
            //this.Read(j: 123, s: 456);
        }

        //private void Read(int s, int j = 2)
        //{
        //}
    }
}
