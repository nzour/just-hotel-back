using System;
using Domain.Room;

#nullable disable

namespace Application.CQS.Room.Output
{
    public class RoomOutput
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }
        public bool IsRented { get; set; }

        public RoomOutput(RoomEntity room)
        {
            Id = room.Id;
            RoomType = room.RoomType;
            Cost = room.Cost;
        }
    }
}