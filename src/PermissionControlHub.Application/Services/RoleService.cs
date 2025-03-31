using PermissionControlHub.Domain.Entities;
using PermissionControlHub.Domain.Repositories;

namespace PermissionControlHub.Application.Services;

public class RoleService(IRoleRepository roleRepository)
{
    public async Task CreateRoleAsync(string name)
    {
        var role = new Role { Name = name };
        await roleRepository.AddAsync(role);
    }

    public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
    {
        var role = await roleRepository.GetByIdWithPermissionsAsync(roleId);
        var permission = await roleRepository.GetPermissionByIdAsync(permissionId);
        if (role != null && permission != null && !role.Permissions.Contains(permission))
        {
            role.Permissions.Add(permission);
            await roleRepository.UpdateAsync(role);
        }
    }
}
