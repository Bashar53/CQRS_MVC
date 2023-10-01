using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        var response = await _httpClient.GetAsync("Country");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var countryList = JsonConvert.DeserializeObject<List<Country>>(content);
            ViewData["countryId"] = new SelectList(countryList, "Id", "CountryName");
        }
        var response2 = await _httpClient.GetAsync("State");
        if (response.IsSuccessStatusCode)
        {
            var content = await response2.Content.ReadAsStringAsync();
            var countryList = JsonConvert.DeserializeObject<List<State>>(content);
            ViewData["stateId"] = new SelectList(countryList, "Id", "StateName");

        }

        if (Id == 0)
        {
            //create form
            return View(new Employees());

        }
        else
        {

            //update get By Id
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

        return View(new Employees());

    }

    public async Task<IActionResult> Delete(int Id)
    {
        var data = await _httpClient.DeleteAsync($"Employee/{Id}");

        if (data.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        else
        {
            return NotFound();
        }
    }


    //[AutoValidateAntiforgeryToken]
    //[HttpPost]

    //public async Task<IActionResult> AddOrEdit(int Id, Employees employee)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (Id == 0)
    //        {

    //            var result = await _httpClient.PostAsJsonAsync("Employee", employee);
    //            if (result.IsSuccessStatusCode)
    //            {
    //                return RedirectToAction("Index");
    //            }
    //        }

    //        else

    //        {
    //            var result = await _httpClient.PutAsJsonAsync($"Employee/{Id}", employee);

    //            if (result.IsSuccessStatusCode)
    //            {
    //                return RedirectToAction("Index");

    //            }


    //        }

    //    }


    //}

}


