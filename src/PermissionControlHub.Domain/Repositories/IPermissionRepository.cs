using PermissionControlHub.Domain.Entities;

namespace PermissionControlHub.Domain.Repositories;

public interface IPermissionRepository
{
    Task AddAsync(Permission permission);
    Task<Permission> GetByIdAsync(int permissionId);
    Task UpdateAsync(Permission permission);
}