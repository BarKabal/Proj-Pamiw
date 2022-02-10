using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proj.Core.Domain
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        public DateTime AnnounceDate { get; set; }
        public string Text { get; set; }
    }
}
