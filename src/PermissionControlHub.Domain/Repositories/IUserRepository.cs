using PermissionControlHub.Domain.Entities;

namespace PermissionControlHub.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(int userId);
    Task<User> GetByIdWithPermissionsAsync(int userId);
    Task<User> GetByUsernameWithDetailsAsync(string username);
    Task<Permission> GetPermissionByIdAsync(int permissionId);
    Task UpdateAsync(User user);
}
