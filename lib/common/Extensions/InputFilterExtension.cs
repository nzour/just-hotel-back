using System.Linq;
using Common.Util;

namespace Common.Extensions
{
    public static class InputFilterExtension
    {
        public static IQueryable ApplyFilter(this IQueryable query, IInputFilter filter)
        {
            return filter.Process(query);
        }
    }
}