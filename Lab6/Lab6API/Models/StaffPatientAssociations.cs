using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class StaffPatientAssociations
    {
        [Key]
        //[Column("association_id")]
        public Guid AssociationId { get; set; }
        //[Column("association_start_date")]
        public DateTimeOffset AssociationStartDate { get; set; }
        //[Column("association_end_date")]
        public DateTimeOffset AssociationEndDate { get; set; }
        //[Column("association_details")]
        public string? AssociationDetails { get; set; }
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
