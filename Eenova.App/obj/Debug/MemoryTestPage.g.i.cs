﻿#pragma checksum "E:\work\Chart\Eenova.App\Eenova.App\MemoryTestPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7FD25E0E18DC95B88F5C9965FD9AE9EA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Eenova.App {
    
    
    public partial class MemoryTestPage : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button NewButton;
        
        internal System.Windows.Controls.Button ClearButton;
        
        internal System.Windows.Controls.Border ChartContainer;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Eenova.App;component/MemoryTestPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.NewButton = ((System.Windows.Controls.Button)(this.FindName("NewButton")));
            this.ClearButton = ((System.Windows.Controls.Button)(this.FindName("ClearButton")));
            this.ChartContainer = ((System.Windows.Controls.Border)(this.FindName("ChartContainer")));
        }
    }
}
