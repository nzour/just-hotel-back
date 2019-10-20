using System;

namespace kernel.Attribute
{
    public class ScopedAttribute : AbstractServiceAttribute
    {
        public ScopedAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}