using System;

namespace kernel.Attribute
{
    public class TransientAttribute : AbstractServiceAttribute
    {
        public TransientAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}