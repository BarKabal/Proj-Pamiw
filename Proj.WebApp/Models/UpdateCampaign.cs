using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.WebApp.Models
{
    public class UpdateCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string System { get; set; }
        public string Status { get; set; }
    }
}
