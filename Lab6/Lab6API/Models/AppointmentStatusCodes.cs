using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class AppointmentStatusCodes
    {
        [Key]
        //[Column("appointment_status_code")]
        public Guid AppointmentStatusCode { get; set; }
        //[Column("appointment_status_description")]
        public string? AppointmentStatusDescription { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
