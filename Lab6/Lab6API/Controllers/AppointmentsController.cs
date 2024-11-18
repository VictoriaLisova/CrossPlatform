using Lab6API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public AppointmentsController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/appointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return appointments.Any() ? Ok(appointments) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/appointment/{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            return appointment != null ? Ok(appointment) : BadRequest("invalid id");
        }
    }
}
