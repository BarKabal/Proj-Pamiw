using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Infrastructure.Commands
{
    public class UpdateAnnouncement
    {
        public DateTime AnnounceDate { get; set; }
        public string Text { get; set; }
    }
}
