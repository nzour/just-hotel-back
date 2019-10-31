using System.Linq;
using Application.CQS.Room.Output;
using Common.Extensions;
using Common.Util;
using Domain.Room;

namespace Application.CQS.Room.Query
{
    public class GetRentedRoomsQuery
    {
        private IRoomRepository RoomRepository { get; }

        public GetRentedRoomsQuery(IRoomRepository roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public PaginatedData<RoomShortOutput> Execute(Pagination pagination)
        {
            return RoomRepository
                .FindAll()
                .Where(r => r.IsRented)
                .Select(r => new RoomShortOutput(r))
                .Paginate(pagination);
        }
    }
}