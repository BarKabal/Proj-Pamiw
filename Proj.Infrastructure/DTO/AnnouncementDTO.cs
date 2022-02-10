using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Infrastructure.DTO
{
    public class AnnouncementDTO
    {
        public int Id { get; set; }
        public DateTime AnnounceDate { get; set; }
        public string Text { get; set; }
    }
}
