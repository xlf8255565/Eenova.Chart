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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Eenova.Chart.Controls;
using Eenova.Chart.Setter;

namespace Eenova.Chart.Elements
{
    public abstract class UIControl : Control
    {
        #region field

        private static int _zIndex = 0;
        private bool _isSettingItemsLoaded;
        private bool _isSettingWindowOpen;//设置窗口是否打开。
        private bool _isLeftMouseClick;//计算双击事件。
        private DispatcherTimer _timer;//计算双击的计数器。

        #endregion field

        #region property

        protected SettingWindow SettingWindow { get; private set; }
        protected ImproveContextMenu ContextMenu { get; private set; }
        protected virtual string SettingTitle { get { return null; } }
        protected virtual bool HasContextMenu { get { return true; } }

        #endregion

        public UIControl()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(300);

            this.LoadMenu();

            this.Loaded += new RoutedEventHandler(UIControl_Loaded);
        }

        void UIControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetEvent(this.IsHitEnable);
            this.Unloaded += new RoutedEventHandler(UIControl_Unloaded);
        }

        void UIControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.SetEvent(false);
            this.Unloaded -= new RoutedEventHandler(UIControl_Unloaded);
            this.SetEvent(false);
        }

        #region public method

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //this.LoadControls();
            //this.SetEvent(this.IsHitEnable);
        }

        #endregion public method

        #region protected method

        protected virtual void LoadMenu()
        {
            ContextMenu = new ImproveContextMenu();

            var item = new ImproveMenuItem { Header = SettingTitle };
            item.Click += (s, e) => this.ShowSettingWindow();
            ContextMenu.Items.Add(item);
        }

        //protected virtual void LoadControls()
        //{
        //var element = this.GetTemplateChild("SelctedEffect") as UIElement;
        //if (element != null)
        //{
        //    SelectedEffect = element;
        //}
        //}

        protected abstract void LoadSetter();

        #endregion protected method

        #region private method

        protected bool CheckDoubleClick()
        {
            if (!_isLeftMouseClick)
            {
                _timer.Start();
                _isLeftMouseClick = true;
                return false;
            }

            this.ShowSettingWindow();
            return true;
        }

        internal void Select()
        {
            _zIndex++;
            Canvas.SetZIndex(this, _zIndex);

            this.Focus();

            if (SelectedEffect != null)
            {
                SelectedEffect.Visibility = Visibility.Visible;
            }
        }

        internal void Deselect()
        {
            Canvas.SetZIndex(this, 1);
            if (ContextMenu.IsOpen || _isSettingWindowOpen)
                return;

            if (SelectedEffect != null)
            {
                SelectedEffect.Visibility = Visibility.Collapsed;
            }
        }

        private void SetEvent(bool enable)
        {
            if (enable)
            {
                if (SettingWindow != null)
                {
                    SettingWindow.Closed += new EventHandler(Window_Closed);
                }
                _timer.Tick += new EventHandler(Timer_Tick);
                if (this.HasContextMenu)
                {
                    ContextMenu.Opened += new RoutedEventHandler(ContextMenu_Opened);
                    ContextMenu.Closed += new RoutedEventHandler(ContextMenu_Closed);
                    ContextMenu.Owner = this;
                }
                this.MouseLeftButtonDown += new MouseButtonEventHandler(UIControl_MouseLeftButtonDown);
            }
            else
            {
                if (SettingWindow != null)
                {
                    SettingWindow.Closed -= new EventHandler(Window_Closed);
                }
                _timer.Tick -= new EventHandler(Timer_Tick);
                ContextMenu.Opened -= new RoutedEventHandler(ContextMenu_Opened);
                ContextMenu.Closed -= new RoutedEventHandler(ContextMenu_Closed);
                ContextMenu.Owner = null;
                this.MouseLeftButtonDown -= new MouseButtonEventHandler(UIControl_MouseLeftButtonDown);
            }
        }

        void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            this.Select();
        }

        void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            this.Select();
        }

        private void ShowSettingWindow()
        {
            if (!_isSettingItemsLoaded)
            {
                _isSettingItemsLoaded = true;
                SettingWindow = new SettingWindow { Title = SettingTitle };
                this.LoadSetter();
                SettingWindow.SelectedIndex = 0;
                SettingWindow.Closed += new EventHandler(Window_Closed);
            }

            _isSettingWindowOpen = true;
            SettingWindow.Load();
            SettingWindow.Show();
        }

        //public void CloseSettingWindow()
        //{
        //    //_settingWindow.Close();
        //    _settingWindow.OKButton_Click(null, null);
        //}

        private void Timer_Tick(object sender, EventArgs e)
        {
            _isLeftMouseClick = false;
            _timer.Stop();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (this.IsHitEnable)
            {
                this.Deselect();
            }
            base.OnLostFocus(e);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (this.IsHitEnable)
            {
                this.Select();
            }
            base.OnMouseRightButtonDown(e);
        }

        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    if (this.IsHitEnable)
        //    {
        //        this.Select();
        //        e.Handled = this.CheckDoubleClick();
        //        //e.Handled = true;
        //    }
        //}

        protected virtual void UIControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.OriginalSource);
            System.Diagnostics.Debug.WriteLine(this);
            System.Diagnostics.Debug.WriteLine(sender);
            if (this.IsHitEnable && !e.Handled)
            {
                this.Select();
                e.Handled = this.CheckDoubleClick();
                e.Handled = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _isSettingWindowOpen = false;
            this.Select();
        }

        #endregion private method

        #region IsHitEnable

        /// <summary>
        /// 点击效果右键菜单是否有效。
        /// </summary>
        public bool IsHitEnable
        {
            get { return (bool)GetValue(IsHitEnableProperty); }
            set { SetValue(IsHitEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHitEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHitEnableProperty =
            DependencyProperty.Register("IsHitEnable", typeof(bool), typeof(UIControl),
            new PropertyMetadata(true, OnIsHitEnableChanged));


        private static void OnIsHitEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as UIControl;
            source.SetEvent((bool)e.NewValue);
        }

        #endregion

        #region SelectedEffect

        /// <summary>
        /// 选中后的选中效果展示控件。
        /// </summary>
        internal UIElement SelectedEffect
        {
            get { return (UIElement)GetValue(SelectedEffectProperty); }
            set { SetValue(SelectedEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEffect.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty SelectedEffectProperty =
            DependencyProperty.Register("SelectedEffect", typeof(UIElement), typeof(UIControl), null);

        #endregion SelectedEffect
    }
}
