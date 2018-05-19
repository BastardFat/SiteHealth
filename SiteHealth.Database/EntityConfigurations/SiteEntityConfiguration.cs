using SiteHealth.Entity.Models;
using System.Data.Entity.ModelConfiguration;

namespace BotMagic.Database.EntityConfigurations
{
    public class SiteEntityConfiguration : EntityTypeConfiguration<Site>
    {
        public SiteEntityConfiguration()
        {
            ToTable("dbo.sites");
            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired();
        }
    }
}
