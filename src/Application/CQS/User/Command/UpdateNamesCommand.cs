using Application.CQS.User.Input;

namespace Application.CQS.User.Command
{
    public class UpdateNamesCommand : AbstractUserAware
    {
        public void Execute(UpdateNamesInput input)
        {
            CurrentUser.FirstName = input.FirstName;
            CurrentUser.LastName = input.LastName;
        }
    }
}
