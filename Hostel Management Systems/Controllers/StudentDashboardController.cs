using Hostel_Management_Systems.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hostel_Management_Systems.Controllers
{
    public class StudentDashboardController : Controller
    {
        private readonly HttpClient _client;

        public StudentDashboardController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();

            _client.BaseAddress =
                new Uri("https://localhost:7255/");
        }

        private void AddToken()
        {
            var token =
                HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers
                    .AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }

        public async Task<IActionResult> StudentIndex()
        {
            AddToken();

            var response =
                await _client.GetAsync(
                    "api/Student/my-profile");

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }

            var json =
                await response.Content.ReadAsStringAsync();

            var student =
                JsonConvert.DeserializeObject<Student>(json);

            return View(student);
        }

        public async Task<IActionResult> MyRoom()
        {
            AddToken();

            var response =
                await _client.GetAsync(
                    "api/RoomAllocation/my-room");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    await response.Content.ReadAsStringAsync();

                return View();
            }

            var json =
                await response.Content.ReadAsStringAsync();

            dynamic data =
                JsonConvert.DeserializeObject<dynamic>(json);

            return View(data);
        }

        public async Task<IActionResult> MyFees()
        {
            AddToken();

            var response =
                await _client.GetAsync(
                    "api/Fees/my-fees");

            var json =
                await response.Content.ReadAsStringAsync();

            var data =
                JsonConvert.DeserializeObject<List<Fees>>(json);

            return View(data);
        }

        public async Task<IActionResult> MyComplaints()
        {
            AddToken();

            var response =
                await _client.GetAsync(
                    "api/Complaint/my-complaints");

            var json =
                await response.Content.ReadAsStringAsync();

            var data =
                JsonConvert.DeserializeObject<List<Complaint>>(json);

            return View(data);
        }
    }
}
