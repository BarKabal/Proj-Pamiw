using Microsoft.AspNetCore.Mvc;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using Proj.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        // campaign
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var z = await _campaignService.BrowseAll();
            return Json(z);
        }

        // campaign/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaign(int id)
        {
            var Campaign = await _campaignService.GetAsync(id);
            return Json(Campaign);
        }

        // campaign/filter?name={name}
        [HttpGet("filter")]
        public async Task<IActionResult> GetCampaignByName(string name)
        {
            IEnumerable<CampaignDTO> Campaigns = await _campaignService.BrowseAllByFilterAsync(name);
            return Json(Campaigns);
        }

        // campaign
        [HttpPost]
        public async Task<IActionResult> AddCampaign([FromBody] CreateCampaign Campaign)
        {
            await _campaignService.AddAsync(Campaign);
            return Created("", Campaign);
        }

        // campaign/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCampaign([FromBody] UpdateCampaign Campaign, int id)
        {
            await _campaignService.UpdateAsync(id, Campaign);
            return NoContent();
        }

        // campaign/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            await _campaignService.DeleteAsync(id);
            return NoContent();
        }
    }
}
