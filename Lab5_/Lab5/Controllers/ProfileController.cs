using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index(ClientModel client)
        {
            return View(client);
        }
    }
}
