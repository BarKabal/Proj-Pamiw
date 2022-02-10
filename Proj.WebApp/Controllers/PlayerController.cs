using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Proj.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Proj.WebApp.Controllers
{
    public class PlayerController : Controller 
    { 

    public IConfiguration Configuration;
    public JWToken JWToken;

    public PlayerController(IConfiguration configuration)
    {
        Configuration = configuration;
        JWToken = new JWToken(configuration);
    }

    public ContentResult GetHostURL()
    {
        var result = Configuration["RestApiUrl:HostUrl"];
        return Content(result);
    }

    private string ControllerName()
    {
        string cn = ControllerContext.RouteData.Values["controller"].ToString();
        return cn;
    }

    public async Task<IActionResult> Index()
    {
        var tokenString = JWToken.TokenString;

        string _restpath = GetHostURL().Content + ControllerName();

        List<PlayerVM> campaignList = new List<PlayerVM>();

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(_restpath))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                campaignList = JsonConvert.DeserializeObject<List<PlayerVM>>(apiResponse);
            }
        }
        return View(campaignList);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        string _restpath = GetHostURL().Content + ControllerName();

        PlayerVM c = new PlayerVM();

        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                c = JsonConvert.DeserializeObject<PlayerVM>(apiResponse);
            }
        }
        return View(c);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(UpdatePlayer p)
    {
        var tokenString = JWToken.TokenString;

        string _restpath = GetHostURL().Content + ControllerName();

        UpdatePlayer result = new UpdatePlayer();
        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonString = System.Text.Json.JsonSerializer.Serialize(p);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.PutAsync($"{_restpath}/{p.Id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UpdatePlayer>(apiResponse);
                }
            }
        }
        catch (Exception ex)
        {
            return View(ex);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        CreatePlayer t = new CreatePlayer();
        return await Task.Run(() => View(t));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreatePlayer p)
    {
        var tokenString = JWToken.TokenString;

        string _restpath = GetHostURL().Content + ControllerName();

        PlayerVM result = new PlayerVM();
        try
        {
            using (var httpClient = new HttpClient())
            {
                string jsonString = System.Text.Json.JsonSerializer.Serialize(p);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.PostAsync($"{_restpath}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<PlayerVM>(apiResponse);
                }
            }
        }
        catch (Exception ex)
        {
            return View(ex);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var tokenString = JWToken.TokenString;

        string _restpath = GetHostURL().Content + ControllerName();

        PlayerVM result = new PlayerVM();
        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<PlayerVM>(apiResponse);
                }
            }
        }
        catch (Exception ex)
        {
            return View(ex);
        }

        return RedirectToAction(nameof(Index));
    }
}
}
