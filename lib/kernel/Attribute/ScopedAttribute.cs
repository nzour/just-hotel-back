using System;

namespace Kernel.Attribute
{
    public class ScopedAttribute : AbstractServiceAttribute
    {
        public ScopedAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}