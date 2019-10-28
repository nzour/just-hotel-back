using Domain.User;

namespace Application.CQS
{
    public abstract class AbstractUserAware
    {
        protected UserEntity? CurrentUser { get; private set; }

        public void SetCurrentUser(UserEntity userEntity)
        {
            CurrentUser = userEntity;
        }
    }
}