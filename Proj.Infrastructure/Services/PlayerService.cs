using Proj.Core.Domain;
using Proj.Core.Repositories;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task AddAsync(CreatePlayer a)
        {
            await _playerRepository.AddAsync(Map(a));
        }

        public async Task<IEnumerable<PlayerDTO>> BrowseAllAsync()
        {
            var z = await _playerRepository.BrowseAllAsync();
            return z.Select(x => Map(x));
        }

        public async Task DeleteAsync(int id)
        {
            var Player = _playerRepository.GetAsync(id).Result;
            await _playerRepository.DeleteAsync(Player);
        }

        public async Task<PlayerDTO> GetAsync(int id)
        {
            var a = _playerRepository.GetAsync(id).Result;
            return await Task.FromResult(Map(a));
        }

        public async Task UpdateAsync(int id, UpdatePlayer a)
        {
            await _playerRepository.UpdateAsync(Map(a, id));
        }

        private PlayerDTO Map(Player p)
        {
            return new PlayerDTO()
            {
                Id = p.Id,
                Name = p.Name,
                UserId = p.UserId
            };
        }

        private Player Map(CreatePlayer p)
        {
            return new Player()
            {
                Name = p.Name,
                UserId = p.UserId
            };
        }

        private Player Map(UpdatePlayer p, int id)
        {
            return new Player()
            {
                Id = id,
                Name = p.Name,
                UserId = p.UserId
            };
        }
    }
}
