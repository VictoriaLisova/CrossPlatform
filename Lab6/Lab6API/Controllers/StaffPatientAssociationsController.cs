using Lab6API.Data;
using Lab6API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Controllers
{
    [ApiController]
    public class StaffPatientAssociationsController : ControllerBase
    {
        private readonly IAPIDbContext _context;
        public StaffPatientAssociationsController(IAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/get-staff-patient-associations")]
        public async Task<IActionResult> GetStaffPatientAssociation()
        {
            var staffPatientAssociations = await _context.StaffPatientAssociations.ToListAsync();
            return staffPatientAssociations.Any() ? Ok(staffPatientAssociations) : BadRequest("don`t have any data");
        }
        [HttpGet]
        [Route("api/get-association/{id}")]
        public async Task<IActionResult> GetStaffPatientAssociationById(Guid id)
        {
            var element = await _context.StaffPatientAssociations.FirstOrDefaultAsync(p=>p.AssociationId == id);
            return element != null ? Ok(element) : BadRequest("invalid id");
        }
    }
}
