using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDTO>> BrowseAllAsync();
        Task<PlayerDTO> GetAsync(int id);
        Task UpdateAsync(int id, UpdatePlayer p);
        Task DeleteAsync(int id);
        Task AddAsync(CreatePlayer p);
    }
}
