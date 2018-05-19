using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete.Interfaces;

namespace SiteHealth.Database.Concrete
{
    public class SiteHealthDbContextFactory
        : DbContextFactoryBase<SiteHealthDbContext>, ISiteHealthDbContextFactory
    {
    }
}
