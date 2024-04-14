using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers;
[Authorize]

public class CoursesController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private string _categoryApi = "https://localhost:7124/api/Category";
    private string _courseApi = "https://localhost:7124/api/Courses";
    public async Task <IActionResult> Courses(string category="", string searchQuery ="",int pageNumber=1, int pageSize=6)
    {
        var model = new CoursesViewModel();

        var categoryResponse = await _httpClient.GetAsync(_categoryApi);
        if(categoryResponse.IsSuccessStatusCode) 
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<Category>>(await categoryResponse.Content.ReadAsStringAsync());
            if(result != null)
            {
                model.Categories = result;
            }
        }

        var courseResponse = await _httpClient.GetAsync($"{_courseApi}?category={Uri.EscapeDataString(category)}&searchQuery={Uri.EscapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");
        if(courseResponse.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResult>(await courseResponse.Content.ReadAsStringAsync());
            if(result != null)
            {
                model.Courses = result.Courses;
                model.pagination = new Pagination
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = result.TotalPages,
                    TotalCount =result.TotalItems
                };

            }

        }

        return View(model);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.GetAsync($"{_courseApi}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<SingleCourse>(await response.Content.ReadAsStringAsync());
                if (result != null)
                {
                    var model = new SingleCourse
                    {
                        Id = id,
                        Title = result.Title,
                        Author = result.Author,
                        IsBestseller = result.IsBestseller,
                        IsDigital = result.IsDigital,
                        NumberofLikes = result.NumberofLikes,
                        Hours = result.Hours,
                        DiscountedPrice = result.DiscountedPrice,
                        OriginalPrice = result.OriginalPrice,
                        LikesInProcent = result.LikesInProcent,
                        CourseDescription = result.CourseDescription,
                        learn1 = result.learn1,
                        learn2 = result.learn2,
                        learn3 = result.learn3,
                        learn4 = result.learn4,
                        learn5 = result.learn5,
                        programdetails1 = result.programdetails1,
                        programdetails2 = result.programdetails2,
                        programdetails3 = result.programdetails3,
                        programdetails4 = result.programdetails4,
                        programdetails5 = result.programdetails5,
                        programdetails6 = result.programdetails6,
                    };
                    return View(model);
                }

            }

        }
        return BadRequest();

    }
}