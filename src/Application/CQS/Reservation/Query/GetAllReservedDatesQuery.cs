using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using NHibernate.Linq;

namespace Application.CQS.Reservation.Query
{
    public class GetAllReservedDatesQuery
    {
        private IRepository<ReservationEntity> ReservationRepository { get; }

        public GetAllReservedDatesQuery(IRepository<ReservationEntity> reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservedDateOutput>> ExecuteAsync()
        {
            return await ReservationRepository.FindAll()
                .Where(r => r.ReservedFrom >= DateTime.Now)
                .Select(reservation => new ReservedDateOutput(reservation))
                .ToListAsync();
        }
    }
}
