namespace PermissionControlHub.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; } = []; // Roles assigned by admin
    public List<Permission> AdditionalPermissions { get; set; } = []; // Extra permissions
    public List<Permission> RevokedPermissions { get; set; } = []; // Revoked permissions
}
