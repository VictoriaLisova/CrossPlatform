using IdentityModel.Client;
using Lab5.Models.APILab6Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Lab5.Controllers
{
	public class RolesController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;
        public RolesController(IHttpClientFactory httpClientFactory, TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public async Task<IActionResult> RolesList()
		{
            var responce_stauscode = await _tokenService.GetToken();
            if(responce_stauscode == HttpStatusCode.OK)
            {
                var api = _httpClientFactory.CreateClient();

                api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);

                var response = await api.GetAsync("https://localhost:7277/api/roles-list");
                var content = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<Roles>>(content);
                if (roles.Any())
                {
                    return View(roles);
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

                var response = await api.GetAsync($"https://localhost:7277/api/get-role/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var role = JsonConvert.DeserializeObject<Roles>(content);
                if (role != null)
                {
                    return View(role);
                }
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
