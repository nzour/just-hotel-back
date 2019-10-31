using System;

namespace Domain.Room
{
    public class RoomException : Exception
    {
        public RoomException(string message) : base(message)
        {
        }
    }
}