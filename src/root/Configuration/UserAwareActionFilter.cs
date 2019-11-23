using System;
using System.Linq;
using Application.CQS;
using Common.Extensions;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class UserAwareActionFilter : IActionFilter
    {
        private IHttpContextAccessor Http { get; }
        private IUserRepository UserRepository { get; }

        public UserAwareActionFilter(IHttpContextAccessor http, IUserRepository userRepository)
        {
            Http = http;
            UserRepository = userRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.Foreach(pair =>
            {
                if (pair.Value is IUserAware userAware)
                {
                    userAware.CurrentUser = DetermineCurrentUser();
                }
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // noop
        }

        private UserEntity DetermineCurrentUser()
        {
            string userId = Http.HttpContext.User.Claims.FirstOrDefault(c => "UserId" == c.Type)?.Value
                            ?? throw new Exception("Action requires logged user.");

            return UserRepository.Get(Guid.Parse(userId));
        }
    }
}