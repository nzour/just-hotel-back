using Domain.Room;

namespace Application.CQS.Room.Input
{
    public class CreateRoomInput
    {
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }

        public CreateRoomInput(RoomType roomType, int cost)
        {
            RoomType = roomType;
            Cost = cost;
        }
    }
}