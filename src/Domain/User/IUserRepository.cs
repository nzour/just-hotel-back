using System;
using System.Threading.Tasks;

namespace app.Domain.User
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        
        User GetUser(Guid id);

        User? FindUserWithLoginAndPassword(string login, string encryptedPassword);
    }
}