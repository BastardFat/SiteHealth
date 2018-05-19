using System.Data.Entity;
using System.Threading.Tasks;

namespace SiteHealth.Database.Common.Interfaces
{
    public interface IUnitOfWork<out TDbContext> where TDbContext : DbContext
    {
        void Commit();
        Task CommitAsync();
    }
}
