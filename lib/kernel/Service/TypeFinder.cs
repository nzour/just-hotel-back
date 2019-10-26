using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kernel.Service
{
    public class TypeFinder
    {
        public TypeFinder(Assembly applicationScope)
        {
            ApplicationScope = applicationScope;
        }

        public Assembly ApplicationScope { get; }

        public IEnumerable<TypeInfo> FindTypes(Func<TypeInfo, bool> filter)
        {
            return ApplicationScope.DefinedTypes.Where(filter);
        }
    }
}