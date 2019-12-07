using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Domain;
using Domain.Entities;
using NHibernate.Linq;

namespace Application.CQS.Reservation.Query
{
    public class GetAllReservationsQuery
    {
        private IRepository<ReservationEntity> ReservationRepository { get; }

        public GetAllReservationsQuery(IRepository<ReservationEntity> reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationsOutput>> ExecuteAsync(ReservationsFilter filter)
        {
            return await ReservationRepository.FindAll()
                .ApplyFilter(filter)
                .Select(r => new ReservationsOutput(r))
                .ToListAsync();
        }
    }
}
