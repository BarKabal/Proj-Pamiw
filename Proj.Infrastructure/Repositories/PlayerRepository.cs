using Proj.Core.Domain;
using Proj.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private AppDbContext _appDbContext;

        public PlayerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Player p)
        {
            try
            {
                _appDbContext.Player.Add(p);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Player>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Player);
        }

        public async Task DeleteAsync(Player p)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Player.FirstOrDefault(x => x.Id == p.Id));
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Player> GetAsync(int id)
        {
            return await Task.FromResult(_appDbContext.Player.FirstOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(Player p)
        {
            try
            {
                var z = _appDbContext.Campaign.FirstOrDefault(x => x.Id == p.Id);

                z.Name = p.Name;

                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }
    }
}
