using System;

namespace Common.Extensions
{
    public static class ExpressionExtension
    {
        public static bool Between(this DateTime @this, DateTime left, DateTime right)
        {
            return left <= @this && @this <= right;
        }
    }
}
