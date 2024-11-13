using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccessController : Controller
    {
        [Route("api/secret")]
        [Authorize]
        public IActionResult Index()
        {
            var claims = User.Claims.ToList();
            if (claims.Any())
            {
                return Ok("successs");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
