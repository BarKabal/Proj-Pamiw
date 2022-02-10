using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // announcement
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var z = await _announcementService.BrowseAllAsync();
            return Json(z);
        }

        // announcement/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement(int id)
        {
            var Announcement = await _announcementService.GetAsync(id);
            return Json(Announcement);
        }

        // announcement
        [HttpPost]
        public async Task<IActionResult> AddAnnouncement([FromBody] CreateAnnouncement Announcement)
        {
            await _announcementService.AddAsync(Announcement);
            return Created("", Announcement);
        }

        // announcement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAnnouncement([FromBody] UpdateAnnouncement Announcement, int id)
        {
            await _announcementService.UpdateAsync(id, Announcement);
            return NoContent();
        }

        // announcement/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await _announcementService.DeleteAsync(id);
            return NoContent();
        }
    }
}
