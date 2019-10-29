using Domain.User;

namespace Application.Abstraction
{
    public interface IJwtTokenService
    {
        string CreateToken(UserEntity userEntity);
    }
}