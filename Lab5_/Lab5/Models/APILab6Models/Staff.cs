namespace Lab5.Models.APILab6Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        public string? Gender { get; set; }
        public string? StaffFirstName { get; set; }
        public string? StaffMiddleName { get; set; }
        public string? StaffLastName { get; set; }
        public DateTimeOffset StaffBirthDate { get; set; }
        public string? StaffDetails { get; set; }
        public string? StaffQualifications { get; set; }
        public Guid StaffCategoryCode { get; set; }
        public Guid RoleCode { get; set; } 
        public Roles? Role { get; set; }
        public StaffCategories? StaffCategory { get; set; } 
    }
}
