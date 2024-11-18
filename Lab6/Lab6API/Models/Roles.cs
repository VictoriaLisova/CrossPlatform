using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6API.Models
{
    public class Roles
    {
        [Key]
        public Guid RoleCode { get; set; }
        public string? RoleDescription { get; set; }
        public ICollection<Staff>? Staffs { get; set; }
    }
}
