﻿#pragma checksum "E:\work\Chart\Eenova.App\Eenova.Chart\Setter\Alarm\AlarmBoardSetter.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EE901B3AB6AB68E7BF38300640077990"
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
    
    
    public partial class AlarmBoardSetter : Eenova.Chart.Setter.BaseSetter {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid LayoutSetter;
        
        internal Eenova.Chart.Controls.BindingComboBox cbAlarmGroup;
        
        internal System.Windows.Controls.ListBox lbItems;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Eenova.Chart;component/Setter/Alarm/AlarmBoardSetter.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.LayoutSetter = ((System.Windows.Controls.Grid)(this.FindName("LayoutSetter")));
            this.cbAlarmGroup = ((Eenova.Chart.Controls.BindingComboBox)(this.FindName("cbAlarmGroup")));
            this.lbItems = ((System.Windows.Controls.ListBox)(this.FindName("lbItems")));
        }
    }
}

