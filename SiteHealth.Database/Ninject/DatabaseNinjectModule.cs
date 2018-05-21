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
        private bool _singletonMode;
        public DatabaseNinjectModule(bool singletonMode = false)
        {
            _singletonMode = singletonMode;
        }

        public override void Load()
        {
            if (_singletonMode)
            {
                Bind<ISiteHealthDbContextFactory>().To<SiteHealthDbContextFactory>().InSingletonScope();
                Bind<ISiteHealthUnitOfWork>().To<SiteHealthUnitOfWork>().InSingletonScope();
            }
            else
            {
                Bind<ISiteHealthDbContextFactory>().To<SiteHealthDbContextFactory>().InRequestScope();
                Bind<ISiteHealthUnitOfWork>().To<SiteHealthUnitOfWork>().InRequestScope();
            }
                

            var bindings = NinjectDependencyAttribute.GetBindingsForAssembly(GetType().Assembly)
                .Select(x =>
                {
                    var binding = Bind(x.Source).To(x.Destenation);

                    if (_singletonMode)
                        return binding.InSingletonScope();

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
