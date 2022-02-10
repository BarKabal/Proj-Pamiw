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
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campainRepository;

        public CampaignService(ICampaignRepository campainRepository)
        {
            _campainRepository = campainRepository;
        }

        public async Task AddAsync(CreateCampaign c)
        {
            await _campainRepository.AddAsync(Map(c));
        }

        public async Task<IEnumerable<CampaignDTO>> BrowseAll()
        {
            var z = await _campainRepository.BrowseAllAsync();
            return z.Select(x => Map(x));
        }

        public async Task<IEnumerable<CampaignDTO>> BrowseAllByFilterAsync(string name)
        {
            var z = await _campainRepository.BrowseAllByFilterAsync(name);
            return z.Select(x => Map(x));
        }

        public async Task DeleteAsync(int id)
        {
            var Campaign = _campainRepository.GetAsync(id).Result;
            await _campainRepository.DeleteAsync(Campaign);
        }

        public async Task<CampaignDTO> GetAsync(int id)
        {
            var c = _campainRepository.GetAsync(id).Result;
            return await Task.FromResult(Map(c));
        }

        public async Task UpdateAsync(int id, UpdateCampaign c)
        {
            await _campainRepository.UpdateAsync(Map(c, id));
        }

        private CampaignDTO Map(Campaign c)
        {
            return new CampaignDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                System = c.System,
                Status = c.Status
            };
        }

        private Campaign Map(CreateCampaign c)
        {
            return new Campaign()
            {
                Name = c.Name,
                Description = c.Description,
                System = c.System
            };
        }

        private Campaign Map(UpdateCampaign c, int id)
        {
            return new Campaign()
            {
                Id = id,
                Name = c.Name,
                Description = c.Description,
                System = c.System
            };
        }
    }
}
