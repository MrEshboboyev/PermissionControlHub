namespace PermissionControlHub.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties for many-to-many relationships
    public List<Role> Roles { get; set; } = [];             // Roles that have this permission
    public List<User> UsersAdditional { get; set; } = [];  // Users with this as an additional permission
    public List<User> UsersRevoked { get; set; } = [];     // Users with this permission revoked
}