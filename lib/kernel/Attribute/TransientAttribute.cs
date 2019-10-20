using System;

namespace Kernel.Attribute
{
    public class TransientAttribute : AbstractServiceAttribute
    {
        public TransientAttribute(Type? @interface = null) : base(@interface)
        {
        }
    }
}