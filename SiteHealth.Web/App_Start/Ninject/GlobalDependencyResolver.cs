using System.Web.Http;

namespace SiteHealth.Ninject
{
    public static class GlobalDependencyResolver
    {
        public static T GetService<T>()
        {
            return (T) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }
}