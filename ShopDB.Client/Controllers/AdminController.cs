using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace ShopDB.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient client = null;
        private string URLApi = "https://localhost:7255/api/StaffAccount/";

        public AdminController()
        {
            client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            HttpResponseMessage response = await client.PostAsync(URLApi + $"Login?username={username}&password={password}", null);
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("Role", json["role"].ToString());
                HttpContext.Session.SetString("Token", json["token"].ToString());

                if (json["role"].ToString() == "Admin" || json["role"].ToString() == "Staff")
                {
                    return RedirectToAction("View", "Products");
                }

                if (json["role"].ToString() == "Customer")
                {
                    return RedirectToAction("ViewClient", "Products");
                }
            }

            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound
                || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                TempData["LoginFail"] = json["message"].ToString();
                return RedirectToAction(nameof(Login));
            }

            if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                TempData["LoginFail"] = json["message"].ToString();
                return RedirectToAction("Error", "Shared");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")))
            {
                HttpContext.Session.Remove("Token");
                HttpContext.Session.Remove("Role");
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
