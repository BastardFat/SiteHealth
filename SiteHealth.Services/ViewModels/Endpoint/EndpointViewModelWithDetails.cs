using SiteHealth.Services.ViewModels.HealthReport;
using System.Collections.Generic;

namespace SiteHealth.Services.ViewModels.Endpoint
{
    public class EndpointViewModelWithDetails : EndpointViewModel
    {
        public ChartItem[] Chart { get; set; }
        public double? Uptime { get; set; }
        public StatusesAndErrors Stat { get; set; }
    }
}
