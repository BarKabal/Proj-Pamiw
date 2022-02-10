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
    public class ApplyFormService : IApplyFormService
    {
        private readonly IApplyFormRepository _applyFormRepository;

        public ApplyFormService(IApplyFormRepository applyFormRepository)
        {
            _applyFormRepository = applyFormRepository;
        }

        public async Task AddAsync(CreateApplyForm af)
        {
            await _applyFormRepository.AddAsync(Map(af));
        }

        public async Task<IEnumerable<ApplyFormDTO>> BrowseAllAsync()
        {
            var z = await _applyFormRepository.BrowseAllAsync();
            return z.Select(x => Map(x));
        }

        public async Task DeleteAsync(int id)
        {
            var Announcement = _applyFormRepository.GetAsync(id).Result;
            await _applyFormRepository.DeleteAsync(Announcement);
        }

        public async Task<ApplyFormDTO> GetAsync(int id)
        {
            var af = _applyFormRepository.GetAsync(id).Result;
            return await Task.FromResult(Map(af));
        }

        public async Task UpdateAsync(int id, UpdateApplyForm af)
        {
            await _applyFormRepository.UpdateAsync(Map(af, id));
        }

        private ApplyFormDTO Map(ApplyForm af)
        {
            return new ApplyFormDTO()
            {
                Id = af.Id,
                PostTime = af.PostTime,
                Message = af.Message
            };
        }

        private ApplyForm Map(CreateApplyForm af)
        {
            return new ApplyForm()
            {
                PostTime = af.PostTime,
                Message = af.Message
            };
        }

        private ApplyForm Map(UpdateApplyForm af, int id)
        {
            return new ApplyForm()
            {
                Id = id,
                PostTime = af.PostTime,
                Message = af.Message
            };
        }
    }
}
