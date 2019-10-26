using Domain.UserEntity;

namespace Application.CQS
{
    public abstract class AbstractUserAware
    {
        protected User? CurrentUser { get; private set; }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
    }
}