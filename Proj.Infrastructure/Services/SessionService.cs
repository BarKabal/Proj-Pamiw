using Proj.Core.Domain;
using Proj.Core.Repositories;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task AddAsync(CreateSession s)
        {
            await _sessionRepository.AddAsync(Map(s));
        }

        public async Task<IEnumerable<SessionDTO>> BrowseAllAsync()
        {
            var z = await _sessionRepository.BrowseAllAsync();
            return z.Select(x => Map(x));
        }

        public async Task DeleteAsync(int id)
        {
            var Session = _sessionRepository.GetAsync(id).Result;
            await _sessionRepository.DeleteAsync(Session);
        }

        public async Task<SessionDTO> GetAsync(int id)
        {
            var s = _sessionRepository.GetAsync(id).Result;
            return await Task.FromResult(Map(s));
        }

        public async Task UpdateAsync(int id, UpdateSession s)
        {
            await _sessionRepository.UpdateAsync(Map(s, id));
        }

        private SessionDTO Map(Session s)
        {
            return new SessionDTO()
            {
                Id = s.Id,
                Date = s.Date,
                Description = s.Description
            };
        }

        private Session Map(CreateSession s)
        {
            return new Session()
            {
                Date = s.Date,
                Description = s.Description
            };
        }

        private Session Map(UpdateSession s, int id)
        {
            return new Session()
            {
                Id = id,
                Date = s.Date,
                Description = s.Description
            };
        }
    }
}
