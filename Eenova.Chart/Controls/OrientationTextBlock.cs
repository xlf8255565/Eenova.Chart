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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Data;

namespace Eenova.Chart.Controls
{
    public class OrientationTextBlock : Control
    {
        WrapPanel _panel;

        public OrientationTextBlock()
        {
            IsTabStop = false;

            var templateXaml =
                @"<ControlTemplate xmlns='http://schemas.microsoft.com/client/2007' " +
                                  "xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' " +
                                  "xmlns:toolkit='http://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit' >" +
                    "<Grid>" +
                        "<toolkit:WrapPanel x:Name=\"WrapPanel\" Margin=\"0\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>" +
                    "</Grid>" +
                "</ControlTemplate>";

            Template = (ControlTemplate)XamlReader.Load(templateXaml);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LoadControls();
            this.CreateText();
        }


        private void LoadControls()
        {
            _panel = GetTemplateChild("WrapPanel") as WrapPanel;

            var b = new Binding("Orientation") { Source = this };
            _panel.SetBinding(WrapPanel.OrientationProperty, b);
        }

        private void CreateText()
        {
            if (_panel == null)
                return;

            _panel.Children.Clear();

            var text = this.Text;
            if (string.IsNullOrEmpty(text))
                return;

            UIElement element;
            if (this.Orientation == Orientation.Horizontal)
            {
                element = this.CreateElement(text);
                _panel.Children.Add(element);
            }
            else
            {
                foreach (var c in text)
                {
                    element = this.CreateElement(c.ToString());
                    _panel.Children.Add(element);
                }
            }
        }

        private UIElement CreateElement(string text)
        {
            var panel = this.CreateBackground();
            var textBlock = this.CreateTextBlock(text);
            panel.Children.Add(textBlock);

            return panel;
        }

        private Panel CreateBackground()
        {
            var panel = new Grid()
            {
                Margin = new Thickness(0),
            };
            var b = new Binding("Background") { Source = this };
            panel.SetBinding(Panel.BackgroundProperty, b);

            return panel;
        }

        private TextBlock CreateTextBlock(string text)
        {
            var textBlock = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            var b = new Binding("TextDecorations") { Source = this };
            textBlock.SetBinding(TextBlock.TextDecorationsProperty, b);
            return textBlock;
        }


        #region dp

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(OrientationTextBlock),
            new PropertyMetadata("OrientationTextBlock", OnTextChanged));



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(OrientationTextBlock),
            new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));

        /// <summary>
        /// 下划线。
        /// </summary>
        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(OrientationTextBlock), null);

        #endregion

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((OrientationTextBlock)o).OnTextChanged((string)(e.OldValue), (string)(e.NewValue));
        }

        private static void OnOrientationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((OrientationTextBlock)o).OnOrientationChanged((Orientation)(e.OldValue), (Orientation)(e.NewValue));
        }

        private void OnTextChanged(string oldValue, string newValue)
        {
            this.CreateText();
        }

        private void OnOrientationChanged(Orientation oldValue, Orientation newValue)
        {
            this.CreateText();
        }
    }
}
