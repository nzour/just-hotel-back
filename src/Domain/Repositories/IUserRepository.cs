using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity?> FindUserAsync(string login, string encryptedPassword);

        Task<bool> IsLoginBusyAsync(string login);
    }
}