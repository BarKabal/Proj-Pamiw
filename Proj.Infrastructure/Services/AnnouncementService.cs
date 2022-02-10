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
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task AddAsync(CreateAnnouncement a)
        {
            await _announcementRepository.AddAsync(Map(a));
        }

        public async Task<IEnumerable<AnnouncementDTO>> BrowseAllAsync()
        {
            var z = await _announcementRepository.BrowseAllAsync();
            return z.Select(x => Map(x));
        }

        public async Task DeleteAsync(int id)
        {
            var Announcement = _announcementRepository.GetAsync(id).Result;
            await _announcementRepository.DeleteAsync(Announcement);
        }

        public async Task<AnnouncementDTO> GetAsync(int id)
        {
            var a = _announcementRepository.GetAsync(id).Result;
            return await Task.FromResult(Map(a));
        }

        public async Task UpdateAsync(int id, UpdateAnnouncement a)
        {
            await _announcementRepository.UpdateAsync(Map(a, id));
        }

        private AnnouncementDTO Map(Announcement a)
        {
            return new AnnouncementDTO()
            {
                Id = a.Id,
                AnnounceDate = a.AnnounceDate,
                Text = a.Text
            };
        }

        private Announcement Map(CreateAnnouncement a)
        {
            return new Announcement()
            {
                AnnounceDate = a.AnnounceDate,
                Text = a.Text
            };
        }

        private Announcement Map(UpdateAnnouncement a, int id)
        {
            return new Announcement()
            {
                Id = id,
                AnnounceDate = a.AnnounceDate,
                Text = a.Text
            };
        }
    }
}
