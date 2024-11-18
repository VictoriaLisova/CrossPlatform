using Lab6API.Data;
using Lab6API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public SearchController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/get-by-datetime")]
        public async Task<IActionResult> SearchByDateTime(DateTimeOffset start, DateTimeOffset end)
        {
            // get appointments where start time is between two measures
            var appointments = await _context.Appointments.Where(a=>a.AppointmentStartDatetime >= start 
                                                            && a.AppointmentEndDatetime <= end).ToListAsync();
            return appointments.Any() ? Ok(appointments) : BadRequest("don`t have any appointments");
        }

        [HttpGet]
        [Route("api/get-by-list-elements")]
        public async Task<IActionResult> SearchByListElements(string stuffIdList)
        {
            var split = new char[] { ' ', ',', '.', ';', ':' };
            var idList = stuffIdList.Split(split)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => Guid.Parse(l))
                .ToList();

            // get all staff by id and then join by role and get role description,
            // then join by category and get category description

            var staff = await _context.Staffs.Where(s=>idList.Contains(s.StaffId))
                .Include(s => s.Role)
                .Include(s => s.StaffCategory)
                .Include(s => s.PatientRecords)
                    .ThenInclude(p => p.Patient)
                .Select(s => new  // to avoid circle dependency
                {
                    s.StaffId,
                    s.Gender,
                    s.StaffFirstName,
                    s.StaffMiddleName,
                    s.StaffLastName,
                    s.StaffBirthDate,
                    s.StaffDetails,
                    s.StaffQualifications,
                    s.Role,
                    s.StaffCategory,
                    PatientRecords = s.PatientRecords.Select(p => p.Patient.PatientName).ToList()
                })
                .ToListAsync();

            return staff.Any() ? Ok(staff) : BadRequest("don`t have any staff");
        }

        [HttpGet]
        [Route("api/get-by-substring")]
        public async Task<IActionResult> SearchBySubString(string substring)
        {
            // get patients where nhs number contains some substring
            var patients = await _context.Patients.Where(p => p.NhsNumber.Contains(substring)).ToListAsync();
            return patients.Any() ? Ok(patients) : BadRequest("don`t have any patients");
        }
    }
}
