using System;

namespace Common.Extensions
{
    public static class NullAwareExtension
    {
        /// <summary>
        ///     Если object null, то выполнит void-action, и пойдет дальше. В action можно бросать исключения.
        /// </summary>
        public static void IfIsNull(this object @object, Action action)
        {
            if (null == @object) action.Invoke();
        }

        /// <summary>
        ///     Если object НЕ null, то выполнит void-action, и пойдет дальше. В action можно бросать исключения.
        /// </summary>
        public static void IfNotNull(this object @object, Action action)
        {
            if (null != @object) action.Invoke();
        }

        /// <summary>
        ///     Если object null, то выполнится action, который должен бросить исключение. Если action не бросает исключение, то
        ///     вручную бросится NullReferenceException.
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public static TResult AssertNull<TResult>(this TResult @object, Action action)
        {
            if (null == @object)
            {
                action.Invoke();
                throw new NullReferenceException();
            }

            return @object;
        }
    }
}