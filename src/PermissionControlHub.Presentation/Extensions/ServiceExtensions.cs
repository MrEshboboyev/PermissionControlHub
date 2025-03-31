namespace PermissionControlHub.Presentation.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("RequireManageRoles", policy =>
                policy.RequireClaim("Permission", "ManageRoles"))
            .AddPolicy("RequireReadBooks", policy =>
                policy.RequireClaim("Permission", "ReadBooks"));

        return services;
    }
}
