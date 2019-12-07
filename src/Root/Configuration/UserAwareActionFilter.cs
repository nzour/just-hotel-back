using System.Threading.Tasks;
using Application;
using Application.CQS;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class UserAwareActionFilter : IAsyncActionFilter
    {
        private UserExtractor UserExtractor { get; }

        public UserAwareActionFilter(UserExtractor userExtractor)
        {
            UserExtractor = userExtractor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var (_, parameter) in context.ActionArguments)
            {
                if (parameter is AbstractUserAware userAware)
                {
                    userAware.CurrentUser = await UserExtractor.ProvideUserAsync();
                }
            }

            await next();
        }
    }
}
