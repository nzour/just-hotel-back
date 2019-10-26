using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> mapper)
        {
            return collection
                .Select(mapper)
                .Where(el => el != null);
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection) action.Invoke(item);

            return collection;
        }

        /// <summary>
        ///     Поиск дубликато в списке
        /// </summary>
        public static IEnumerable<T> FindDuplicates<T>(this IEnumerable<T> collection)
        {
            var count = collection.Count();
            var array = collection.ToArray();

            var result = new List<T>();

            for (var i = 0; i < count; i++)
                if (Equals(array[i], array[i + 1]))
                    result.Add(array[i]);

            return result;
        }

        /// <summary>
        ///     Поиск дубликатов в списке, предварительно смаппив его
        /// </summary>
        public static IEnumerable<TResult> FindDuplicates<TSource, TResult>(
            this IEnumerable<TSource> collection,
            Func<TSource, TResult> mapper
        )
        {
            var count = collection.Count();
            var array = collection.Select(mapper).ToArray();

            var result = new List<TResult>();

            for (var i = 0; i < count - 1; i++)
                if (Equals(array[i], array[i + 1]))
                    result.Add(array[i]);

            return result;
        }
    }
}