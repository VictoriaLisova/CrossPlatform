using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class RecordComponents
    {
        [Key]
        //[Column("component_code")]
        public Guid ComponentCode { get; set; }
        //[Column("component_description")]
        public string? ComponentDescription { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
    }
}
