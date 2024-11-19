using Lab6API.Data;
using Lab6API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class StaffController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public StaffController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/get-staff-list")]
        public async Task<IActionResult> GetStaffList()
        {
            var staff = await _context.Staffs.ToListAsync();
            return staff.Any() ? Ok(staff) : BadRequest("don`t have this type of data");
        }

        [HttpGet]
        [Route("api/get-staff/{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
            return staff != null ? Ok(staff) : BadRequest("invalid id");
        }
    }
}
