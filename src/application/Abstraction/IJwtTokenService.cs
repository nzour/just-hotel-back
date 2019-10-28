using Domain.User;
using Domain.UserEntity;

namespace Application.Abstraction
{
    public interface IJwtTokenService
    {
        string CreateToken(UserEntity userEntity);
    }
}