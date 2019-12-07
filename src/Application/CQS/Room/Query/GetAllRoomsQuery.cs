using System.Linq;
using Application.CQS.Room.Input;
using Application.CQS.Room.Output;
using Common.Extensions;
using Common.Util;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Query
{
    public class GetAllRoomsQuery
    {
        private IEntityRepository<RoomEntity> RoomRepository { get; }

        public GetAllRoomsQuery(IEntityRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public PaginatedData<RoomOutput> Execute(RoomsFilter filter, Pagination pagination)
        {
            return RoomRepository.FindAll()
                .ApplyFilter(filter)
                .Select(room => new RoomOutput(room))
                .Paginate(pagination);
        }
    }
}