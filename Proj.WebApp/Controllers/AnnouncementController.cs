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
    public class AnnouncementController : Controller
    {

        public IConfiguration Configuration;
        public JWToken JWToken;

        public AnnouncementController(IConfiguration configuration)
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

            List<AnnouncementVM> campaignList = new List<AnnouncementVM>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    campaignList = JsonConvert.DeserializeObject<List<AnnouncementVM>>(apiResponse);
                }
            }
            return View(campaignList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostURL().Content + ControllerName();

            AnnouncementVM c = new AnnouncementVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<AnnouncementVM>(apiResponse);
                }
            }
            return View(c);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UpdateAnnouncement a)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            UpdateAnnouncement result = new UpdateAnnouncement();
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
                        result = JsonConvert.DeserializeObject<UpdateAnnouncement>(apiResponse);
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
            CreateAnnouncement t = new CreateAnnouncement();
            return await Task.Run(() => View(t));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAnnouncement a)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            AnnouncementVM result = new AnnouncementVM();
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
                        result = JsonConvert.DeserializeObject<AnnouncementVM>(apiResponse);
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

            AnnouncementVM result = new AnnouncementVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<AnnouncementVM>(apiResponse);
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
