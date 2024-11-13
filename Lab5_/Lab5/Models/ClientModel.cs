using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class ClientModel
    {
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(500)]
        [Required]
        public string FullName { get; set; }
        [MinLength(8)]
        [MaxLength(16)]
        [Required]
        public string Password { get; set; }
        [MinLength(8)]
        [MaxLength(16)]
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }   
    }
}
