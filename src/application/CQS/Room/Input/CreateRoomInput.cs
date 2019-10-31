using Domain.Room;

#nullable disable

namespace Application.CQS.Room.Input
{
    public class CreateRoomInput
    {
        public RoomType Type { get; set; }
        public int Cost { get; set; }
    }
}