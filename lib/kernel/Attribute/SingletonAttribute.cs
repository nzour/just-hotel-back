using System;

namespace Kernel.Attribute
{
    public class SingletonAttribute : AbstractServiceAttribute
    {
        public SingletonAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}