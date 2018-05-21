using SiteHealth.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Entity.Models
{
    public class HealthReport: IEntity
    {
        public virtual long Id { get; set; }

        public virtual DateTime Timestamp { get; set; }
        public virtual int? StatusCode { get; set; }
        public virtual string Error { get; set; }
        public virtual long? ResponseTime { get; set; }
        public virtual int IntervalInMinutes { get; set; }

        public virtual long EndpointId{ get; set; }
        public virtual Endpoint Endpoint { get; set; }
    }
}
