using Lab6API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class StaffCategoriesController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public StaffCategoriesController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/staff-categories")]
        public async Task<IActionResult> GetStaffCategories()
        {
            var staff_categories = await _context.StaffCategories.ToListAsync();
            return staff_categories.Any() ? Ok(staff_categories) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/staff-category/{id}")]
        public async Task<IActionResult> GetStaffCategoryById(Guid id)
        {
            var staff_category = await _context.StaffCategories.FirstOrDefaultAsync(s => s.StaffCategoryCode == id);
            return staff_category != null ? Ok(staff_category) : BadRequest("invalid id");
        }
    }
}
