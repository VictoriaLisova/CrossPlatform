using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Staff
    {
        [Key]
        //[Column("staff_id")]
        public Guid StaffId { get; set; }
        //[Column("gender")]
        public string? Gender { get; set; }
        //[Column("staff_first_name")]
        public string? StaffFirstName { get; set; }
        //[Column("staff_middle_name")]
        public string? StaffMiddleName { get; set; }
        //[Column("staff_last_name")]
        public string? StaffLastName { get; set; }
        //[Column("staff_birth_date")]
        public DateTimeOffset StaffBirthDate { get; set; }
        //[Column("other_staff_details")]
        public string? StaffDetails { get; set; }
        //[Column("staff_qualifications")]
        public string? StaffQualifications { get; set; }
        //[Column("staff_category_code")]
        public Guid StaffCategoryCode { get; set; }
        [ForeignKey("StaffCategoryCode")]
        public StaffCategories? StaffCategory { get; set; }
        //[Column("role_code")]
        public Guid RoleCode { get; set; }
        [ForeignKey("RoleCode")]
        public Roles? Role { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
