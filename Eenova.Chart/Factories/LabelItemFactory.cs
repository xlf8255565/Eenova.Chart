using Eenova.Chart.Elements;

namespace Eenova.Chart.Factories
{
    class LabelItemFactory
    {
        public static LabelItem Create(double interval, object label)
        {
            return new LabelItem()
            {
                Interval = interval,
                Label = label
            };
        }
    }
}
