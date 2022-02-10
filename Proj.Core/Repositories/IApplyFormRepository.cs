using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Repositories
{
    public interface IApplyFormRepository
    {
        Task<IEnumerable<ApplyForm>> BrowseAllAsync();
        Task<ApplyForm> GetAsync(int id);
        Task UpdateAsync(ApplyForm af);
        Task DeleteAsync(ApplyForm af);
        Task AddAsync(ApplyForm af);
    }
}
