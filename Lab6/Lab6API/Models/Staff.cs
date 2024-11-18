using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Staff
    {
        [Key]
        public Guid StaffId { get; set; }
        public string? Gender { get; set; }
        public string? StaffFirstName { get; set; }
        public string? StaffMiddleName { get; set; }
        public string? StaffLastName { get; set; }
        public DateTimeOffset StaffBirthDate { get; set; }
        public string? StaffDetails { get; set; }
        public string? StaffQualifications { get; set; }
        public Guid StaffCategoryCode { get; set; }
        [ForeignKey("StaffCategoryCode")]
        public StaffCategories? StaffCategory { get; set; }
        public Guid RoleCode { get; set; }
        [ForeignKey("RoleCode")]
        public Roles? Role { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
