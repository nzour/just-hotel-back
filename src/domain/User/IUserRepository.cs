using System.Linq;
using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserRepository : IEntityRepository<UserEntity>
    {
        Task CreateUserAsync(UserEntity user);

        void CreateUser(UserEntity userEntity);

        UserEntity? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);
    }
}