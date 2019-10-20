using App.Domain.UserEntity;

namespace App.Application.Abstraction
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}