using Employee.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employee.Frontend.Controllers;

public class StateController : Controller
{
    private readonly HttpClient _httpClient;
    public StateController(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient("EmployeeApiBase");

    //public async Task<IActionResult> Index()
    //{

    //    var data = await GetAllState();
    //    return View(data);

    //}

    public async Task<IActionResult> Index() => View(await GetAllState());

    //public async Task<IEnumerable<State>> GetAllState()
    //{
    //    var response = await _httpClient.GetAsync("State");
    //    if (response.IsSuccessStatusCode)
    //    {
    //        var content = await response.Content.ReadAsStringAsync();
    //        var states = JsonConvert.DeserializeObject<IEnumerable<State>>(content);
    //        return states;
    //    }


    //    return new List<State>();
    //}


    public async Task<List<State>> GetAllState()
    {
        var response = await _httpClient.GetFromJsonAsync<List<State>>("State");
        return response is not null ? response : new List<State>();
    }



    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> AddOrEdit(int Id)
    {
        if (Id == 0)
        {
            return View(new State());

        }
        else
        {
            var data = await _httpClient.GetAsync($"State/{Id}");

            if (data.IsSuccessStatusCode)
            {
                var result = await data.Content.ReadFromJsonAsync<State>();
                return View(result);

            }
        }
        return View(new State());
    }
    [AutoValidateAntiforgeryToken]
    [HttpPost]

    public async Task<IActionResult> AddOrEdit(int Id, State state)
    {
        if (ModelState.IsValid)
        {
            if (Id == 0)
            {

                var result = await _httpClient.PostAsJsonAsync("State", state);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            else

            {
                var result = await _httpClient.PutAsJsonAsync($"State/{Id}", state);

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }


            }

        }

        return View(new State());

    }

    public async Task<IActionResult> Delete(int Id)
    {
        var data = await _httpClient.DeleteAsync($"State/{Id}");

        if (data.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        else
        {
            return NotFound();
        }
    }




    
}
