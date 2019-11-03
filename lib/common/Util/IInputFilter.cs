using System.Linq;

namespace Common.Util
{
    public interface IInputFilter
    {
        IQueryable Process(IQueryable query);
    }
}