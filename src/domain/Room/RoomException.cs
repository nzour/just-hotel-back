using System;

namespace Domain.Room
{
    public class RoomException : Exception
    {
        public RoomException(string message) : base(message)
        {
        }

        public static RoomException InvalidRoomType(string invalidType)
        {
            return new RoomException($"Room type {invalidType} is invalid. Valid types: {string.Join(", ", RoomTypes.ValidTypes)}");
        }
    }
}