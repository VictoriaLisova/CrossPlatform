using Lab5.Models.APILab6Models;
using Lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace Lab5.Controllers
{
    public class StaffPatientAssociationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;
        public StaffPatientAssociationsController(IHttpClientFactory httpClientFactory, TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public async Task<IActionResult> ListOfAssociations()
        {
            var responce = await _tokenService.GetToken();
            if(responce == HttpStatusCode.OK)
            {
                var api = _httpClientFactory.CreateClient();

                api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);

                var response = await api.GetAsync("https://localhost:7277/api/get-staff-patient-associations");
                var content = await response.Content.ReadAsStringAsync();
                var associations = JsonConvert.DeserializeObject<List<StaffPatientAssociations>>(content);
                if (associations != null)
                {
                    foreach (var association in associations)
                    {
                        var patient_response = await api.GetAsync($"https://localhost:7277/api/get-patient/{association.PatientId}");
                        var patient_content = await patient_response.Content.ReadAsStringAsync();
                        var patient = JsonConvert.DeserializeObject<Patients>(patient_content);

                        var staff_response = await api.GetAsync($"https://localhost:7277/api/get-staff/{association.StaffId}");
                        var staff_content = await staff_response.Content.ReadAsStringAsync();
                        var staff = JsonConvert.DeserializeObject<Staff>(staff_content);

                        if (patient != null && staff != null)
                        {
                            association.Staff = staff;
                            association.Patient = patient;
                        }
                    }
                    return View(associations);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Details(Guid id)
        {
            var responce = await _tokenService.GetToken();
            if(responce == HttpStatusCode.OK)
            {
                var api = _httpClientFactory.CreateClient();

                api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);

                var response = await api.GetAsync($"https://localhost:7277/api/get-association/{id}");
                var content = await response.Content.ReadAsStringAsync();
                var association = JsonConvert.DeserializeObject<StaffPatientAssociations>(content);
                if (association != null)
                {
                    var patient_response = await api.GetAsync($"https://localhost:7277/api/get-patient/{association.PatientId}");
                    var patient_content = await patient_response.Content.ReadAsStringAsync();
                    var patient = JsonConvert.DeserializeObject<Patients>(patient_content);

                    var staff_response = await api.GetAsync($"https://localhost:7277/api/get-staff/{association.StaffId}");
                    var staff_content = await staff_response.Content.ReadAsStringAsync();
                    var staff = JsonConvert.DeserializeObject<Staff>(staff_content);

                    if (patient != null && staff != null)
                    {
                        association.Staff = staff;
                        association.Patient = patient;

                        var category_code_responce = await api.GetAsync($"https://localhost:7277/api/staff-category/{staff.StaffCategoryCode}");
                        var category_code_content = await category_code_responce.Content.ReadAsStringAsync();
                        var category = JsonConvert.DeserializeObject<StaffCategories>(category_code_content);

                        var role_responce = await api.GetAsync($"https://localhost:7277/api/get-role/{staff.RoleCode}");
                        var role_content = await role_responce.Content.ReadAsStringAsync();
                        var role = JsonConvert.DeserializeObject<Roles>(role_content);

                        if (role != null && category != null)
                        {
                            association.Staff.Role = role;
                            association.Staff.StaffCategory = category;
                        }
                    }
                    return View(association);
                }
            }
            return RedirectToAction("ListOfAssociations");
        }

        [HttpPost]
        public IActionResult Back()
        {
            return RedirectToAction("ListOfAssociations");
        }
    }
}
