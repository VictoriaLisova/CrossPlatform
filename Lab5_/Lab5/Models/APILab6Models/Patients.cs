namespace Lab5.Models.APILab6Models
{
    public class Patients
    {
        public Guid PattientId { get; set; }
        public string? CommanagedYn { get; set; }
        public string? NhsNumber { get; set; }
        public string? Gender { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAddress { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string? Details { get; set; }
    }
}
