using SiteHealth.Services.ViewModels.HealthReport;
using System.Collections.Generic;

namespace SiteHealth.Services.ViewModels.Endpoint
{
    public class EndpointViewModelWithChilds : EndpointViewModel
    {
        public List<HealthReportViewModel> HealthReports { get; set; }
    }
}
