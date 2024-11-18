using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class StaffCategories
    {
        [Key]
        public Guid StaffCategoryCode { get; set; }
        public string? StaffCategoryDescription { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
