using SiteHealth.Entity.Models;
using System.Data.Entity.ModelConfiguration;

namespace BotMagic.Database.EntityConfigurations
{
    public class EndpointEntityConfiguration : EntityTypeConfiguration<Endpoint>
    {
        public EndpointEntityConfiguration()
        {
            ToTable("dbo.endpoints");
            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired();
            Property(x => x.Url).IsRequired();

            HasRequired(x => x.Site)
                .WithMany(y => y.Endpoints)
                .HasForeignKey(x => x.SiteId);
        }
    }
}
