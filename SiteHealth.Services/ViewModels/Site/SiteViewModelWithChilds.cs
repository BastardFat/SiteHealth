using SiteHealth.Services.ViewModels.Endpoint;
using System.Collections.Generic;

namespace SiteHealth.Services.ViewModels.Site
{
    public class SiteViewModelWithChilds: SiteViewModel
    {
        public List<EndpointViewModel> Endpoints { get; set; }
    }
}
