using Lab6API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class RecordComponentsController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public RecordComponentsController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/record-components")]
        public async Task<IActionResult> GetRecordComponents()
        {
            var components = await _context.RecordComponents.ToListAsync();
            return components.Any() ? Ok(components) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/get-record-component/{id}")]
        public async Task<IActionResult> GetRecordComponentById(Guid id)
        {
            var record_component = await _context.RecordComponents.FirstOrDefaultAsync(r => r.ComponentCode == id);
            return record_component != null ? Ok(record_component) : BadRequest("invalif id");
        }
    }
}
