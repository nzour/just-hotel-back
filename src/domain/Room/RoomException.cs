using System;
using Domain.User;
using Domain.UserEntity;

namespace Domain.RoomEntity
{
    public class RoomException : Exception
    {
        public RoomException(string message) : base(message)
        {
        }
        
        public static void AssertCanRentRoom(Room.RoomEntity roomEntity, User.UserEntity rentedBy)
        {
            if (roomEntity.IsRented)
            {
                throw new RoomException("Room is already rented.");
            }

            if (UserRole.Client != rentedBy.Role)
            {
                throw new RoomException($"User with role {rentedBy.Role} can't rent the room.");
            }
        }
        
        public static void AssertRentalDatesValid(DateTime? from, DateTime? to)
        {
            if (null == from || null == to)
            {
                throw new RoomException("Both of rental dates must be provided.");
            }

            if (from < DateTime.Now)
            {
                throw new RoomException("Rental date 'from' must be greater that current date.");
            }

            if (from >= to)
            {
                throw new RoomException("Rental date 'to' can't be less or equals than date 'from'.");
            }
        }
    }
}