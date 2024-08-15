using System.Security.Principal;

namespace Karma.MVC.Helpers.Extensions;

public static class CheckRoleExtension
{
    public static bool IsInAnyRole(this IPrincipal principal, params string[] roles)
    {
        return roles.Any(principal.IsInRole);
    }
}
