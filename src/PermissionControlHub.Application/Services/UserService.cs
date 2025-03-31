using PermissionControlHub.Domain.Entities;
using PermissionControlHub.Domain.Repositories;

namespace PermissionControlHub.Application.Services;

public class UserService(
    IUserRepository userRepository, 
    IRoleRepository roleRepository)
{
    public async Task RegisterUserAsync(string username, string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { Username = username, PasswordHash = passwordHash };
        await userRepository.AddAsync(user);
    }

    public async Task AssignRoleToUserAsync(int userId, int roleId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        var role = await roleRepository.GetByIdAsync(roleId);
        if (user != null && role != null)
        {
            user.Roles.Add(role);
            await userRepository.UpdateAsync(user);
        }
    }

    public async Task GrantPermissionToUserAsync(int userId, int permissionId)
    {
        var user = await userRepository.GetByIdWithPermissionsAsync(userId);
        var permission = await userRepository.GetPermissionByIdAsync(permissionId);
        if (user != null && permission != null && !user.AdditionalPermissions.Contains(permission))
        {
            user.AdditionalPermissions.Add(permission);
            await userRepository.UpdateAsync(user);
        }
    }

    public async Task RevokePermissionFromUserAsync(int userId, int permissionId)
    {
        var user = await userRepository.GetByIdWithPermissionsAsync(userId);
        var permission = await userRepository.GetPermissionByIdAsync(permissionId);
        if (user != null && permission != null && !user.RevokedPermissions.Contains(permission))
        {
            user.RevokedPermissions.Add(permission);
            await userRepository.UpdateAsync(user);
        }
    }
}
