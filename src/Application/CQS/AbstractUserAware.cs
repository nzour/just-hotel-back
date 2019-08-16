using _User = app.Domain.Entity.User.User;

namespace app.Application.CQS
{
    public abstract class AbstractUserAware
    {
        protected _User CurrentUser { get; private set; }

        public void SetCurrentUser(_User user)
        {
            CurrentUser = user;
        }
    }
}