using Microsoft.AspNetCore.Http;
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
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // player
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var z = await _playerService.BrowseAllAsync();
            return Json(z);
        }

        // player/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var Player = await _playerService.GetAsync(id);
            return Json(Player);
        }

        // player
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] CreatePlayer Player)
        {
            await _playerService.AddAsync(Player);
            return Created("", Player);
        }

        // player/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPlayer([FromBody] UpdatePlayer Player, int id)
        {
            await _playerService.UpdateAsync(id, Player);
            return NoContent();
        }

        // player/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await _playerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
