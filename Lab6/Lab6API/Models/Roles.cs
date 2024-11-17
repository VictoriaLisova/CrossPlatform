using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Roles
    {
        [Key]
        //[Column("role_code")]
        public Guid RoleCode { get; set; }
        //[Column("role_description")]
        public string? RoleDescription { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
