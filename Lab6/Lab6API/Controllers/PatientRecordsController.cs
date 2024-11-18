using Lab6API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class PatientRecordsController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public PatientRecordsController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/patient-records")]
        public async Task<IActionResult> GetPatientRecords()
        {
            var patient_records = await _context.PatientRecords.ToListAsync();
            return patient_records.Any() ? Ok(patient_records) : BadRequest("don`t have any data");
        }

        [HttpGet]
        [Route("api/patient-record/{id}")]
        public async Task<IActionResult> GetPatientRecordById(Guid id)
        {
            var record = await _context.PatientRecords.FirstOrDefaultAsync(p => p.PatientRecordId == id);
            return record != null ? Ok(record) : BadRequest("invalid id");
        }
    }
}
