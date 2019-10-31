using System;

namespace Common.Util.Caster
{
    public class CasterException : Exception
    {
        public CasterException(string message): base(message)
        {
        }

        public static CasterException ConstructorWasNotFound<T>(Type toCast)
        {
            return new CasterException($"Type {toCast.Name} must have constructor with one argument type of {typeof(T).Name}.");
        }
    }
}