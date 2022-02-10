using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> BrowseAllAsync();
        Task<Player> GetAsync(int id);
        Task UpdateAsync(Player p);
        Task DeleteAsync(Player p);
        Task AddAsync(Player p);
    }
}
