using Application.CQS.Room.Input;
using Domain.Room;

namespace Application.CQS.Room.Command
{
    public class CreateRoomCommand
    {
        private IRoomRepository RoomRepository { get; }

        public CreateRoomCommand(IRoomRepository roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public void Execute(CreateRoomInput input)
        {
            RoomRepository.Save(new RoomEntity(input.Type, input.Cost));
        }
    }
}