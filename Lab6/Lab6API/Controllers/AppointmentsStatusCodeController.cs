using Lab6API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class AppointmentsStatusCodeController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public AppointmentsStatusCodeController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/appointment-status-codes")]
        public async Task<IActionResult> GetAppointmentStatusCodes()
        {
            var appointments_status_codes = await _context.AppointmentStatusCodes.ToListAsync();
            return appointments_status_codes.Any() ? Ok(appointments_status_codes) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/appointment-status-code/{id}")]
        public async Task<IActionResult> GetAppointmentStatusCodeById(Guid id)
        {
            var appointment_status_code = await _context.AppointmentStatusCodes.FirstOrDefaultAsync(a => a.AppointmentStatusCode == id);
            return appointment_status_code != null ? Ok(appointment_status_code) : BadRequest("invalid id");
        }
    }
}
