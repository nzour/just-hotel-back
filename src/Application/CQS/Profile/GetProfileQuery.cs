using Domain.Entities;

namespace Application.CQS.Profile
{
    public class GetProfileQuery : IUserAware
    {
        public UserEntity? CurrentUser { get; set; }

        public ProfileOutput Execute()
        {
            return new ProfileOutput(CurrentUser!);
        }
    }
}
