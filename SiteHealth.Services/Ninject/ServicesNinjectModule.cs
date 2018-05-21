using SiteHealth.Utils;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Linq;

namespace SiteHealth.Services.Ninject
{
    public class ServicesNinjectModule : NinjectModule
    {
        private bool _singletonMode;
        public ServicesNinjectModule(bool singletonMode = false)
        {
            _singletonMode = singletonMode;
        }

        public override void Load()
        {
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
