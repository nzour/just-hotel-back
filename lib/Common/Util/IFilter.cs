using System.Linq;

namespace Common.Util
{
    public interface IFilter<T>
    {
        IQueryable<T> Process(IQueryable<T> query);
    }
}