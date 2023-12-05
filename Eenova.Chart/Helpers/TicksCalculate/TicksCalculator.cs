using Eenova.Chart.Interfaces;

namespace Eenova.Chart.Helpers
{
    /// <summary>
    /// 根据自动最大值自动最小值等设置计算最大值最小值。
    /// </summary>
    abstract class TicksCalculator
    {
        protected IAxis _axis;

        public double MinValue { get; protected set; }
        public double MaxValue { get; protected set; }
        public double MainUnit { get; protected set; }

        public TicksCalculator(IAxis axis)
        {
            _axis = axis;
        }

        public abstract void Caculate();
    }
}
