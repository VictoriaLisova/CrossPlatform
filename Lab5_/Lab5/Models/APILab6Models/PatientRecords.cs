namespace Lab5.Models.APILab6Models
{
    public class PatientRecords
    {
        public Guid PatientRecordId { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid ComponentCode { get; set; }
        public string? PatientRecordComponentDetails { get; set; }
        public Guid UpdatedByStaffId { get; set; }
    }
}
