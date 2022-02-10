using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.Infrastructure.DTO
{
    public class CampaignDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string System { get; set; }
        public string Status { get; set; }
    }
}
