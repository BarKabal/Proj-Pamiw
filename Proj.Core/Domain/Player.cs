using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proj.Core.Domain
{
    public class Player
    {
        public Player()
        {
            this.Campaigns = new HashSet<Campaign>();
            this.ApplyForms = new HashSet<ApplyForm>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<ApplyForm> ApplyForms { get; set; }
        public int UserId { get; set; }
    }
}
