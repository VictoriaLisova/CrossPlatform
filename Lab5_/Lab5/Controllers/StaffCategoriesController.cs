using Lab5.Models.APILab6Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Lab5.Controllers
{
	public class StaffCategoriesController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly TokenService _tokenService;
		public StaffCategoriesController(IHttpClientFactory httpClientFactory, TokenService tokenService)
		{
			_httpClientFactory = httpClientFactory;
			_tokenService = tokenService;
		}

		public async Task<IActionResult> Categories()
		{
			var responce = await _tokenService.GetToken();
			if(responce == HttpStatusCode.OK)
			{
                var api = _httpClientFactory.CreateClient();

                api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);

                var response = await api.GetAsync("https://localhost:7277/api/staff-categories");
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<StaffCategories>>(content);
                if (categories.Any())
                {
                    return View(categories);
                }
            }
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Details(Guid id)
		{
            var responce = await _tokenService.GetToken();
            if (responce == HttpStatusCode.OK)
			{
                var api = _httpClientFactory.CreateClient();

                api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);

                var response = await api.GetAsync($"https://localhost:7277/api/staff-category/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<StaffCategories>(content);
                if (category != null)
                {
                    return View(category);
                }
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
