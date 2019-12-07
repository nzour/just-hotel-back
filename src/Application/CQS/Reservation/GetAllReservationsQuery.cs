using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Domain;
using Domain.Entities;

namespace Application.CQS.Reservation
{
    public class GetAllReservationsQuery
    {
        private IEntityRepository<ReservationEntity> ReservationRepository { get; }

        public GetAllReservationsQuery(IEntityRepository<ReservationEntity> reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }

        public IEnumerable<ReservationsOutput> Execute(ReservationsFilter filter)
        {
            return ReservationRepository.FindAll()
                .ApplyFilter(filter)
                .Select(r => new ReservationsOutput(r));
        }
    }
}
