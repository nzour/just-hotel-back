using System.Linq;
using Common.Extensions;
using Common.Util;
using Domain;
using Domain.Reservation;

namespace Application.CQS.Reservation
{
    public class GetAllReservationsQuery
    {
        private IEntityRepository<ReservationEntity> ReservationRepository { get; }

        public GetAllReservationsQuery(IEntityRepository<ReservationEntity> reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }

        public PaginatedData<ReservationsOutput> Execute(ReservationsFilter filter, Pagination pagination)
        {
            return ReservationRepository.FindAll()
                .ApplyFilter(filter)
                .Select(r => new ReservationsOutput(r))
                .Paginate(pagination);
        }
    }
}
