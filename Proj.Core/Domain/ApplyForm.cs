using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proj.Core.Domain
{
    public class ApplyForm
    {
        [Key]
        public int Id { get; set; }
        public virtual Player Player { get; set; }
        public virtual Campaign Campaign { get; set; }
        public DateTime PostTime { get; set; }
        public string Message { get; set; }
    }
}
