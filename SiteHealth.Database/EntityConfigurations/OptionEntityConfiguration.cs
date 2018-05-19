using SiteHealth.Entity.Models;
using System.Data.Entity.ModelConfiguration;

namespace BotMagic.Database.EntityConfigurations
{
    public class OptionEntityConfiguration : EntityTypeConfiguration<Option>
    {
        public OptionEntityConfiguration()
        {
            ToTable("dbo.options");
            HasKey(x => x.Id);

            Property(x => x.Key).IsRequired();
        }
    }
}
