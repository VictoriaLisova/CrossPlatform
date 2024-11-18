using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Patients
    {
        [Key]
        public Guid PattientId { get; set; }
        public string? CommanagedYn { get; set; }
        [StringLength(10)]
        public string? NhsNumber { get; set; }
        public string? Gender { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAddress { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string? Details { get; set; }
        public ICollection<StaffPatientAssociations>? Associations { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
