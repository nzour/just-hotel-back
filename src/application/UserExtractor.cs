using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application
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

        public UserEntity ProvideUser()
        {
            return UserRepository.Get(Guid.Parse(ExtractUserId()));
        }

        public async Task<UserEntity> ProvideUserAsync()
        {
            return await UserRepository.GetAsync(Guid.Parse(ExtractUserId()));
        }

        private string ExtractUserId()
        {
            return ContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => "UserId" == c.Type)?.Value
                   ?? throw new AuthenticationException("Action requires logged user.");
        }
    }
}
