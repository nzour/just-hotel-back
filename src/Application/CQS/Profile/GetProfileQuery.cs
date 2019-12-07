namespace Application.CQS.Profile
{
    public class GetProfileQuery : AbstractUserAware
    {
        public ProfileOutput Execute()
        {
            return new ProfileOutput(CurrentUser);
        }
    }
}
