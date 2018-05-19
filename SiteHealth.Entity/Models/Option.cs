using SiteHealth.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Entity.Models
{
    public class Option : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Key { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public virtual string Value { get; set; }
        public virtual string Type { get; set; }
    }
}
