using Proj.Core.Domain;
using Proj.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private AppDbContext _appDbContext;

        public CampaignRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        async public Task AddAsync(Campaign c)
        {
            try {
                _appDbContext.Campaign.Add(c);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Campaign>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Campaign);
        }

        public async Task<IEnumerable<Campaign>> BrowseAllByFilterAsync(string name)
        {
            var s = _appDbContext.Campaign.Where(x => x.Name.Contains(name)).AsEnumerable();
            return await Task.FromResult(s);
        }

        async public Task DeleteAsync(Campaign c)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Campaign.FirstOrDefault(x => x.Id == c.Id));
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        async public Task<Campaign> GetAsync(int id)
        {
            return await Task.FromResult(_appDbContext.Campaign.FirstOrDefault(x => x.Id == id));
        }

        async public Task UpdateAsync(Campaign c)
        {
            try
            {
                var z = _appDbContext.Campaign.FirstOrDefault(x => x.Id == c.Id);

                z.Name = c.Name;
                z.Description = c.Description;
                z.System = c.System;
                z.Status = c.Status;

                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }
    }
}
