using SiteHealth.Services.ViewModels.Site;

namespace SiteHealth.Services.ViewModels.Endpoint
{
    public class EndpointViewModelWithParent : EndpointViewModel
    {
        public SiteViewModel Site { get; set; }
    }
}
