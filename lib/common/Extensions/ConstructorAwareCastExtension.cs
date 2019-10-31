using System.Collections.Generic;
using Common.Util.Caster;

namespace Common.Extensions
{
    public static class ConstructorAwareCastExtension
    {
        public static TCasted ConstructorCast<TCasted>(this object @object)
        {
            return ConstructorAwareCaster<TCasted>.Cast(@object);
        }

        public static IEnumerable<TCasted> ConstructorCast<TSource, TCasted>(this IEnumerable<TSource> collection) where TSource : class
        {
            return collection.Map(ConstructorCast<TCasted>);
        }
    }
}