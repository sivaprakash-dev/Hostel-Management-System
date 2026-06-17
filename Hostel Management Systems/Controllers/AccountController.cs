using Hostel_Management_Systems.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hostel_Management_Systems.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        public AccountController(
            IHttpClientFactory factory)
        {
            _client = factory.CreateClient();

            _client.BaseAddress =
                new Uri("https://localhost:7255/");
        }

        // =========================================
        // LOGIN PAGE
        // =========================================

        [HttpGet]
        public IActionResult Login()
        {
            var token =
                HttpContext.Session.GetString("token");

            var role =
                HttpContext.Session.GetString("Role");

            if (!string.IsNullOrEmpty(token))
            {
                if (role == "Admin")
                {
                    return RedirectToAction(
                        "Index",
                        "Dashboard");
                }

                if (role == "Student")
                {
                    return RedirectToAction(
                        "StudentIndex",
                        "StudentDashboard");
                }
            }

            return View();
        }

        // =========================================
        // LOGIN
        // =========================================

        [HttpPost]
        public async Task<IActionResult> Login(AdminLogin vm)
        {
            string apiUrl = "";

            if (vm.Role == "Admin")
            {
                apiUrl = "api/Auth/admin-login";
            }
            else if (vm.Role == "Student")
            {
                apiUrl = "api/Auth/student-login";
            }
            else
            {
                ViewBag.Message = "Select Role";
                return View(vm);
            }

            var json =
                JsonConvert.SerializeObject(vm);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _client.PostAsync(
                    apiUrl,
                    content);


            if (response.IsSuccessStatusCode)
            {
                var result =
                    await response.Content
                        .ReadAsStringAsync();

                var data =
                    JsonConvert.DeserializeObject<LoginResponse>(result);

                HttpContext.Session.SetString(
                    "token",
                    data!.Token!);

                HttpContext.Session.SetString(
                    "Role",
                    vm.Role!);

                HttpContext.Session.SetString(
                    "Email",
                    vm.Email!);   // ADD HERE

                if (vm.Role == "Admin")
                {
                    return RedirectToAction(
                        "Index",
                        "Dashboard");
                }

                return RedirectToAction(
                    "StudentIndex",
                    "StudentDashboard");
            }

            ViewBag.Message =
                "Invalid Email or Password";

            return View(vm);
        }

        // =========================================
        // STUDENT LOGIN
        // =========================================

        [HttpPost]
        public async Task<IActionResult> StudentLogin(Student vm)
        {
            var json =
                JsonConvert.SerializeObject(vm);


            if (vm.Role == "Admin")
            {
                // Admin Dashboard
                return RedirectToAction("Index", "AdminDashboard");
            }

            if (vm.Role == "Student")
            {
                // Student Dashboard
                return RedirectToAction("Index", "StudentDashboard");
            }

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _client.PostAsync(
                    "api/Auth/student-login",
                    content);

            if (response.IsSuccessStatusCode)
            {
                var result =
                    await response.Content
                        .ReadAsStringAsync();

                var data =
                    JsonConvert.DeserializeObject<LoginResponse>(result);

                HttpContext.Session.SetString(
                    "token",
                    data!.Token!);

                HttpContext.Session.SetString(
                    "Role",
                    "Student");

                HttpContext.Session.SetString(
                    "Email",
                    vm.Email!);

                return RedirectToAction(
                    "StudentIndex",
                    "StudentDashboard");
            }

            ViewBag.Message =
                "Invalid Email or Password";

            return View(vm);
        }

        // =========================================
        // LOGOUT
        // =========================================

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account");
        }
    }
}