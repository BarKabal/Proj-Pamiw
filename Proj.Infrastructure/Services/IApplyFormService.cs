using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public interface IApplyFormService
    {
        Task<IEnumerable<ApplyFormDTO>> BrowseAllAsync();
        Task<ApplyFormDTO> GetAsync(int id);
        Task UpdateAsync(int id, UpdateApplyForm af);
        Task DeleteAsync(int id);
        Task AddAsync(CreateApplyForm af);
    }
}
