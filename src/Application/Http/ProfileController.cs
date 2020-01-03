using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQS.Reservation;
using Application.CQS.Reservation.Query;
using Application.CQS.User.Command;
using Application.CQS.User.Input;
using Application.CQS.User.Output;
using Infrastructure.NHibernate;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController
    {
        private UserExtractor UserExtractor { get; }
        public TransactionFacade Transaction { get; }

        public ProfileController(UserExtractor userExtractor, TransactionFacade transaction)
        {
            UserExtractor = userExtractor;
            Transaction = transaction;
        }

        [HttpGet]
        public async Task<ProfileOutput> GetProfile()
        {
            return new ProfileOutput(await UserExtractor.ProvideUserAsync());
        }

        [HttpGet("reservations")]
        public async Task<IEnumerable<ReservationsOutput>> GetCurrentUserReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter
        )
        {
            var currentUser = await UserExtractor.ProvideUserAsync();
            filter.UserId = currentUser.Id;

            return await query.ExecuteAsync(filter);
        }


        [HttpPatch("update-names")]
        public async Task UpdateUserNames([FromBody] UpdateNamesInput input)
        {
            var user = await UserExtractor.ProvideUserAsync();

            await Transaction.ActionAsync(() =>
            {
                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
            });
        }

        [HttpPatch("update-avatar")]
        public async Task UpdateUserAvatar([FromBody] UpdateAvatarInput input)
        {
            var user = await UserExtractor.ProvideUserAsync();

            await Transaction.ActionAsync(() => user.Avatar = input.Avatar);
        }

        [HttpPatch("update-password")]
        public void UpdateUserPassword([FromServices] UpdatePasswordCommand command, [FromBody] UpdatePasswordInput input)
        {
            command.Execute(input);
        }
    }
}
