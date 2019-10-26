using System;

namespace Domain.DebtEntity
{
    public class DebtException : Exception
    {
        public DebtException(string message) : base(message)
        {
        }
    }
}