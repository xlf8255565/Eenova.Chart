using Eenova.Chart.Elements;

namespace Eenova.Chart.Interfaces
{
    public interface IDataLink : IShadowStroke, IMark, IText, ILink
    {
        DataPointCollection DataPoints { get; }
    }
}
