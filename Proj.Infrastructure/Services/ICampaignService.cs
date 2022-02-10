using Proj.Core.Domain;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Infrastructure.Services
{
    public interface ICampaignService
    {
        Task<IEnumerable<CampaignDTO>> BrowseAll();
        Task<IEnumerable<CampaignDTO>> BrowseAllByFilterAsync(string name);
        Task<CampaignDTO> GetAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, UpdateCampaign c);
        Task AddAsync(CreateCampaign c);

    }
}
