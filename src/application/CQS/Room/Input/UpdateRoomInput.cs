using Domain.Room;

namespace Application.CQS.Room.Input
{
    public class UpdateRoomInput
    {
        public int Cost { get; }
        public RoomType RoomType { get; }

        public UpdateRoomInput(int cost, RoomType roomType)
        {
            RoomType = roomType;
            Cost = cost;
        }
    }
}