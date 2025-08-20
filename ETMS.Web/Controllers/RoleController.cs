using ETMS.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController(IRoleService roleService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var roles = await roleService.GetAllRolesAsync();
        return Success(roles);
    }
}
