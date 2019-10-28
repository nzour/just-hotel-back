using System.Linq;
using System.Threading.Tasks;

namespace Domain.UserEntity
{
    public interface IUserRepository : IEntityRepository<User.UserEntity>
    {
        Task CreateUserAsync(User.UserEntity userEntity);

        void CreateUser(User.UserEntity userEntity);

        User.UserEntity? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);

        IQueryable<User.UserEntity> FindAllUsers();
    }
}