using SiteHealth.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Entity.Models
{
    public class Site : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual int FetchIntervalInMinutes { get; set; }

        public virtual ICollection<Endpoint> Endpoints { get; set; }
    }
}
