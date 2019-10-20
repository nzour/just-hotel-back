using System;

namespace kernel.Attribute
{
    public class SingletonAttribute : AbstractServiceAttribute
    {
        public SingletonAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}