using SiteHealth.Database.Common.Base;
using SiteHealth.Database.Concrete.Interfaces;

namespace SiteHealth.Database.Concrete
{
    public class SiteHealthUnitOfWork
        : UnitOfWorkBase<SiteHealthDbContext>, ISiteHealthUnitOfWork
    {
        public SiteHealthUnitOfWork(ISiteHealthDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
