using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);
    }
}