using Domain.Entities;

namespace Application.CQS
{
    public abstract class AbstractUserAware
    {
        private UserEntity? _currentUser;

        public UserEntity CurrentUser
        {
            set => _currentUser = value;
            get => _currentUser ?? throw new System.Exception("You are requiring user, which wasn't specified.");
        }
    }
}
