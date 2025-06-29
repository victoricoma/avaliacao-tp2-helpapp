using Microsoft.AspNetCore.Authorization;

namespace StockApp.Domain.Entities
{
    public class ClaimsAuthorizationRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public ClaimsAuthorizationRequirement(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
