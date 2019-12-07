using System.Threading.Tasks;
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

        public async Task ExecuteAsync(RoomInput input)
        {
            await RoomRepository.SaveAndFlushAsync(new RoomEntity(input.RoomType, input.Cost));
        }
    }
}