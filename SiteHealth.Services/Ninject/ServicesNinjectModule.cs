using SiteHealth.Utils;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Linq;
using BotMagic.Utils;

namespace SiteHealth.Services.Ninject
{
    public class ServicesNinjectModule : NinjectModule
    {
        private ServicesNinjectModuleOptions _options;
        public ServicesNinjectModule(ServicesNinjectModuleOptions options)
        {
            _options = options;
        }

        public override void Load()
        {
            var bindings = NinjectDependencyAttribute.GetBindingsForAssembly(GetType().Assembly)
                .Select(x =>
                {
                    var binding = Bind(x.Source).To(x.Destenation);

                    if (_options.SingletonMode)
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
            Bind(typeof(IHmacSerializer<>)).To(typeof(HmacSerializer<>)).WithConstructorArgument("key", _options.HmacSerializerKey);
            Bind<ISha256Hasher>().To<Sha256Hasher>().InRequestScope();
        }
    }

    public class ServicesNinjectModuleOptions
    {
        public string HmacSerializerKey { get; set; }
        public bool SingletonMode { get; set; }
    }
}
