using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDTO>> BrowseAllAsync();
        Task<AnnouncementDTO> GetAsync(int id);
        Task UpdateAsync(int id, UpdateAnnouncement a);
        Task DeleteAsync(int id);
        Task AddAsync(CreateAnnouncement a);
    }
}
