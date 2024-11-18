using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class AppointmentStatusCodes
    {
        [Key]
        public Guid AppointmentStatusCode { get; set; }
        public string? AppointmentStatusDescription { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
