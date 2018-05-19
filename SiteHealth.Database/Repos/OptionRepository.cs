using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Utils;

namespace SiteHealth.Database.Repos
{
    [NinjectDependency(typeof(IOptionRepository), NinjectDependencyScope.Request)]
    public class OptionRepository : RepositoryBase<Option, SiteHealthDbContext>, IOptionRepository
    {
        public OptionRepository(ISiteHealthDbContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
