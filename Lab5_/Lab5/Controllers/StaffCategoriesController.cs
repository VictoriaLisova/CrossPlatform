using Lab5.Models.APILab6Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab5.Controllers
{
	public class StaffCategoriesController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public StaffCategoriesController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Categories()
		{
            var api = _httpClientFactory.CreateClient();
            var response = await api.GetAsync("https://localhost:7277/api/staff-categories");
            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<StaffCategories>>(content);
			if (categories.Any())
			{
                return View(categories);
            }
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Details(Guid id)
		{
            var api = _httpClientFactory.CreateClient();
            var response = await api.GetAsync($"https://localhost:7277/api/staff-category/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<StaffCategories>(content);
			if(category != null)
			{
                return View(category);
            }
			return RedirectToAction("Categories");
		}

		[HttpPost]
		public IActionResult Back()
		{
			return RedirectToAction("Categories");
		}
	}
}
