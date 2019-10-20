using System;
using System.Threading.Tasks;

namespace app.Domain.User
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task CreateUserAsync(User user);
        
        User? FindUserWithLoginAndPassword(string login, string encryptedPassword);

        bool IsLoginBusy(string login);
    }
}