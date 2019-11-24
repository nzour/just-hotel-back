using System;

namespace Domain.Exceptions
{
    public class RoomException : Exception
    {
        public RoomException(string message) : base(message)
        {
        }
    }
}