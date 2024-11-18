using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class RecordComponents
    {
        [Key]
        public Guid ComponentCode { get; set; }
        public string? ComponentDescription { get; set; }
        public ICollection<PatientRecords>? PatientRecords { get; set; }
    }
}
