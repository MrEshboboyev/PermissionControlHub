using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionControlHub.Application.Services;

namespace PermissionControlHub.Presentation.Controllers;

[Authorize(Policy = "RequireManageRoles")]
[Route("api/roles")]
[ApiController]
public class RolesController(RoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole(string name)
    {
        await roleService.CreateRoleAsync(name);
        return Ok();
    }
}