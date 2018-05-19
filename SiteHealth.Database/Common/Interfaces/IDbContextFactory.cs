using System.Data.Entity;

namespace SiteHealth.Database.Common.Interfaces
{
    public interface IDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
