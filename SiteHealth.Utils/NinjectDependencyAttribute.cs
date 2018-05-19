using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Utils
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class NinjectDependencyAttribute : Attribute
    {
        public Type InjectFrom { get; private set; }
        public NinjectDependencyScope Scope { get; private set; }

        public NinjectDependencyAttribute(Type injectFrom, NinjectDependencyScope scope)
        {
            InjectFrom = injectFrom;
            Scope = scope;
        }

        public static IEnumerable<NinjectDependencyBinding> GetBindingsForAssembly(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .SelectMany(type =>
                {
                    return type
                        .GetCustomAttributes(typeof(NinjectDependencyAttribute), true)
                        .Cast<NinjectDependencyAttribute>()
                        .Select(attr => new NinjectDependencyBinding
                        {
                            Destenation = type,
                            Source = attr.InjectFrom,
                            Scope = attr.Scope,
                        });
                })
                .ToArray();
        }
    }

    public class NinjectDependencyBinding
    {
        public Type Source { get; set; }
        public Type Destenation { get; set; }
        public NinjectDependencyScope Scope { get; set; }
    }

    public enum NinjectDependencyScope
    {
        Transient,
        Request,
        Thread,
        Singleton
    }
}