using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Appointments
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public DateTimeOffset AppointmentStartDatetime { get; set; }
        public DateTimeOffset AppointmentEndDatetime { get; set; }
        public string? AppointmentDetails { get; set; }
        public Guid AppointmentStatusCode { get; set; }
        [ForeignKey("AppointmentStatusCode")]
        public AppointmentStatusCodes? AppointmentStatusCodes { get; set; }
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patients? Patient { get; set; }
        public Guid StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}
