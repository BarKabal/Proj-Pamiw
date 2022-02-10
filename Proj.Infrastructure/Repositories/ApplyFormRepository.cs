using Proj.Core.Domain;
using Proj.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Repositories
{
    public class ApplyFormRepository : IApplyFormRepository
    {
        private AppDbContext _appDbContext;

        public ApplyFormRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(ApplyForm af)
        {
            try
            {
                _appDbContext.ApplyForm.Add(af);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<ApplyForm>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.ApplyForm);
        }

        public async Task DeleteAsync(ApplyForm af)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.ApplyForm.FirstOrDefault(x => x.Id == af.Id));
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<ApplyForm> GetAsync(int id)
        {
            return await Task.FromResult(_appDbContext.ApplyForm.FirstOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(ApplyForm af)
        {
            throw new NotImplementedException();
        }
    }
}
