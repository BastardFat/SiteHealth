using SiteHealth.Ninject;
using SiteHealth.Database.Ninject;
using SiteHealth.Services.Ninject;
using Ninject;
using System.Web.Http;
using System.Web.Mvc;
using System;

namespace SiteHealth
{
    public class DependencyResolverConfig
    {
        public static void RegisterNinject()
        {
            NinjectDependencyResolver resolver = GetResolver();
            DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static NinjectDependencyResolver GetResolver(bool singletonMode = false)
        {
            var kernel = new StandardKernel(

                new DatabaseNinjectModule(singletonMode),
                new ServicesNinjectModule(singletonMode)

            );


            NinjectDependencyResolver resolver = new NinjectDependencyResolver(kernel);
            return resolver;
        }
    }
}