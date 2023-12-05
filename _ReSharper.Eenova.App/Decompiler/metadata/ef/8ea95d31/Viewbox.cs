// Type: System.Windows.Controls.Viewbox
// Assembly: System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\System.Windows.dll

using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace System.Windows.Controls
{
  [ContentProperty("Child", true)]
  public sealed class Viewbox : FrameworkElement
  {
    public static readonly DependencyProperty StretchProperty;
    public static readonly DependencyProperty StretchDirectionProperty;
    public Viewbox();
    public UIElement Child { get; set; }
    public Stretch Stretch { get; set; }
    public StretchDirection StretchDirection { get; set; }
  }
}
