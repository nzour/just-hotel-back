using System;

namespace Domain.RoomEntity
{
    public class RoomException : Exception
    {
        public RoomException(string message) : base(message)
        {
        }
    }
}