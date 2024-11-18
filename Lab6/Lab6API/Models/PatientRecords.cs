using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class PatientRecords
    {
        [Key]
        public Guid PatientRecordId { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patients? Patient { get; set; }
        public Guid ComponentCode { get; set; }
        [ForeignKey("ComponentCode")]
        public RecordComponents? RecordComponent { get; set; }
        public string? PatientRecordComponentDetails { get; set; }
        public Guid UpdatedByStaffId { get; set; }
        [ForeignKey("UpdatedByStaffId")]
        public Staff? Staff { get; set; }
    }
}
