using SiteHealth.Entity.Models;
using System.Data.Entity.ModelConfiguration;

namespace BotMagic.Database.EntityConfigurations
{
    public class HealthReportEntityConfiguration : EntityTypeConfiguration<HealthReport>
    {
        public HealthReportEntityConfiguration()
        {
            ToTable("dbo.reports");
            HasKey(x => x.Id);

            Property(x => x.Timestamp).IsRequired();

            Property(x => x.Error).IsOptional();
            Property(x => x.StatusCode).IsOptional();
            Property(x => x.ResponseTime).IsOptional();

            HasRequired(x => x.Endpoint)
                .WithMany(y => y.HealthReports)
                .HasForeignKey(x => x.EndpointId);
        }
    }
}
