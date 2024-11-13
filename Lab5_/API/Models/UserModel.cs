using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class UserModel : IdentityUser
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
