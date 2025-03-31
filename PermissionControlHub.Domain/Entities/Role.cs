namespace PermissionControlHub.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } // e.g., "Admin", "User1"
    public List<Permission> Permissions { get; set; } = [];
}
