using System;
using System.Threading.Tasks;
using Application.CQS.Room.Output;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Query
{
    public class GetRoomQuery
    {
        private IRepository<RoomEntity> RoomRepository { get; }

        public GetRoomQuery(IRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task<RoomOutput> ExecuteAsync(Guid roomId)
        {
            return new RoomOutput(
                await RoomRepository.GetAsync(roomId)
            );
        }
    }
}