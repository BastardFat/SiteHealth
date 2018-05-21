using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.ViewModels.HealthReport
{
    public class ChartItem
    {
        public DateTime Time { get; set; }
        public long Response { get; set; }
    }
}
