using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Patients
    {
        [Key]
        //[Column("patient_id")]
        public Guid PattientId { get; set; }
        //[Column("comanaged_yn")]
        public string? CommanagedYn { get; set; }
        //[Column("nhs_number")]
        [StringLength(10)]
        public string? NhsNumber { get; set; }
        //[Column("gender")]  
        public string? Gender { get; set; }
        //[Column("date_of_birth")]
        public DateTimeOffset DateOfBirth { get; set; }
        //[Column("patient_name")]
        public string? PatientName { get; set; }
        //[Column("patient_address")]
        public string? PatientAddress { get; set; }
        //[Column("height")]
        public float Height { get; set; }
        //[Column("weight")]
        public float Weight { get; set; }
        //[Column("other_patient_details")]
        public string? Details { get; set; }
        public ICollection<StaffPatientAssociations>? Associations { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
        public ICollection<Appointments>? Appointments { get; set; }
    }
}
