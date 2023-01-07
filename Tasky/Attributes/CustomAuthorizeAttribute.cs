using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Tasky.Exceptions;

namespace Tasky.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _roles;
    public CustomAuthorizeAttribute(string roles)
    {
        _roles = roles;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //TODO: Improve this authorization part
        
        if(_roles == "admin")
        {
            var claim = new Claim(ClaimTypes.Role, _roles);
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value);
            if (!hasClaim)
            {
                throw new UnauthorizedException("You're not allowed to use this feature.");
            }
        }

    }
}