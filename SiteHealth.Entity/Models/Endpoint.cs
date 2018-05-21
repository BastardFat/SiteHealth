using SiteHealth.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Entity.Models
{
    public class Endpoint: IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }

        public virtual long SiteId { get; set; }
        public virtual Site Site { get; set; }

        public virtual ICollection<HealthReport> HealthReports { get; set; }
    }
}
