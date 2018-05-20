using SiteHealth.Services.ViewModels.Base;
using System;

namespace SiteHealth.Services.ViewModels.Site
{
    public class SiteViewModel: ViewModelBase
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UtdatedAt { get; set; }
        public string Name { get; set; }
        public int EndpointsCount { get; set; }
    }
}
