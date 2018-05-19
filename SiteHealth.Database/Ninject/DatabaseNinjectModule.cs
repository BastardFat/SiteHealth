using SiteHealth.Database.Concrete;
using SiteHealth.Database.Concrete.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Linq;
using SiteHealth.Utils;

namespace SiteHealth.Database.Ninject
{
    public class DatabaseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISiteHealthDbContextFactory>().To<SiteHealthDbContextFactory>().InRequestScope();
            Bind<ISiteHealthUnitOfWork>().To<SiteHealthUnitOfWork>().InRequestScope();

            var bindings = NinjectDependencyAttribute.GetBindingsForAssembly(GetType().Assembly)
                .Select(x =>
                {
                    var binding = Bind(x.Source).To(x.Destenation);
                    switch (x.Scope)
                    {
                        case NinjectDependencyScope.Transient:
                            return binding.InTransientScope();
                        case NinjectDependencyScope.Request:
                            return binding.InRequestScope();
                        case NinjectDependencyScope.Thread:
                            return binding.InThreadScope();
                        case NinjectDependencyScope.Singleton:
                        default:
                            return binding.InSingletonScope();
                    }
                })
                .ToArray();

        }
    }
}
