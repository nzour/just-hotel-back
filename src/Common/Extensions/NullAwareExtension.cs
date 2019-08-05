using System;

namespace app.Common.Extensions
{
    public static class NullAwareExtension
    {
        public static bool IsNull(this object @object)
        {
            return null == @object;
        }
        
        public static bool IsNotNull(this object @object)
        {
            return null != @object;
        }

        public static void IfIsNull(this object @object, Action action)
        {
            if (null == @object)
            {
                action.Invoke();   
            }
        }

        public static void IfNotNull(this object @object, Action action)
        {
            if (null != @object)
            {
                action.Invoke();
            }
        }
    }
}