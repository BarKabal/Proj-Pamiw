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
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // session
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var z = await _sessionService.BrowseAllAsync();
            return Json(z);
        }

        // session/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(int id)
        {
            var Session = await _sessionService.GetAsync(id);
            return Json(Session);
        }

        // session
        [HttpPost]
        public async Task<IActionResult> AddSession([FromBody] CreateSession Session)
        {
            await _sessionService.AddAsync(Session);
            return Created("", Session);
        }

        // session/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSession([FromBody] UpdateSession Session, int id)
        {
            await _sessionService.UpdateAsync(id, Session);
            return NoContent();
        }

        // session/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            await _sessionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
