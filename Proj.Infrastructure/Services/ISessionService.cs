using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionDTO>> BrowseAllAsync();
        Task<SessionDTO> GetAsync(int id);
        Task UpdateAsync(int id, UpdateSession p);
        Task DeleteAsync(int id);
        Task AddAsync(CreateSession p);
    }
}
