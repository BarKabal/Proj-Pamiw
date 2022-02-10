using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Repositories
{
    public interface ICampaignRepository
    {
        Task<IEnumerable<Campaign>> BrowseAllAsync();
        Task<IEnumerable<Campaign>> BrowseAllByFilterAsync(string name);
        Task<Campaign> GetAsync(int id);
        Task UpdateAsync(Campaign c);
        Task DeleteAsync(Campaign c);
        Task AddAsync(Campaign c);
    }
}
