using System;

namespace Kernel.Attribute
{
    public abstract class AbstractServiceAttribute : System.Attribute
    {
        protected AbstractServiceAttribute(Type? @interface = null)
        {
            Interface = @interface;
        }

        public Type? Interface { get; }
    }
}