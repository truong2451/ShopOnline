using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopDB.Client.Models;
using ShopDB.Repositories.EntityModel;
using System.Text.Json;

namespace ShopDB.Client.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string URLApi = "https://localhost:7255/api/Product/";

        public ProductsController()
        {
            client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> View(string? search)
        {
            HttpResponseMessage response = await client.GetAsync(URLApi + $"GetAll?search={search}");
            string strData = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> list = new List<Product>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (json["message"].ToString() == "Success")
                {
                    list = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json["data"].ToString(), options);
                }
            }

            return View(new ProductsVM
            {
                Search = search,
                Products = list
            });
        } 
    }
}
