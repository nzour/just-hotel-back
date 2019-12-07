using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQS.Profile;
using Application.CQS.Reservation;
using Application.CQS.User.Command;
using Application.CQS.User.Input;
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

        [HttpGet("reservations")]
        public async Task<IEnumerable<ReservationsOutput>> GetCurrentUserReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter
        )
        {
            var currentUser = await UserExtractor.ProvideUserAsync();
            filter.UserId = currentUser.Id;

            return query.Execute(filter);
        }


        [HttpPut("update-names")]
        public void UpdateUserNames([FromServices] UpdateNamesCommand command, [FromBody] UpdateNamesInput input)
        {
            command.Execute(input);
        }

        [HttpPut("update-password")]
        public void UpdateUserPassword([FromServices] UpdatePasswordCommand command, [FromBody] UpdatePasswordInput input)
        {
            command.Execute(input);
        }
    }
}
