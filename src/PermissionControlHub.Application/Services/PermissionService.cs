using PermissionControlHub.Domain.Entities;
using PermissionControlHub.Domain.Repositories;

namespace PermissionControlHub.Application.Services;

public class PermissionService(IPermissionRepository permissionRepository)
{
    public async Task CreatePermissionAsync(string name)
    {
        var permission = new Permission { Name = name };
        await permissionRepository.AddAsync(permission);
    }
}