using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kernel.Service
{
    public class TypeFinder
    {
        public Assembly Scope { get; }

        public TypeFinder(Assembly scope)
        {
            Scope = scope;
        }

        public IEnumerable<TypeInfo> FindTypes(Func<TypeInfo, bool> filter)
        {
            return Scope.DefinedTypes.Where(filter);
        }
    }
}