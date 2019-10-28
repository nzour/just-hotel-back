using System;
using Domain.Room;

namespace Application.CQS.Room.Output
{
    public class RoomShortOutput
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }
        public bool IsRented { get; set; }

        public static implicit operator RoomShortOutput(RoomEntity room)
        {
            return new RoomShortOutput
            {
                Id = room.Id,
                RoomType = room.RoomType,
                Cost = room.Cost,
                IsRented = room.IsRented
            };
        }
    }
}