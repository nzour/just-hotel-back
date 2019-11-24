using Application.CQS.Reservation;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController
    {
        [HttpGet]
        public PaginatedData<ReservationsOutput> GetReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(filter, pagination);
        }
    }
}
