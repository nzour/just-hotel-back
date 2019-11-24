using Domain.Entities;

namespace Application.CQS
{
    public interface IUserAware
    {
        UserEntity? CurrentUser { get; set; }
    }
}