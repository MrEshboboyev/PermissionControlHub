namespace PermissionControlHub.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    // Navigation properties for many-to-many relationships
    public List<Role> Roles { get; set; } = [];                  // Roles assigned to this user
    public List<Permission> AdditionalPermissions { get; set; } = []; // Extra permissions granted
    public List<Permission> RevokedPermissions { get; set; } = [];    // Permissions revoked
}