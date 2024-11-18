using Lab6API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public RolesController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/roles-list")]
        public async Task<IActionResult> GetRolesList()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.Any() ? Ok(roles) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/get-role/{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == id);
            return role != null ? Ok(role) : BadRequest("invalid id");
        }
    }
}
