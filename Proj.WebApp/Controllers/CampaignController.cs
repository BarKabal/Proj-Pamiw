using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Proj.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Proj.WebApp.Controllers
{
    public class CampaignController : Controller
    {
        public IConfiguration Configuration;
        public JWToken JWToken;

        public CampaignController(IConfiguration configuration)
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

            List<CampaignVM> campaignList = new List<CampaignVM>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    campaignList = JsonConvert.DeserializeObject<List<CampaignVM>>(apiResponse);
                }
            }
            return View(campaignList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostURL().Content + ControllerName();

            CampaignVM c = new CampaignVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<CampaignVM>(apiResponse);
                }
            }
            return View(c);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UpdateCampaign a)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            UpdateCampaign result = new UpdateCampaign();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(a);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.PutAsync($"{_restpath}/{a.Id}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<UpdateCampaign>(apiResponse);
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
            CreateCampaign t = new CreateCampaign();
            return await Task.Run(() => View(t));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCampaign a)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            CampaignVM result = new CampaignVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(a);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.PostAsync($"{_restpath}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<CampaignVM>(apiResponse);
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

            CampaignVM result = new CampaignVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<CampaignVM>(apiResponse);
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
