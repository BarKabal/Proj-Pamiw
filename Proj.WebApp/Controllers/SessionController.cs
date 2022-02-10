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
    public class SessionController : Controller
    {
        public IConfiguration Configuration;
        public JWToken JWToken;

        public SessionController(IConfiguration configuration)
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

            List<SessionVM> campaignList = new List<SessionVM>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    campaignList = JsonConvert.DeserializeObject<List<SessionVM>>(apiResponse);
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
        public async Task<IActionResult> Edit(UpdateSession s)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            UpdateSession result = new UpdateSession();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.PutAsync($"{_restpath}/{s.Id}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<UpdateSession>(apiResponse);
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
            CreateSession t = new CreateSession();
            return await Task.Run(() => View(t));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateSession s)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            SessionVM result = new SessionVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(s);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.PostAsync($"{_restpath}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<SessionVM>(apiResponse);
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

            SessionVM result = new SessionVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<SessionVM>(apiResponse);
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
