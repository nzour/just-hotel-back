using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Domain.User;
using Microsoft.AspNetCore.Http;

namespace Root.Configuration
{
    public class UserExtractor
    {
        private IHttpContextAccessor ContextAccessor { get; }
        private IUserRepository UserRepository { get; }

        public UserExtractor(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            ContextAccessor = contextAccessor;
            UserRepository = userRepository;
        }

        public async Task<UserEntity> ProvideUser()
        {
            string userId = ContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => "UserId" == c.Type)?.Value
                            ?? throw new AuthenticationException("Action requires logged user.");

            return await UserRepository.GetAsync(Guid.Parse(userId));
        }
    }
}
