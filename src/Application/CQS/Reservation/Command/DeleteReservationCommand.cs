using System;
using System.Threading.Tasks;
using Application.CQS.Reservation.Exception;
using Domain;
using Domain.Entities;

namespace Application.CQS.Reservation.Command
{
    public class DeleteReservationCommand : IUserAware
    {
        public UserEntity? CurrentUser { get; set; }
        private IRepository<ReservationEntity> ReservationRepository { get; }

        public DeleteReservationCommand(IRepository<ReservationEntity> reservationRepository)
        {
            ReservationRepository = reservationRepository;
        }
        
        public async Task ExecuteAsync(Guid reservationId)
        {
            var reservation = await ReservationRepository.GetAsync(reservationId);

            if (reservation.User != CurrentUser)
            {
                throw ReservationException.DoesNotBelongToYou();
            }

            if (DateTime.Now > reservation.ReservedTo)
            {
                throw ReservationException.AlreadyExpired();
            }

            await ReservationRepository.DeleteAndFlushAsync(reservation);
        }
    }
}
