using System.Threading.Tasks;
using Application.CQS.Profile;
using Application.CQS.Reservation;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController
    {
        private UserExtractor UserExtractor { get; }

        public ProfileController(UserExtractor userExtractor)
        {
            UserExtractor = userExtractor;
        }
        
        [HttpGet]
        public ProfileOutput GetProfile([FromServices] GetProfileQuery query)
        {
            return query.Execute();
        }
        
        [HttpGet("")]
        public async Task<PaginatedData<ReservationsOutput>> GetCurrentUserReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            var currentUser = await UserExtractor.ProvideUser();
            filter.UserId = currentUser.Id;

            return query.Execute(filter, pagination);
        }
    }
}
