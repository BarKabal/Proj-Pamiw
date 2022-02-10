using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proj.Core.Domain
{
    public class Campaign
    {
        public Campaign()
        {
            this.Sessions = new HashSet<Session>();
            this.Players = new HashSet<Player>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string System { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
