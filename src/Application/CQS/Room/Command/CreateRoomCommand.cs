using Application.CQS.Room.Input;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Command
{
    public class CreateRoomCommand
    {
        private IRepository<RoomEntity> RoomRepository { get; }

        public CreateRoomCommand(IRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public void Execute(RoomInput input)
        {
            RoomRepository.SaveAndFlush(new RoomEntity(input.RoomType, input.Cost));
        }
    }
}