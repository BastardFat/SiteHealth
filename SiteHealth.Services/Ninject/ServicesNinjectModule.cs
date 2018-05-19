using SiteHealth.Utils;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Linq;

namespace SiteHealth.Services.Ninject
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
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
