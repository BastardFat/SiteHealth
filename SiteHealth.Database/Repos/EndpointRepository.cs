using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Utils;

namespace SiteHealth.Database.Repos
{
    [NinjectDependency(typeof(IEndpointRepository), NinjectDependencyScope.Request)]
    public class EndpointRepository : RepositoryBase<Endpoint, SiteHealthDbContext>, IEndpointRepository
    {
        public EndpointRepository(ISiteHealthDbContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
