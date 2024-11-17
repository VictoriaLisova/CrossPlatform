using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class PatientRecords
    {
        [Key]
        //[Column("patient_record_id")]
        public Guid PatientRecordId { get; set; }
        //[Column("updated_date")]
        public DateTimeOffset UpdatedDate { get; set; }
        //[Column("patient_id")]
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patients? Patient { get; set; }
        //[Column("component_code")]
        public Guid ComponentCode { get; set; }
        [ForeignKey("ComponentCode")]
        public RecordComponents? RecordComponent { get; set; }
        //[Column("patient_record_component_details")]
        public string? PatientRecordComponentDetails { get; set; }
        //[Column("updated_by_staff_id")]
        public Guid UpdatedByStaffId { get; set; }
        [ForeignKey("UpdatedByStaffId")]
        public Staff? Staff { get; set; }
    }
}
