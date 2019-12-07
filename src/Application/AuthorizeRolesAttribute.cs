using Domain.Entities;
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