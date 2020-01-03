using Application.CQS.User.Input;

namespace Application.CQS.User.Command
{
    public class UpdateAvatarCommand : AbstractUserAware
    {
        public void Execute(UpdateAvatarInput input)
        {
            CurrentUser.Avatar = input.Avatar;
        }
    }
}
