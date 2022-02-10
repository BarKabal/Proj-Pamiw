using Proj.Core.Domain;
using Proj.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private AppDbContext _appDbContext;

        public AnnouncementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Announcement a)
        {
            try
            {
                _appDbContext.Announcement.Add(a);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Announcement>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Announcement);
        }

        public async Task DeleteAsync(Announcement a)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Announcement.FirstOrDefault(x => x.Id == a.Id));
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Announcement> GetAsync(int id)
        {
            return await Task.FromResult(_appDbContext.Announcement.FirstOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(Announcement a)
        {
            throw new NotImplementedException();
        }
    }
}
