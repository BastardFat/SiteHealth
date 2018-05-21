using SiteHealth.Services.ViewModels.Base;

namespace SiteHealth.Services.ViewModels.Endpoint
{
    public class EndpointViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public long SiteId { get; set; }
    }
}
