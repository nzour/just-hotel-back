using Domain.User;

namespace Application.CQS
{
    public interface IUserAware
    {
        UserEntity? CurrentUser { get; set; }
    }
}