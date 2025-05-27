using Microsoft.AspNetCore.Authorization;

namespace SimpleCare.Infrastructure.Interfaces.Authorization;

public enum AccessLevel
{
    Read = 1,
    Write = 2
}
public record SimpleCareAuthorizationRequirement(AccessLevel AccessLevel) : IAuthorizationRequirement;
