using PermissionControlHub.Domain.Entities;

namespace PermissionControlHub.Domain.Repositories;

public interface IRoleRepository
{
    Task AddAsync(Role role);
    Task<Role> GetByIdAsync(int roleId);
    Task<Role> GetByIdWithPermissionsAsync(int roleId);
    Task<Permission> GetPermissionByIdAsync(int permissionId);
    Task UpdateAsync(Role role);
}
