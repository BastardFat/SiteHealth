using SiteHealth.Database.Common.Interfaces;

namespace SiteHealth.Database.Concrete.Interfaces
{
    public interface ISiteHealthDbContextFactory
        : IDbContextFactory<SiteHealthDbContext>
    {
    }
}
