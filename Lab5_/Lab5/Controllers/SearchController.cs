using Lab5.Models.APILab6Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace Lab5.Controllers
{
	public class SearchController : Controller
	{
		private readonly IHttpClientFactory httpClientFactory;
		public SearchController(IHttpClientFactory httpClientFactory)
		{
			this.httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public IActionResult Search()
		{
			// check if has temp data
			if (TempData["Appointments"] != null)
			{
				// deserialize data to appointments list and pass on view
				var appointments = JsonConvert.DeserializeObject<List<Appointments>>(TempData["Appointments"].ToString());
				ViewBag.Appointments = appointments;
			}
			else if (TempData["Patients"] != null)
			{
                var patients = JsonConvert.DeserializeObject<List<Patients>>(TempData["Patients"].ToString());
                ViewBag.Patients = patients;
            }
            else if (TempData["Staff"] != null)
            {
                var staff = JsonConvert.DeserializeObject<List<StaffForJoin>>(TempData["Staff"].ToString());
                ViewBag.Staff = staff;
            }
            return View();
		}
        [HttpGet]
		public async Task<IActionResult> SearchByDateTime(string startDate, string startTime, string endDate, string endTime)
		{
            TempData["Appointments"] = new List<Appointments>();
            if (startDate != null && startTime != null && endDate != null && endTime != null)
			{
                var startDateList = startDate.Split('-').Select(x => int.Parse(x)).ToList();
                var startTimeList = startTime.Split(':').Select(x => int.Parse(x)).ToList();
                var endDateList = endDate.Split('-').Select(x => int.Parse(x)).ToList();
                var endTimeList = endTime.Split(':').Select(x => int.Parse(x)).ToList();

				var startDateTime = new DateTime(startDateList[0], startDateList[1], startDateList[2], startTimeList[0], startTimeList[1], 0);
				var endDateTime = new DateTime(endDateList[0], endDateList[1], endDateList[2], endTimeList[0], endTimeList[1], 0);

				var startSpecify = DateTime.SpecifyKind(startDateTime, DateTimeKind.Utc);
                var endSpecify = DateTime.SpecifyKind(endDateTime, DateTimeKind.Utc);

				// convert datatime to transport format
				var finalStartDateTime = new DateTimeOffset(startSpecify).ToString("O");
                var finalEndDateTime = new DateTimeOffset(endSpecify).ToString("O");

				var api = httpClientFactory.CreateClient();
                var response = await api.GetAsync($"https://localhost:7277/api/get-by-datetime?start={Uri.EscapeDataString(finalStartDateTime)}&end={Uri.EscapeDataString(finalEndDateTime)}");
                var content = await response.Content.ReadAsStringAsync();
                var appointments = JsonConvert.DeserializeObject<List<Appointments>>(content);

				// convert data to json to send to search controller 
				TempData["Appointments"] = JsonConvert.SerializeObject(appointments);
				
				return RedirectToAction("Search");
            }
			return RedirectToAction("Search");
		}

		[HttpGet]
		public async Task<IActionResult> SearchBySubstring(string substring)
		{
            TempData["Patients"] = new List<Patients>();
            if (!string.IsNullOrEmpty(substring))
			{
                var api = httpClientFactory.CreateClient();
                var response = await api.GetAsync($"https://localhost:7277/api/get-by-substring?substring={substring}");
                var content = await response.Content.ReadAsStringAsync();
                var patients = JsonConvert.DeserializeObject<List<Patients>>(content);

                TempData["Patients"] = JsonConvert.SerializeObject(patients);

                return RedirectToAction("Search");
            }
			return RedirectToAction("Search");
		}

		[HttpGet]
		public async Task<IActionResult> SearchById(string idLine)
		{
            TempData["Staff"] = new List<Staff>();
            if (!string.IsNullOrEmpty(idLine))
			{
                var api = httpClientFactory.CreateClient();
                var response = await api.GetAsync($"https://localhost:7277/api/get-by-list-elements?stuffIdList={idLine}");
                var content = await response.Content.ReadAsStringAsync();
                var staff = JsonConvert.DeserializeObject<List<StaffForJoin>>(content);

                TempData["Staff"] = JsonConvert.SerializeObject(staff);
            }
			return RedirectToAction("Search");
		}
	}
}
