using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.ViewModels.HealthReport
{
    public class StatusesAndErrors
    {
        public DateTime Since { get; set; }
        public Dictionary<string, int> StatusCodes { get; set; }
        public Dictionary<string, int> Errors { get; set; }
        public int TotalRequests { get; set; }
    }
}
