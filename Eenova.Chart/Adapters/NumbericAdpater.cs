using System.Windows;
using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Adapters
{
    /// <summary>
    /// 把任何数值型转化为INumberic接口的适配器。
    /// </summary>
    public class NumbericAdpater : INumberic
    {
        UIElement _element;
        DependencyProperty _dp;

        public NumbericAdpater(UIElement element, DependencyProperty dp)
        {
            _element = element;
            _dp = dp;
        }

        public double Value
        {
            get { return (double)_element.GetValue(_dp); }
            set { _element.SetValue(_dp, value); }
        }
    }
}
