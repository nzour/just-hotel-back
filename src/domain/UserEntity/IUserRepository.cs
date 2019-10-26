using System.Threading.Tasks;

namespace Domain.UserEntity
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task CreateUserAsync(User user);

        void CreateUser(User user);

        User? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);
    }
}