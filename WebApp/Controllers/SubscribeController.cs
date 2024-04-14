using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Unicode;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SubscribeController(HttpClient httpClient) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;

        [HttpPost]
        public async Task<IActionResult> Register(SubscribeViewModel form)
        {
            if(ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json" );
                var response = await _httpClient.PostAsync("https://localhost:7177/api/subscribe", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Thank you for subscribing";
                    return RedirectToAction("Home", "Default", "subscribe");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict) 
                {
                    TempData["Status"] = "You are already subscribed";
                    return RedirectToAction("Home", "Default", "subscribe");
                }
            }
           
            TempData["Status"] = "Ops, something went wrong";
            return RedirectToAction("Home", "Default", "subscribe");

        }
    }
}
