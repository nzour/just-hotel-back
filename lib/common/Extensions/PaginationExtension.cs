using System.Linq;
using Common.Util;

namespace Common.Extensions
{
    public static class PaginationExtension
    {
        public static PaginatedData<TOutput> Paginate<TOutput>(this IQueryable<object> query, Pagination pagination)
        {
            var result = new PaginatedData<TOutput>(query.Count());
            
            if (null != pagination.Offset)
            {
                query = query.Skip((int) pagination.Offset);
            }

            if (null != pagination.Limit)
            {
                query = query.Take((int) pagination.Limit);
            }

            result.Data = query.ToArray() as TOutput[];

            return result;
        }
    }
}