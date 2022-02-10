using Proj.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> BrowseAllAsync();
        Task<Session> GetAsync(int id);
        Task UpdateAsync(Session s);
        Task DeleteAsync(Session s);
        Task AddAsync(Session s);
    }
}
