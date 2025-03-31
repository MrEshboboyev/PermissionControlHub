using Microsoft.EntityFrameworkCore;
using PermissionControlHub.Domain.Entities;

namespace PermissionControlHub.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User-Role many-to-many
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "RoleId"); // Composite primary key
                    j.ToTable("UserRoles");
                });

        // Role-Permission many-to-many
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolePermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j =>
                {
                    j.HasKey("RoleId", "PermissionId"); // Composite primary key
                    j.ToTable("RolePermissions");
                });

        // User-AdditionalPermissions many-to-many
        modelBuilder.Entity<User>()
            .HasMany(u => u.AdditionalPermissions)
            .WithMany(p => p.UsersAdditional)
            .UsingEntity<Dictionary<string, object>>(
                "UserAdditionalPermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "PermissionId"); // Composite primary key
                    j.ToTable("UserAdditionalPermissions");
                });

        // User-RevokedPermissions many-to-many
        modelBuilder.Entity<User>()
            .HasMany(u => u.RevokedPermissions)
            .WithMany(p => p.UsersRevoked)
            .UsingEntity<Dictionary<string, object>>(
                "UserRevokedPermissions",
                j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "PermissionId"); // Composite primary key
                    j.ToTable("UserRevokedPermissions");
                });

        // Seed Admin user and related data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Permissions
        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = 1, Name = "ManageRoles" },
            new Permission { Id = 2, Name = "ManagePermissions" },
            new Permission { Id = 3, Name = "ReadBooks" }
        );

        // Seed Admin Role
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" }
        );

        // Seed RolePermissions for Admin
        modelBuilder.Entity("RolePermissions").HasData(
            new { RoleId = 1, PermissionId = 1 }, // ManageRoles
            new { RoleId = 1, PermissionId = 2 }  // ManagePermissions
        );

        // Seed Admin User
        var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = adminPasswordHash }
        );

        // Assign Admin Role to Admin User
        modelBuilder.Entity("UserRoles").HasData(
            new { UserId = 1, RoleId = 1 }
        );
    }
}