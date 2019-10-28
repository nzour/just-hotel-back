using System.Collections.Generic;
using System.Linq;

namespace Common.Util
{
    public class PaginatedData<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedData()
        {
            Total = 0;
            Data = new List<T>();
        }

        public PaginatedData(IQueryable<T> query)
        {
            Total = query.Count();
            Data = query.ToArray();
        }

        public PaginatedData(int total, IEnumerable<T>? data = null)
        {
            Total = total;
            Data = data ?? new List<T>();
        }
    }
}