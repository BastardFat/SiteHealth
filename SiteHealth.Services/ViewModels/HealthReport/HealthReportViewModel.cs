using SiteHealth.Services.ViewModels.Base;
using System;

namespace SiteHealth.Services.ViewModels.HealthReport
{
    public class HealthReportViewModel : ViewModelBase
    {
        public virtual DateTime Timestamp { get; set; }
        public virtual int? StatusCode { get; set; }
        public virtual string Error { get; set; }
        public virtual int? ResponseTime { get; set; }

        public virtual long EndpointId { get; set; }
    }
}
