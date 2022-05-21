using FirebaseAdmin.Auth;
using JobPlatformUIService.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobPlatformUIService.Authorization
{

    public class ShouldBeAnAdminRequirement : IAuthorizationRequirement
    {
    }
    public class ShouldBeAnAdminRequirementHandler : AuthorizationHandler<ShouldBeAnAdminRequirement>
    {
        private readonly IJWTParser _jwtParser;


        public ShouldBeAnAdminRequirementHandler(IJWTParser jwtParser)
        {
            _jwtParser = jwtParser;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAnAdminRequirement requirement)
        {
            // check if Role claim exists - Else Return
            // (sort of Claim-based requirement)
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
                return Task.CompletedTask;

            // claim exists - retrieve the value
            var claim = context.User
                .Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            var role = claim.Value;

            int i = 0;
            // check if the claim equals to either Admin or Editor
            // if satisfied, set the requirement as success
            //if (role == Roles.Admin || role == Roles.Editor)
            //    context.Succeed(requirement);



            return Task.CompletedTask;
        }
    }
}
