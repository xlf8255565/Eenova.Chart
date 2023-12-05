using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Eenova.Chart.Controls
{
    public class DirectionTextBlock : Control
    {
        #region field

        private TextBlock _textBlock;

        #endregion

        public DirectionTextBlock()
        {
            IsTabStop = false;

            var templateXaml =
                @"<ControlTemplate " +
                                   "xmlns='http://schemas.microsoft.com/client/2007' " +
                                   "xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>" +
                    "<Grid Background=\"{TemplateBinding Background}\" Height=\"{TemplateBinding Height}\" Width=\"{TemplateBinding Width}\">" +
                        "<TextBlock x:Name=\"TextBlock\" TextAlignment=\"Center\" TextWrapping=\"Wrap\"/>" +
                    "</Grid>" +
                "</ControlTemplate>";

            Template = (ControlTemplate)XamlReader.Load(templateXaml);

            this.Text = "Vertical";
            this.TextDirection = TextDirection.Vertical;
        }

        #region public method

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBlock = GetTemplateChild("TextBlock") as TextBlock;

            this.CreateText();
            this.SetUnderline();
        }

        #endregion public method

        #region private method

        private void CreateText()
        {
            if (_textBlock == null)
                return;

            _textBlock.Inlines.Clear();

            if (this.Text == null)
                return;

            if (this.TextDirection == TextDirection.Vertical)
            {
                bool first = true;
                foreach (var c in this.Text)
                {
                    if (!first)
                    {
                        _textBlock.Inlines.Add(new LineBreak());
                    }
                    _textBlock.Inlines.Add(new Run { Text = c.ToString() });
                    first = false;
                }
            }
            else
            {
                _textBlock.Inlines.Add(this.Text);
            }
        }

        private void SetUnderline()
        {
            if (_textBlock == null)
                return;

            _textBlock.TextDecorations = this.TextDecorations;
        }

        #endregion private method

        #region dp

        /// <summary>
        /// 文本内容。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DirectionTextBlock),
            new PropertyMetadata(OnTextChanged));


        /// <summary>
        /// 文本方向。
        /// </summary>
        public TextDirection TextDirection
        {
            get { return (TextDirection)GetValue(TextDirectionProperty); }
            set { SetValue(TextDirectionProperty, value); }
        }

        public static readonly DependencyProperty TextDirectionProperty =
            DependencyProperty.Register("TextDirection", typeof(TextDirection), typeof(DirectionTextBlock),
            new PropertyMetadata(OnTextAlignmentChanged));


        /// <summary>
        /// 下划线。
        /// </summary>
        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }

        public static readonly DependencyProperty TextDecorationsProperty =
            DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(DirectionTextBlock),
            new PropertyMetadata(OnTextDecorationsChanged));


        #endregion dp

        #region static callback

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((DirectionTextBlock)o).OnTextChanged((string)(e.NewValue));
        }

        private static void OnTextAlignmentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((DirectionTextBlock)o).OnTextAlignmentChanged((TextDirection)(e.NewValue));
        }

        private static void OnTextDecorationsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((DirectionTextBlock)o).OnTextDecorationsChanged((TextDecorationCollection)(e.NewValue));
        }

        #endregion static callback

        #region callback

        private void OnTextChanged(string newValue)
        {
            this.CreateText();
        }

        private void OnTextAlignmentChanged(TextDirection newValue)
        {
            this.CreateText();
        }

        private void OnTextDecorationsChanged(TextDecorationCollection textDecorationCollection)
        {
            this.SetUnderline();
        }

        #endregion callback
    }
}
