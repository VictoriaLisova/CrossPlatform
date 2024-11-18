using Lab5.Models.APILab6Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab5.Controllers
{
	public class RolesController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        public RolesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> RolesList()
		{
            var api = _httpClientFactory.CreateClient();
            var response = await api.GetAsync("https://localhost:7277/api/roles-list");
            var content = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<Roles>>(content);
            if (roles.Any())
            {
                return View(roles);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Details(Guid id)
        {
            var api = _httpClientFactory.CreateClient();
            var response = await api.GetAsync($"https://localhost:7277/api/get-role/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var role = JsonConvert.DeserializeObject<Roles>(content);
            if(role != null)
            {
                return View(role);
            }
            return RedirectToAction("RoleList");
        }

        [HttpPost]
        public IActionResult Back()
        {
            return RedirectToAction("RolesList");
        }
	}
}
