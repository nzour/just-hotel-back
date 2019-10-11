using app.Domain.User;

namespace app.Application.Abstraction
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}