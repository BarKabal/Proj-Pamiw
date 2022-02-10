using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proj.Core.Domain
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Announcement Announcement { get; set; }
        public DateTime Date { get; set; }
    }
}
