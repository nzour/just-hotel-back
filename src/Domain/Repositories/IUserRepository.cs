using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IEntityRepository<UserEntity>
    {
        UserEntity? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);
    }
}