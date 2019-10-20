using System;

namespace kernel.Attribute
{
    public abstract class AbstractServiceAttribute : System.Attribute
    {
        public Type? Interface { get; }

        protected AbstractServiceAttribute(Type? @interface = null)
        {
            Interface = @interface;
        }
    }
}