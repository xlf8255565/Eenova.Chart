﻿#pragma checksum "E:\work\Chart\Eenova.App\Eenova.Chart\Setter\Markup\MarkupLineSetter.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40106311756DA21C50CDA6D8DE386DD8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Eenova.Chart.Controls;
using Eenova.Chart.Setter;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Eenova.Chart.Setter {
    
    
    public partial class MarkupLineSetter : Eenova.Chart.Setter.BaseSetter {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid LayoutSetter;
        
        internal System.Windows.Controls.ListBox lbItems;
        
        internal System.Windows.Controls.NumericUpDown nbPosition;
        
        internal Eenova.Chart.Controls.SpanDateTimePicker tpPosition;
        
        internal Eenova.Chart.Controls.StrokeStyleComboBox cbStyle;
        
        internal Eenova.Chart.Controls.ColorPicker cpBrush;
        
        internal Eenova.Chart.Controls.StrokeThicknessComboBox cbThickness;
        
        internal System.Windows.Controls.Button btnAdd;
        
        internal System.Windows.Controls.Button btnRemove;
        
        internal Eenova.Chart.Controls.GroupBox LayoutNotSupport;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Eenova.Chart;component/Setter/Markup/MarkupLineSetter.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.LayoutSetter = ((System.Windows.Controls.Grid)(this.FindName("LayoutSetter")));
            this.lbItems = ((System.Windows.Controls.ListBox)(this.FindName("lbItems")));
            this.nbPosition = ((System.Windows.Controls.NumericUpDown)(this.FindName("nbPosition")));
            this.tpPosition = ((Eenova.Chart.Controls.SpanDateTimePicker)(this.FindName("tpPosition")));
            this.cbStyle = ((Eenova.Chart.Controls.StrokeStyleComboBox)(this.FindName("cbStyle")));
            this.cpBrush = ((Eenova.Chart.Controls.ColorPicker)(this.FindName("cpBrush")));
            this.cbThickness = ((Eenova.Chart.Controls.StrokeThicknessComboBox)(this.FindName("cbThickness")));
            this.btnAdd = ((System.Windows.Controls.Button)(this.FindName("btnAdd")));
            this.btnRemove = ((System.Windows.Controls.Button)(this.FindName("btnRemove")));
            this.LayoutNotSupport = ((Eenova.Chart.Controls.GroupBox)(this.FindName("LayoutNotSupport")));
        }
    }
}

