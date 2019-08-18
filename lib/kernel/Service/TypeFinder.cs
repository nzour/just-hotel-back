using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace kernel.Service
{
    public class TypeFinder
    {
        public Assembly Assembly { get; }

        public TypeFinder(Assembly assembly)
        {
            Assembly = assembly;
        }

        public IEnumerable<TypeInfo> FindTypes(Func<TypeInfo, bool> filter)
        {
            return Assembly.DefinedTypes.Where(filter);
        }
    }
}