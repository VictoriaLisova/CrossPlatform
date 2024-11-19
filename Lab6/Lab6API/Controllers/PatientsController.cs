using Lab6API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class PatientsController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public PatientsController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/get-patients")]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients.Any() ? Ok(patients) : BadRequest("don`t have any patients");
        }

        [HttpGet]
        [Route("api/get-patient/{id}")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p=>p.PattientId == id);
            return patient != null ? Ok(patient) : BadRequest("invalid id");
        }
    }
}
