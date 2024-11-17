using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Appointments
    {
        [Key]
        //[Column("appointment_id")]
        public Guid AppointmentId { get; set; }
        //[Column("appointment_datatime")]
        public DateTimeOffset AppointmentStartDatetime { get; set; }
        //[Column("appointment_end_datetime")]
        public DateTimeOffset AppointmentEndDatetime { get; set; }
        //[Column("appointment_details")]
        public string? AppointmentDetails { get; set; }
        //[Column("appointment_status_code")]
        public Guid AppointmentStatusCode { get; set; }
        [ForeignKey("AppointmentStatusCode")]
        public AppointmentStatusCodes? AppointmentStatusCodes { get; set; }
        //[Column("patient_id")]
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patients? Patient { get; set; }
        //[Column("staff_id")]
        public Guid StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
    }
}
