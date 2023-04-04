using BPJ_U2T.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoleByID(int id)
        {
            return Ok(await roleService.GetRoleByID(id));
        }
    }
}
