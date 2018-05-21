using BotMagic.Utils;
using SiteHealth.Entity.Models;
using System;
using System.Data.Entity;
using System.Reflection;

namespace SiteHealth.Database.Concrete
{
    public class SiteHealthDbContext : DbContext
    {
        public SiteHealthDbContext() : base ("DefaultConnection")
        {
            System.Data.Entity.Database.SetInitializer(new Initializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(SiteHealthDbContext)));
        }
    }

    class Initializer : DropCreateDatabaseIfModelChanges<SiteHealthDbContext>
    {
        protected override void Seed(SiteHealthDbContext context)
        {
            base.Seed(context);

            context.Set<Option>().Add(new Option
            {
                CreatedAt = DateTime.UtcNow,
                Key = "password",
                Type = typeof(string).FullName,
                Value = "admin"
            });
        }
    }
}
