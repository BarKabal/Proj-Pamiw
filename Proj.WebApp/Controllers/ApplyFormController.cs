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
    public class ApplyFormController : Controller
    {

        public IConfiguration Configuration;
        public JWToken JWToken;

        public ApplyFormController(IConfiguration configuration)
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

            List<ApplyFormVM> campaignList = new List<ApplyFormVM>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    campaignList = JsonConvert.DeserializeObject<List<ApplyFormVM>>(apiResponse);
                }
            }
            return View(campaignList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostURL().Content + ControllerName();

            ApplyFormVM c = new ApplyFormVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c = JsonConvert.DeserializeObject<ApplyFormVM>(apiResponse);
                }
            }
            return View(c);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UpdateApplyForm p)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            UpdateApplyForm result = new UpdateApplyForm();
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
                        result = JsonConvert.DeserializeObject<UpdateApplyForm>(apiResponse);
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
            CreateApplyForm t = new CreateApplyForm();
            return await Task.Run(() => View(t));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateApplyForm p)
        {
            var tokenString = JWToken.TokenString;

            string _restpath = GetHostURL().Content + ControllerName();

            ApplyFormVM result = new ApplyFormVM();
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
                        result = JsonConvert.DeserializeObject<ApplyFormVM>(apiResponse);
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

            ApplyFormVM result = new ApplyFormVM();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApplyFormVM>(apiResponse);
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
