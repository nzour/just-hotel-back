using System;
using Domain.Entities;

namespace Application.CQS.Reservation.Query
{
    public class ReservedDateOutput
    {
        public DateTime From { get; }
        public DateTime To { get; set; }

        public ReservedDateOutput(ReservationEntity reservation)
        {
            From = reservation.ReservedFrom;
            To = reservation.ReservedTo;
        }
    }
}
