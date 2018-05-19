using SiteHealth.Services.ViewModels.Endpoint;

namespace SiteHealth.Services.ViewModels.HealthReport
{
    public class HealthReportViewModelWithParent : HealthReportViewModel
    {
        public EndpointViewModel Endpoint { get; set; }
    }
}
