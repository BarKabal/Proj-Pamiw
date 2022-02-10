using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.WebApp.Models
{
    public class AnnouncementVM
    {
        public int Id { get; set; }
        public DateTime AnnounceDate { get; set; }
        public string Text { get; set; }
    }
}
