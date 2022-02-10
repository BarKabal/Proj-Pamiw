using Proj.Core.Domain;
using Proj.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private AppDbContext _appDbContext;

        public SessionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Session s)
        {
            try
            {
                _appDbContext.Session.Add(s);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Session>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Session);
        }

        public async Task DeleteAsync(Session s)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Session.FirstOrDefault(x => x.Id == s.Id));
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Session> GetAsync(int id)
        {
            return await Task.FromResult(_appDbContext.Session.FirstOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(Session s)
        {
            try
            {
                var z = _appDbContext.Session.FirstOrDefault(x => x.Id == s.Id);

                z.Date = s.Date;
                z.Description = s.Description;

                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }
    }
}
