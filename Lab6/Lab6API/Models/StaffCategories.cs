using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class StaffCategories
    {
        [Key]
        //[Column("staff_category_code")]
        public Guid StaffCategoryCode { get; set; }
        //[Column("staff_category_description")]
        public string? StaffCategoryDescription { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
