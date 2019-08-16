using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using app.Application.CQS;
using app.Common.Attribute;
using app.Common.Services.Jwt;
using app.DependencyInjection;
using FluentNHibernate.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace app.Aspect.FilterAttribute
{
    public class AuthorizationFilter : AbstractAssemblyAware, IAuthorizationFilter, IGlobalFilter
    {
        public JwtTokenManager TokenManager { get; }
        public IServiceProvider ServiceProvider { get; }

        public AuthorizationFilter(JwtTokenManager tokenManager, IServiceProvider serviceProvider)
        {
            TokenManager = tokenManager;
            ServiceProvider = serviceProvider;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (IsAnonymous(context))
            {
                return;
            }

            var user = TokenManager.GetCurrentUser();
            GetRequiredUserActions().Each(action => action.SetCurrentUser(user));
        }

        private IEnumerable<AbstractUserAware> GetRequiredUserActions()
        {
            return FindTypes(t => !t.IsAbstract && t.IsSubclassOf(typeof(AbstractUserAware)))
                .Select(action => ServiceProvider.GetService(action) as AbstractUserAware);
        }

        private bool IsAnonymous(AuthorizationFilterContext context)
        {
            var regex = new Regex(@"(?<controller>.*)\..*");
            
            // Достаем имя контроллера
            var controller = regex.Match(context.ActionDescriptor.DisplayName)
                .Groups["controller"]
                .Value;

            // Есть ли у контроллера аттрибут Anonymous
            var isControllerAnonymous = GetAssembly().GetType(controller)
                .CustomAttributes.Select(data => data.AttributeType)
                .Contains(typeof(AnonymousAttribute));

            // Есть ли у метода аттрибут Anonymous
            var isMethodAnonymous = context.ActionDescriptor
                .EndpointMetadata
                .Contains(new AnonymousAttribute());
            
            return isControllerAnonymous || isMethodAnonymous;
        }
    }
}