using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace kernel.Service
{
    public class TypeFinder
    {
        public Assembly ApplicationScope { get; }

        public TypeFinder(Assembly applicationScope)
        {
            ApplicationScope = applicationScope;
        }

        public IEnumerable<TypeInfo> FindTypes(Func<TypeInfo, bool> filter)
        {
            return ApplicationScope.DefinedTypes.Where(filter);
        }
    }
}