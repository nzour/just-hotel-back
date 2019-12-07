using System.Linq;

namespace Common.Util
{
    public interface IInputFilter<T>
    {
        IQueryable<T> Process(IQueryable<T> query);
    }
}