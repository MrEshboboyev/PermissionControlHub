namespace PermissionControlHub.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties for many-to-many relationships
    public List<User> Users { get; set; } = [];          // Users assigned to this role
    public List<Permission> Permissions { get; set; } = []; // Permissions tied to this role
}