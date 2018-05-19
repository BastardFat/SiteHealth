using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Utils;

namespace SiteHealth.Database.Repos
{
    [NinjectDependency(typeof(IHealthReportRepository), NinjectDependencyScope.Request)]
    public class HealthReportRepository : RepositoryBase<HealthReport, SiteHealthDbContext>, IHealthReportRepository
    {
        public HealthReportRepository(ISiteHealthDbContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
