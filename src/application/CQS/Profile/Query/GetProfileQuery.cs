using Application.CQS.Profile.Output;
using Domain.User;

namespace Application.CQS.Profile.Query
{
    public class GetProfileQuery : IUserAware
    {
        public UserEntity CurrentUser { get; set; }

        public ProfileOutput Execute()
        {
            return new ProfileOutput(CurrentUser);
        }
    }
}
