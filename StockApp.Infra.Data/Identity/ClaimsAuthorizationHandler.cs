using Microsoft.AspNetCore.Authorization;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.Identity
{
    public class ClaimsAuthorizationHandler : AuthorizationHandler<ClaimsAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(c => c.Type == 
            requirement.ClaimType && c.Value == requirement.ClaimValue))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
