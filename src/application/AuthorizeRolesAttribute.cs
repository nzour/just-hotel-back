using Domain.User;
using Microsoft.AspNetCore.Authorization;

namespace Application
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}