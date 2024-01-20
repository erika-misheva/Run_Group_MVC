using System.Security.Claims;

namespace RunGroupAplication;

public static class ClaimsPrincipalExtansions
{
    public static string? GetUserId (this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
