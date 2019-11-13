using System;
using Domain.Room;

namespace Application.CQS.Room.Output
{
    public class RoomOutput
    {
        public Guid Id { get; set; }
        public string RoomType { get; set; }
        public int Cost { get; set; }
        public bool IsRented { get; set; }

        public RoomOutput(RoomEntity room)
        {
            Id = room.Id;
            RoomType = room.RoomType.ToString();
            Cost = room.Cost;
            IsRented = room.IsRented;
        }
    }
}