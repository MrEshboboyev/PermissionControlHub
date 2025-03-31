using Microsoft.AspNetCore.Authentication.Cookies;
using PermissionControlHub.Domain.Repositories;
using System.Security.Claims;

namespace PermissionControlHub.Infrastructure.Authentication;

public class AuthService(IUserRepository userRepository)
{
    public async Task<ClaimsPrincipal> LoginAsync(string username, string password)
    {
        var user = await userRepository.GetByUsernameWithDetailsAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        var rolePermissions = user.Roles.SelectMany(r => r.Permissions).Distinct();
        var effectivePermissions = rolePermissions.Union(user.AdditionalPermissions)
                                                 .Except(user.RevokedPermissions);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };
        claims.AddRange(effectivePermissions.Select(p => new Claim("Permission", p.Name)));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}