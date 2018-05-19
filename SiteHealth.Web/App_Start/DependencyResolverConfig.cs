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

            var kernel = new StandardKernel(

                new DatabaseNinjectModule(),
                new ServicesNinjectModule()

            );


            NinjectDependencyResolver resolver = new NinjectDependencyResolver(kernel);
            DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}