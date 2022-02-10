using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> BrowseAllAsync();
        Task<Announcement> GetAsync(int id);
        Task UpdateAsync(Announcement a);
        Task DeleteAsync(Announcement a);
        Task AddAsync(Announcement a);
    }
}
