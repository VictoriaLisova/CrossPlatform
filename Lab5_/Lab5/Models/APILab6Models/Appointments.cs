namespace Lab5.Models.APILab6Models
{
    public class Appointments
    {
        public Guid AppointmentId { get; set; }
        public DateTimeOffset AppointmentStartDatetime { get; set; }
        public DateTimeOffset AppointmentEndDatetime { get; set; }
        public string? AppointmentDetails { get; set; }
        public Guid AppointmentStatusCode { get; set; }
        public Guid PatientId { get; set; }
        public Guid StaffId { get; set; }
    }
}
