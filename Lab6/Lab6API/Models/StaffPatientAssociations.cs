using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class StaffPatientAssociations
    {
        [Key]
        public Guid AssociationId { get; set; }
        public DateTimeOffset AssociationStartDate { get; set; }
        public DateTimeOffset AssociationEndDate { get; set; }
        public string? AssociationDetails { get; set; }
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patients? Patient { get; set; }
        public Guid StaffId { get; set; }
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }    
    }
}
