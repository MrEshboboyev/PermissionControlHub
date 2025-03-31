using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionControlHub.Application.Services;

namespace PermissionControlHub.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(string username, string password)
    {
        await userService.RegisterUserAsync(username, password);
        return Ok();
    }

    [Authorize(Policy = "RequireManageRoles")]
    [HttpPost("{id}/roles/{roleId}")]
    public async Task<IActionResult> AssignRole(int id, int roleId)
    {
        await userService.AssignRoleToUserAsync(id, roleId);
        return Ok();
    }

    [Authorize(Policy = "RequireManagePermissions")]
    [HttpPost("{id}/permissions/grant/{permissionId}")]
    public async Task<IActionResult> GrantPermission(int id, int permissionId)
    {
        await userService.GrantPermissionToUserAsync(id, permissionId);
        return Ok();
    }
}