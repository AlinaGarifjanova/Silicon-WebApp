using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private string _optionApi = "https://localhost:7004/api/Option";

    public IActionResult ContactPage()
    {
        var model = new ContactFormViewModel();

        var optionResponse = _httpClient.GetAsync(_optionApi).Result;
        if (optionResponse.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<Option>>(optionResponse.Content.ReadAsStringAsync().Result);
            if (result != null)
            {
                model.Options = result;

            }
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> GetInTouch(ContactFormViewModel model)
    {
        if (ModelState.IsValid)
        {  
            try
            {

                var contact = model.Contact;
                var content = new StringContent(JsonConvert.SerializeObject(contact),Encoding.UTF8,"application/json");
                var response = await _httpClient.PostAsync("https://localhost:7004/api/Contact", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Thank you for your message we will get back to you";
                    return RedirectToAction("ContactPage", "Contact");
                }
                else 
                
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["Status"] = "You have already messaged us";
                    return RedirectToAction("ContactPage", "Contact");
                }
            }
            catch (HttpRequestException ex)
            {
           
                TempData["Status"] = "Something went wrong with the request, please try again later";
            }


        }
        return View(model);

    }
}
 
    
  