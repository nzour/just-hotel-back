using System.Collections.Generic;
using Application.CQS.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController
    {
        [HttpGet]
        public IEnumerable<ReservationsOutput> GetReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter
        )
        {
            return query.Execute(filter);
        }
    }
}
