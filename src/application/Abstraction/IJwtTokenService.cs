using Domain.UserEntity;

namespace Application.Abstraction
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}