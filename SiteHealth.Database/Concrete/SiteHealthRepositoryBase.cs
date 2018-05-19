using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Common.Interfaces;
using SiteHealth.Entity.Interfaces;

namespace SiteHealth.Database.Concrete
{
    public abstract class SiteHealthRepositoryBase<TEntity>
        : RepositoryBase<TEntity, SiteHealthDbContext>
        where TEntity : class, IEntity, new()
    {
        protected SiteHealthRepositoryBase(IDbContextFactory<SiteHealthDbContext> contextFactory) : base(contextFactory)
        {
        }
    }
}
