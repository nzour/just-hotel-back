using System;
using Application.CQS.Room.Output;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Query
{
    public class GetRoomQuery
    {
        private IEntityRepository<RoomEntity> RoomRepository { get; }

        public GetRoomQuery(IEntityRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public RoomOutput Execute(Guid roomId)
        {
            return new RoomOutput(
                RoomRepository.Get(roomId)
            );
        }
    }
}