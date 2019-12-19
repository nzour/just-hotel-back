using System;
using System.Linq;
using System.Threading.Tasks;
using Application.CQS.Reservation.Exception;
using Domain;
using Domain.Entities;
using JetBrains.Annotations;
using NHibernate.Linq;

namespace Application.CQS.Reservation.Command
{
    public class CreateReservationCommand : AbstractUserAware
    {
        private IRepository<ReservationEntity> ReservationRepository { get; }
        private IRepository<RoomEntity> RoomRepository { get; }
        private IRepository<ServiceEntity> ServiceRepository { get; }

        public CreateReservationCommand(
            IRepository<ReservationEntity> reservationRepository,
            IRepository<RoomEntity> roomRepository,
            IRepository<ServiceEntity> serviceRepository
        )
        {
            ReservationRepository = reservationRepository;
            RoomRepository = roomRepository;
            ServiceRepository = serviceRepository;
        }

        public async Task ExecuteAsync(ReservationInput input)
        {
            AssertRentDatesValid(input);
            await AssertRentDatesNotBusyAsync(input);

            var services = await ServiceRepository.FindAll()
                .Where(service => input.ServiceIds.Contains(service.Id))
                .ToListAsync();

            var room = await RoomRepository.GetAsync(input.RoomId);

            var reservation = new ReservationEntity(CurrentUser!, room, input.ReservedFrom, input.ReservedTo, services);

            await ReservationRepository.SaveAndFlushAsync(reservation);
        }

        [AssertionMethod]
        private void AssertRentDatesValid(ReservationInput input)
        {
            if (DateTime.Now > input.ReservedTo)
            {
                throw CreateReservationException.DateToCantBeInPast();
            }

            if (input.ReservedFrom >= input.ReservedTo)
            {
                throw CreateReservationException.InvalidDateValues();
            }
        }

        [AssertionMethod]
        private async Task AssertRentDatesNotBusyAsync(ReservationInput input)
        {
            var reservations = ReservationRepository
                .FindAll()
                .Where(r => input.RoomId == r.Room.Id
                            && (r.ReservedFrom <= input.ReservedFrom && input.ReservedFrom <= r.ReservedTo
                                || r.ReservedFrom <= input.ReservedTo && input.ReservedTo <= r.ReservedTo));

            if (0 != await reservations.CountAsync())
            {
                throw CreateReservationException.DatesAreBusy();
            }
        }
    }
}
