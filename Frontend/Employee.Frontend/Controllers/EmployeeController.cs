using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employee.Frontend.Controllers;

public class EmployeeController : Controller
{

    private readonly HttpClient _httpClient;

    public EmployeeController(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient("EmployeeApiBase");

    //public  async Task <IActionResult>Index()
    //{

    //    var data = await GetAllEmployee();
    //    return View(data);

    //}

    public async Task<IActionResult> Index() => View(await GetAllEmployee());






    //public async Task<IEnumerable<Employees>> GetAllEmployee()
    //{
    //    var response = await _httpClient.GetAsync("Employee");
    //    if (response.IsSuccessStatusCode)
    //    {
    //        var content = await response.Content.ReadAsStringAsync();
    //        var employees = JsonConvert.DeserializeObject<IEnumerable<Employees>>(content);
    //        return employees;
    //    }



    //    return new List<Employees>();
    //}

    public async Task<List<Employees>> GetAllEmployee()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Employees>>("Employee");
        return response is not null ? response : new List<Employees>();
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> AddOrEdit(int Id)
    {
        if (Id == 0)
        {
            return View(new Employees());

        }
        else
        {
            var data = await _httpClient.GetAsync($"Employee/{Id}");

            if (data.IsSuccessStatusCode)
            {
                var result = await data.Content.ReadFromJsonAsync<Employees>();
                return View(result);

            }
        }
        return View(new Employees());
    }


    [AutoValidateAntiforgeryToken]
    [HttpPost]

    public async Task<IActionResult> AddOrEdit(int Id, Employees employee)
    {
        if (ModelState.IsValid)
        {
            if (Id == 0)
            {

                var result = await _httpClient.PostAsJsonAsync("Employee", employee);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            else

            {
                var result = await _httpClient.PutAsJsonAsync($"Employee/{Id}", employee);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }


            }

        }


    }

}


