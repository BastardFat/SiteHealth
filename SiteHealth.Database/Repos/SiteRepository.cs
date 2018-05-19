using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Utils;

namespace SiteHealth.Database.Repos
{
    [NinjectDependency(typeof(ISiteRepository), NinjectDependencyScope.Request)]
    public class SiteRepository : RepositoryBase<Site, SiteHealthDbContext>, ISiteRepository
    {
        public SiteRepository(ISiteHealthDbContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
