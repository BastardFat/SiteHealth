using SiteHealth.Services.ViewModels.Endpoint;
using System.Collections.Generic;

namespace SiteHealth.Services.ViewModels.Site
{
    public class SiteViewModelWithDetailedChilds : SiteViewModel
    {
        public List<EndpointViewModelWithDetails> Endpoints { get; set; }
    }
}
