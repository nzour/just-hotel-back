using System;
using Domain.Room;

namespace Application.CQS.Room.Output
{
    public class RoomOutput
    {
        public Guid Id { get; set; }
        public uint Cost { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsRented { get; set; }

        public RoomOutput(RoomEntity room)
        {
            Id = room.Id;
            Cost = room.Cost;
            RoomType = room.RoomType;
            IsRented = room.IsRented;
        }
    }
}
