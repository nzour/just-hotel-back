using System.Collections.Generic;
using System.Linq;
using Common.Util;

namespace Common.Extensions
{
    public static class PaginationExtension
    {
        public static PaginatedData<T> Paginate<T>(
            this IEnumerable<T> query,
            Pagination pagination
        )
        {
            var result = new PaginatedData<T>(query.Count());

            if (null != pagination.Offset)
            {
                query = query.Skip((int) pagination.Offset);
            }

            if (null != pagination.Limit)
            {
                query = query.Take((int) pagination.Limit);
            }

            result.Data = query.ToArray();

            return result;
        }
    }
}
