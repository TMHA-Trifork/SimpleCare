using System.Security.Principal;

using Microsoft.AspNetCore.Authorization;

using SimpleCare.Infrastructure.Interfaces.Authorization;

namespace SimpleCare.Infrastructure.Authorization;

public class SimpleCareAuthorizationHandler : AuthorizationHandler<SimpleCareAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SimpleCareAuthorizationRequirement requirement)
    {
        if (LookupAccess(context.User, context.Resource as SimpleCareResource, requirement))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }

    private static bool LookupAccess(IPrincipal user, SimpleCareResource? resource, SimpleCareAuthorizationRequirement requirement)
    {
        return resource is not null && resource.Id != Guid.Empty;
    }
}
