using Application;
using Application.CQS;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class UserAwareActionFilter : IActionFilter
    {
        private UserExtractor UserExtractor { get; }

        public UserAwareActionFilter(UserExtractor userExtractor)
        {
            UserExtractor = userExtractor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.Foreach(pair =>
            {
                if (pair.Value is AbstractUserAware userAware)
                {
                    userAware.CurrentUser = UserExtractor.ProvideUser();
                }
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // noop
        }
    }
}
